using API.Models;
using Net.payOS;
using PayOSTypes = Net.payOS.Types;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.Data.Repositories;
using WinFormsFashionShop.Data;
using Microsoft.Extensions.Configuration;
using WinFormsFashionShop.Data.Entities;

namespace API.Services
{
    /// <summary>
    /// Service xử lý thanh toán PayOS
    /// </summary>
    public class PaymentService : IPaymentService
    {
        private readonly PayOS _payOS;
        private readonly IOrderService _orderService;
        private readonly IOrderRepository _orderRepository;
        private readonly IConfiguration _configuration;

        public PaymentService(
            IOrderService orderService,
            IOrderRepository orderRepository,
            IConfiguration configuration)
        {
            _orderService = orderService;
            _orderRepository = orderRepository;
            _configuration = configuration;

            // Lấy PayOS config từ appsettings.json hoặc environment variables
            var clientId = _configuration["PayOS:ClientId"] ?? throw new InvalidOperationException("PayOS:ClientId chưa được cấu hình");
            var apiKey = _configuration["PayOS:ApiKey"] ?? throw new InvalidOperationException("PayOS:ApiKey chưa được cấu hình");
            var checksumKey = _configuration["PayOS:ChecksumKey"] ?? throw new InvalidOperationException("PayOS:ChecksumKey chưa được cấu hình");

            _payOS = new PayOS(clientId, apiKey, checksumKey);
        }

        /// <summary>
        /// Tạo payment link từ PayOS và lưu PayOSOrderCode vào database
        /// CRITICAL: Nếu invoice đã có PayOSOrderCode, KHÔNG tạo payment link mới, chỉ return existing info
        /// </summary>
        public async Task<CreatePaymentResponse> CreatePaymentLinkAsync(CreatePaymentRequest request)
        {
            try
            {
                // Lấy thông tin đơn hàng từ database
                var order = _orderService.GetOrderById(request.OrderId);
                if (order == null)
                {
                    return new CreatePaymentResponse
                    {
                        Success = false,
                        Message = $"Không tìm thấy đơn hàng với ID {request.OrderId}"
                    };
                }

                // CRITICAL CHECK: Nếu invoice đã có PayOSOrderCode, KHÔNG tạo payment link mới
                var orderEntity = _orderRepository.GetById(request.OrderId);
                if (orderEntity != null && orderEntity.PayOSOrderCode.HasValue && orderEntity.PayOSOrderCode.Value > 0)
                {
                    var existingPayOSOrderCode = orderEntity.PayOSOrderCode.Value;
                    System.Diagnostics.Debug.WriteLine($"🚫 BLOCKED: Order {request.OrderId} đã có PayOSOrderCode={existingPayOSOrderCode}. KHÔNG tạo payment link mới.");
                    
                    // Kiểm tra trạng thái từ PayOS để lấy payment info hiện tại
                    try
                    {
                        var payOSStatus = await CheckPayOSPaymentStatusAsync(existingPayOSOrderCode);
                        
                        // Nếu PayOS status = PAID, update database
                        if (payOSStatus == "PAID" && orderEntity.Status != "Paid")
                        {
                            System.Diagnostics.Debug.WriteLine($"✅ PayOS status is PAID, updating order {request.OrderId} via stored procedure");
                            var webhookId = $"EXISTING_CHECK_{existingPayOSOrderCode}_{DateTime.Now:yyyyMMddHHmmss}";
                            var webhookResult = await Task.Run(() => _orderRepository.ProcessPayOSWebhook(
                                webhookId: webhookId,
                                payOSOrderCode: existingPayOSOrderCode,
                                code: "00",
                                amount: (int)orderEntity.TotalAmount,
                                reference: null,
                                paymentLinkId: null,
                                rawData: $"{{\"source\":\"existing_payment_check\",\"orderId\":{request.OrderId},\"payOSOrderCode\":{existingPayOSOrderCode}}}",
                                ipAddress: "ExistingPaymentCheck",
                                userAgent: "PaymentService-ExistingCheck"
                            ));
                            
                            if (webhookResult.Result == "Success")
                            {
                                System.Diagnostics.Debug.WriteLine($"✅ Order {request.OrderId} updated to Paid from existing PayOSOrderCode. Decreasing inventory...");
                                try
                                {
                                    _orderService.DecreaseInventoryForPaidOrder(request.OrderId);
                                    System.Diagnostics.Debug.WriteLine($"✅ Inventory decreased for order {request.OrderId}");
                                }
                                catch (Exception invEx)
                                {
                                    System.Diagnostics.Debug.WriteLine($"⚠️  WARNING: Cannot decrease inventory for order {request.OrderId}: {invEx.Message}");
                                }
                                orderEntity = _orderRepository.GetById(request.OrderId); // Reload để lấy status mới
                            }
                        }
                        
                        // Return existing payment info (không tạo mới)
                        // orderEntity đã được reload ở dòng 99 nếu có update, hoặc vẫn là từ dòng 59
                        if (orderEntity == null)
                        {
                            throw new InvalidOperationException($"Order {request.OrderId} not found after reload");
                        }
                        return new CreatePaymentResponse
                        {
                            Success = true,
                            Message = $"Payment link đã tồn tại (PayOSOrderCode: {existingPayOSOrderCode}). Không tạo payment link mới.",
                            Data = new API.Models.PaymentData
                            {
                                OrderCode = existingPayOSOrderCode,
                                QrCode = string.Empty, // Không có QR code vì không tạo mới
                                CheckoutUrl = $"https://pay.payos.vn/web/{existingPayOSOrderCode}", // Construct URL từ orderCode
                                Amount = (int)orderEntity.TotalAmount,
                                Description = orderEntity.Notes ?? $"Đơn hàng #{request.OrderId}"
                            }
                        };
                    }
                    catch (Exception checkEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"⚠️  Cannot check PayOS status for existing PayOSOrderCode {existingPayOSOrderCode}: {checkEx.Message}");
                        // Vẫn return success với existing PayOSOrderCode
                        // orderEntity đã được định nghĩa ở dòng 59
                        if (orderEntity == null)
                        {
                            throw new InvalidOperationException($"Order {request.OrderId} not found");
                        }
                        return new CreatePaymentResponse
                        {
                            Success = true,
                            Message = $"Payment link đã tồn tại (PayOSOrderCode: {existingPayOSOrderCode}). Không tạo payment link mới.",
                            Data = new API.Models.PaymentData
                            {
                                OrderCode = existingPayOSOrderCode,
                                QrCode = string.Empty,
                                CheckoutUrl = $"https://pay.payos.vn/web/{existingPayOSOrderCode}",
                                Amount = (int)orderEntity.TotalAmount,
                                Description = orderEntity.Notes ?? $"Đơn hàng #{request.OrderId}"
                            }
                        };
                    }
                }

                // CHỈ TẠO PAYMENT LINK MỚI NẾU CHƯA CÓ PayOSOrderCode
                System.Diagnostics.Debug.WriteLine($"✅ Creating NEW payment link for order {request.OrderId} (no existing PayOSOrderCode)");

                // Tạo unique PayOS orderCode từ OrderId và timestamp để tránh trùng
                // PayOS yêu cầu orderCode phải unique, nên kết hợp OrderId với timestamp
                var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                var payOSOrderCode = (int)((request.OrderId * 1000L + (timestamp % 1000)) % int.MaxValue);
                
                // Đảm bảo orderCode > 0 (PayOS requirement)
                if (payOSOrderCode <= 0)
                {
                    payOSOrderCode = (int)(timestamp % int.MaxValue);
                    if (payOSOrderCode <= 0) payOSOrderCode = 1;
                }

                // Tạo payment link từ PayOS
                var paymentData = new PayOSTypes.PaymentData(
                    orderCode: payOSOrderCode, // Sử dụng unique orderCode
                    amount: request.Amount,
                    description: request.Description.Length > 25 
                        ? request.Description.Substring(0, 25) 
                        : request.Description,
                    items: new List<PayOSTypes.ItemData>
                    {
                        new PayOSTypes.ItemData(request.Description, 1, request.Amount)
                    },
                    cancelUrl: request.CancelUrl ?? "https://payos.vn",
                    returnUrl: request.ReturnUrl ?? "https://payos.vn"
                );

                var result = await _payOS.createPaymentLink(paymentData);

                // Log để debug
                System.Diagnostics.Debug.WriteLine($"PaymentService - CreatePaymentLink: OrderId={request.OrderId}, Calculated payOSOrderCode={payOSOrderCode}, PayOS result.orderCode={result.orderCode}");

                // Cập nhật PayOSOrderCode vào database (LUÔN sử dụng result.orderCode từ PayOS response)
                // LƯU Ý: Đến đây chắc chắn orderEntity.PayOSOrderCode IS NULL (đã check ở trên)
                // Reload orderEntity để đảm bảo có dữ liệu mới nhất (orderEntity đã được định nghĩa ở dòng 59)
                orderEntity = _orderRepository.GetById(request.OrderId);
                if (orderEntity != null)
                {
                    // Đảm bảo không có PayOSOrderCode cũ (defensive check)
                    if (orderEntity.PayOSOrderCode.HasValue && orderEntity.PayOSOrderCode.Value > 0)
                    {
                        System.Diagnostics.Debug.WriteLine($"❌ CRITICAL ERROR: Order {request.OrderId} đã có PayOSOrderCode={orderEntity.PayOSOrderCode.Value} nhưng code vẫn tạo payment mới! Điều này không nên xảy ra.");
                        throw new InvalidOperationException($"Order {request.OrderId} đã có PayOSOrderCode. Không được tạo payment link mới.");
                    }

                    // PayOS trả về result.orderCode - đây là PayOSOrderCode thực sự cần lưu
                    // Lưu ý: result.orderCode có thể khác với payOSOrderCode đã tính toán nếu PayOS tự động điều chỉnh
                    if (result.orderCode > 0)
                    {
                        orderEntity.PayOSOrderCode = result.orderCode;
                        System.Diagnostics.Debug.WriteLine($"✅ Updating order {request.OrderId} with PayOSOrderCode={result.orderCode} (from PayOS response)");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"❌ ERROR: PayOS returned invalid orderCode: {result.orderCode} for OrderId={request.OrderId}");
                        // Vẫn lưu payOSOrderCode đã tính toán nếu PayOS không trả về hợp lệ
                        orderEntity.PayOSOrderCode = payOSOrderCode;
                        System.Diagnostics.Debug.WriteLine($"⚠️  Fallback: Using calculated payOSOrderCode={payOSOrderCode}");
                    }
                    
                    // CHỈ set Status = "Pending" nếu order chưa Paid
                    // Nếu đã Paid thì giữ nguyên Status = "Paid"
                    if (orderEntity.Status != "Paid")
                    {
                        orderEntity.Status = "Pending";
                    }
                    
                    // LUÔN UPDATE để lưu PayOSOrderCode
                    _orderRepository.Update(orderEntity);
                    System.Diagnostics.Debug.WriteLine($"✅ Update() called for order {request.OrderId}");
                    
                    // CRITICAL: Sau khi tạo payment link, tự động check payment status
                    // Vì có thể payment đã được thanh toán ngay sau khi tạo link (race condition)
                    await System.Threading.Tasks.Task.Delay(500); // Đợi 500ms để PayOS xử lý
                    
                    try
                    {
                        var payOSStatus = await CheckPayOSPaymentStatusAsync(result.orderCode);
                        if (payOSStatus == "PAID")
                        {
                            // Payment đã được thanh toán, update ngay
                            System.Diagnostics.Debug.WriteLine($"✅ Payment link {result.orderCode} already PAID, updating order {request.OrderId}");
                            
                            // Gọi stored procedure để update
                            var webhookId = $"AUTO_CHECK_{result.orderCode}_{DateTime.Now:yyyyMMddHHmmss}";
                            var webhookResult = await Task.Run(() => _orderRepository.ProcessPayOSWebhook(
                                webhookId: webhookId,
                                payOSOrderCode: result.orderCode,
                                code: "00",
                                amount: request.Amount,
                                reference: null,
                                paymentLinkId: null,
                                rawData: $"{{\"code\":\"00\",\"desc\":\"Success\",\"data\":{{\"orderCode\":{result.orderCode},\"amount\":{request.Amount}}}}}",
                                ipAddress: "AUTO_CHECK",
                                userAgent: "PaymentService-AutoCheck"
                            ));
                            
                            if (webhookResult.Result == "Success")
                            {
                                System.Diagnostics.Debug.WriteLine($"✅ Order {request.OrderId} auto-updated to Paid. Decreasing inventory...");
                                try
                                {
                                    _orderService.DecreaseInventoryForPaidOrder(request.OrderId);
                                    System.Diagnostics.Debug.WriteLine($"✅ Inventory decreased for order {request.OrderId}");
                                }
                                catch (Exception invEx)
                                {
                                    System.Diagnostics.Debug.WriteLine($"⚠️  WARNING: Cannot decrease inventory for order {request.OrderId}: {invEx.Message}");
                                }
                            }
                        }
                    }
                    catch (Exception checkEx)
                    {
                        // Ignore check errors, sẽ check lại khi user click "Check payment"
                        System.Diagnostics.Debug.WriteLine($"⚠️  Could not auto-check payment status: {checkEx.Message}");
                    }
                    
                    // Verify update sau 100ms để đảm bảo DB đã commit
                    await System.Threading.Tasks.Task.Delay(100);
                    var verifyEntity = _orderRepository.GetById(request.OrderId);
                    var payOSCodeStr = verifyEntity?.PayOSOrderCode?.ToString() ?? "NULL";
                    System.Diagnostics.Debug.WriteLine($"✅ Verify - Order {request.OrderId} PayOSOrderCode={payOSCodeStr}, Status={verifyEntity?.Status}");
                    
                    if (verifyEntity?.PayOSOrderCode == null || verifyEntity.PayOSOrderCode <= 0)
                    {
                        System.Diagnostics.Debug.WriteLine($"❌ CRITICAL: PayOSOrderCode vẫn NULL sau khi update! OrderId={request.OrderId}");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"❌ ERROR: Order {request.OrderId} not found in database!");
                    throw new InvalidOperationException($"Không tìm thấy đơn hàng với ID {request.OrderId}");
                }

                return new CreatePaymentResponse
                {
                    Success = true,
                    Message = "Tạo payment link thành công",
                    Data = new API.Models.PaymentData
                    {
                        OrderCode = result.orderCode,
                        QrCode = result.qrCode ?? string.Empty,
                        CheckoutUrl = result.checkoutUrl ?? string.Empty,
                        Amount = result.amount,
                        Description = result.description ?? string.Empty
                    }
                };
            }
            catch (Exception ex)
            {
                return new CreatePaymentResponse
                {
                    Success = false,
                    Message = $"Lỗi khi tạo payment link: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Kiểm tra trạng thái thanh toán từ database, nếu chưa paid thì kiểm tra từ PayOS API
        /// </summary>
        public async Task<PaymentStatusResponse> GetPaymentStatusAsync(int orderId)
        {
            try
            {
                var order = _orderService.GetOrderById(orderId);
                if (order == null)
                {
                    return new PaymentStatusResponse
                    {
                        Success = false,
                        Message = $"Không tìm thấy đơn hàng với ID {orderId}"
                    };
                }

                // Lấy thông tin chi tiết từ entity để có PaidAt và TransactionId
                var orderEntity = _orderRepository.GetById(orderId);
                if (orderEntity == null)
                {
                    return new PaymentStatusResponse
                    {
                        Success = false,
                        Message = $"Không tìm thấy đơn hàng với ID {orderId}"
                    };
                }

                var status = orderEntity.Status ?? "Pending";
                var payOSOrderCodeInDb = orderEntity.PayOSOrderCode;

                // Log để debug
                System.Diagnostics.Debug.WriteLine($"GetPaymentStatus - Order {orderId}: Status={status}, PayOSOrderCode={payOSOrderCodeInDb}");

                // Nếu PayOSOrderCode NULL và PaymentMethod là VietQR, cảnh báo
                // Không thể tự động sync vì PayOS API không hỗ trợ query theo description/amount
                if (!payOSOrderCodeInDb.HasValue && orderEntity.PaymentMethod?.Contains("VietQR") == true)
                {
                    System.Diagnostics.Debug.WriteLine($"WARNING: Order {orderId} has PaymentMethod=VietQR but PayOSOrderCode is NULL. Cannot check PayOS status. PayOSOrderCode should be saved when creating payment link.");
                }

                // Nếu chưa thanh toán và có PayOSOrderCode, kiểm tra trực tiếp từ PayOS API
                // (vì webhook có thể không đến được nếu API chạy trên localhost)
                if (status != "Paid" && payOSOrderCodeInDb.HasValue)
                {
                    try
                    {
                        System.Diagnostics.Debug.WriteLine($"Checking PayOS API for order code: {payOSOrderCodeInDb.Value}");
                        var payOSStatus = await CheckPayOSPaymentStatusAsync(payOSOrderCodeInDb.Value);
                        System.Diagnostics.Debug.WriteLine($"Order {orderId} - PayOS Status: {payOSStatus}, Current DB Status: {status}");
                        
                        // Kiểm tra nếu status là PAID (PayOS trả về "PAID" khi đã thanh toán)
                        if (payOSStatus == "PAID")
                        {
                            // Đã thanh toán trên PayOS, cập nhật qua Stored Procedure để có Audit Log
                            System.Diagnostics.Debug.WriteLine($"PayOS status is PAID, updating via Stored Procedure for order {orderId}, PayOSOrderCode {payOSOrderCodeInDb.Value}");
                            
                            // Lấy thông tin chi tiết từ PayOS để tạo webhook data giả lập
                            try
                            {
                                // Tạo webhookId unique từ PayOSOrderCode để đảm bảo idempotency
                                var webhookId = $"CHECK_{payOSOrderCodeInDb.Value}_{DateTime.Now:yyyyMMddHHmmss}";
                                
                                // Gọi stored procedure để update với audit log
                                var webhookResult = await Task.Run(() => _orderRepository.ProcessPayOSWebhook(
                                    webhookId: webhookId,
                                    payOSOrderCode: payOSOrderCodeInDb.Value,
                                    code: "00", // PayOS code "00" = thành công
                                    amount: (int)orderEntity.TotalAmount,
                                    reference: null,
                                    paymentLinkId: null,
                                    rawData: $"{{\"source\":\"manual_check\",\"orderId\":{orderId},\"payOSOrderCode\":{payOSOrderCodeInDb.Value}}}",
                                    ipAddress: "ManualCheck",
                                    userAgent: "PaymentStatusCheck"
                                ));

                                System.Diagnostics.Debug.WriteLine($"Stored Procedure result: {webhookResult.Result}, Message: {webhookResult.Message}");
                                
                                if (webhookResult.Result == "Success")
                                {
                                    status = "Paid";
                                    System.Diagnostics.Debug.WriteLine($"✅ Order {orderId} updated to Paid via GetPaymentStatus. Decreasing inventory...");
                                    try
                                    {
                                        _orderService.DecreaseInventoryForPaidOrder(orderId);
                                        System.Diagnostics.Debug.WriteLine($"✅ Inventory decreased for order {orderId}");
                                    }
                                    catch (Exception invEx)
                                    {
                                        System.Diagnostics.Debug.WriteLine($"⚠️  WARNING: Cannot decrease inventory for order {orderId}: {invEx.Message}");
                                    }
                                    // Reload entity để lấy PaidAt mới nhất
                                    var reloadedEntity = _orderRepository.GetById(orderId);
                                    if (reloadedEntity != null)
                                    {
                                        orderEntity = reloadedEntity;
                                    }
                                }
                                else
                                {
                                    System.Diagnostics.Debug.WriteLine($"WARNING: Stored Procedure returned {webhookResult.Result}: {webhookResult.Message}");
                                    // Nếu stored procedure fail (có thể do đã được xử lý trước đó), vẫn cập nhật status từ DB
                                    var reloadedEntity = _orderRepository.GetById(orderId);
                                    if (reloadedEntity != null && reloadedEntity.Status == "Paid")
                                    {
                                        orderEntity = reloadedEntity;
                                        status = "Paid";
                                    }
                                }
                            }
                            catch (Exception spEx)
                            {
                                System.Diagnostics.Debug.WriteLine($"ERROR calling Stored Procedure: {spEx.Message}");
                                System.Diagnostics.Debug.WriteLine($"Stack trace: {spEx.StackTrace}");
                                // Fallback: update trực tiếp nếu stored procedure fail
                                System.Diagnostics.Debug.WriteLine($"FALLBACK: Updating order directly");
                                orderEntity.Status = "Paid";
                                orderEntity.PaidAt = DateTime.Now;
                                _orderRepository.Update(orderEntity);
                                status = "Paid";
                            }
                        }
                        else if (payOSStatus != "PENDING")
                        {
                            // Log status khác để debug
                            System.Diagnostics.Debug.WriteLine($"PayOS returned status: {payOSStatus} (not PAID or PENDING)");
                        }
                    }
                    catch (InvalidOperationException ex)
                    {
                        // Lỗi network/timeout khi gọi PayOS API
                        System.Diagnostics.Debug.WriteLine($"WARNING: Cannot check PayOS API for order {orderId}, PayOSOrderCode {payOSOrderCodeInDb.Value}: {ex.Message}");
                        
                        // Vì không thể kiểm tra PayOS API, nhưng PayOS web đã hiển thị "Đã thanh toán",
                        // ta sẽ gọi stored procedure để update (nếu user chắc chắn đã thanh toán)
                        // Hoặc có thể skip và đợi webhook
                        
                        System.Diagnostics.Debug.WriteLine($"NOTE: PayOS API không thể kết nối. " +
                            "Nếu bạn đã thanh toán trên PayOS web, webhook sẽ tự động update sau. " +
                            "Hoặc có thể update thủ công qua stored procedure.");
                    }
                    catch (Exception ex)
                    {
                        // Log lỗi chi tiết để debug
                        System.Diagnostics.Debug.WriteLine($"Error checking PayOS status for order {orderId}, PayOSOrderCode {payOSOrderCodeInDb.Value}: {ex.Message}");
                        System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                    }
                }
                else if (!payOSOrderCodeInDb.HasValue)
                {
                    System.Diagnostics.Debug.WriteLine($"Order {orderId} has no PayOSOrderCode, cannot check PayOS status");
                }

                return new PaymentStatusResponse
                {
                    Success = true,
                    Message = "Lấy trạng thái thành công",
                    Data = new PaymentStatusData
                    {
                        OrderId = orderId,
                        PayOSOrderCode = orderEntity.PayOSOrderCode,
                        Status = status,
                        PaidAt = orderEntity.PaidAt,
                        TransactionId = orderEntity.TransactionId
                    }
                };
            }
            catch (Exception ex)
            {
                return new PaymentStatusResponse
                {
                    Success = false,
                    Message = $"Lỗi khi lấy trạng thái: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Kiểm tra trạng thái thanh toán trực tiếp từ PayOS API
        /// </summary>
        private async Task<string> CheckPayOSPaymentStatusAsync(int payOSOrderCode)
        {
            try
            {
                using var httpClient = new System.Net.Http.HttpClient();
                // Tăng timeout để tránh lỗi network
                httpClient.Timeout = TimeSpan.FromSeconds(30);
                
                var apiUrl = $"https://api.payos.vn/v2/payment-requests/{payOSOrderCode}";

                var clientId = _configuration["PayOS:ClientId"] ?? throw new InvalidOperationException("PayOS:ClientId chưa được cấu hình");
                var apiKey = _configuration["PayOS:ApiKey"] ?? throw new InvalidOperationException("PayOS:ApiKey chưa được cấu hình");

                httpClient.DefaultRequestHeaders.Add("x-client-id", clientId);
                httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);

                System.Diagnostics.Debug.WriteLine($"Attempting to connect to PayOS API: {apiUrl}");
                
                var response = await httpClient.GetAsync(apiUrl);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                // LOG: Print raw response
                System.Diagnostics.Debug.WriteLine($"");
                System.Diagnostics.Debug.WriteLine($"═══════════════════════════════════════════════════════════");
                System.Diagnostics.Debug.WriteLine($"📥 PAYOS API RESPONSE - OrderCode: {payOSOrderCode}");
                System.Diagnostics.Debug.WriteLine($"═══════════════════════════════════════════════════════════");
                System.Diagnostics.Debug.WriteLine($"HTTP Status Code: {response.StatusCode}");
                System.Diagnostics.Debug.WriteLine($"Response Content Length: {responseContent.Length} characters");
                System.Diagnostics.Debug.WriteLine($"Raw Response Content:");
                System.Diagnostics.Debug.WriteLine($"{responseContent}");
                System.Diagnostics.Debug.WriteLine($"");

                if (response.IsSuccessStatusCode)
                {
                    // LOG: Deserialize JSON
                    System.Diagnostics.Debug.WriteLine($"🔄 Deserializing JSON response...");
                    var jsonResponse = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(responseContent);
                    System.Diagnostics.Debug.WriteLine($"✅ JSON deserialized successfully");
                    System.Diagnostics.Debug.WriteLine($"JSON Root Element Kind: {jsonResponse.ValueKind}");
                    System.Diagnostics.Debug.WriteLine($"");

                    if (jsonResponse.TryGetProperty("data", out var dataElement))
                    {
                        // LOG: Print data element
                        System.Diagnostics.Debug.WriteLine($"📦 Found 'data' property");
                        System.Diagnostics.Debug.WriteLine($"Data Element Kind: {dataElement.ValueKind}");
                        System.Diagnostics.Debug.WriteLine($"");
                        
                        // LOG: List all properties in data element
                        System.Diagnostics.Debug.WriteLine($"📋 All properties in 'data' element:");
                        foreach (var prop in dataElement.EnumerateObject())
                        {
                            var propValue = prop.Value.ValueKind == System.Text.Json.JsonValueKind.String 
                                ? prop.Value.GetString() 
                                : prop.Value.ToString();
                            System.Diagnostics.Debug.WriteLine($"  - {prop.Name}: {propValue}");
                        }
                        System.Diagnostics.Debug.WriteLine($"");
                        
                        // PayOS có thể trả về status ở nhiều nơi khác nhau
                        var status = dataElement.TryGetProperty("status", out var statusElement)
                            ? statusElement.GetString() ?? ""
                            : "";
                        
                        // LOG: Print status variable
                        System.Diagnostics.Debug.WriteLine($"🔍 VARIABLE ASSIGNMENT:");
                        System.Diagnostics.Debug.WriteLine($"  ═> status = '{status}' (from data.status)");
                        
                        // Kiểm tra các field khác có thể cho biết trạng thái thanh toán
                        // PayOS có thể trả về paymentStatus, transactionStatus, hoặc trong transactions array
                        string? paymentStatus = null;
                        if (dataElement.TryGetProperty("paymentStatus", out var paymentStatusElement))
                        {
                            paymentStatus = paymentStatusElement.GetString();
                            System.Diagnostics.Debug.WriteLine($"  ═> paymentStatus = '{paymentStatus}' (from data.paymentStatus)");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"  ═> paymentStatus = NULL (data.paymentStatus not found)");
                        }
                        
                        string? transactionStatus = null;
                        if (dataElement.TryGetProperty("transactionStatus", out var transStatusElement))
                        {
                            transactionStatus = transStatusElement.GetString();
                            System.Diagnostics.Debug.WriteLine($"  ═> transactionStatus = '{transactionStatus}' (from data.transactionStatus)");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"  ═> transactionStatus = NULL (data.transactionStatus not found)");
                        }
                        
                        // Nếu có transactions array, kiểm tra status của transaction đầu tiên
                        if (dataElement.TryGetProperty("transactions", out var transactionsElement))
                        {
                            System.Diagnostics.Debug.WriteLine($"  ═> Found 'transactions' array (Kind: {transactionsElement.ValueKind})");
                            
                            if (transactionsElement.ValueKind == System.Text.Json.JsonValueKind.Array)
                            {
                                var arrayLength = transactionsElement.GetArrayLength();
                                System.Diagnostics.Debug.WriteLine($"  ═> Transactions array length: {arrayLength}");
                                
                                if (arrayLength > 0)
                                {
                                    var firstTransaction = transactionsElement[0];
                                    System.Diagnostics.Debug.WriteLine($"  ═> First transaction Kind: {firstTransaction.ValueKind}");
                                    
                                    if (firstTransaction.TryGetProperty("status", out var firstTransStatusElement))
                                    {
                                        var firstTransStatus = firstTransStatusElement.GetString();
                                        transactionStatus = firstTransStatus ?? transactionStatus;
                                        System.Diagnostics.Debug.WriteLine($"  ═> transactionStatus UPDATED = '{transactionStatus}' (from transactions[0].status)");
                                    }
                                }
                            }
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"  ═> transactions = NULL (data.transactions not found)");
                        }
                        
                        // Normalize tất cả status về uppercase để so sánh
                        var upperStatus = status.ToUpper();
                        var upperPaymentStatus = paymentStatus?.ToUpper() ?? "";
                        var upperTransactionStatus = transactionStatus?.ToUpper() ?? "";
                        
                        // LOG: Print normalized values
                        System.Diagnostics.Debug.WriteLine($"");
                        System.Diagnostics.Debug.WriteLine($"🔤 NORMALIZED VALUES:");
                        System.Diagnostics.Debug.WriteLine($"  ═> upperStatus = '{upperStatus}' (from '{status}')");
                        System.Diagnostics.Debug.WriteLine($"  ═> upperPaymentStatus = '{upperPaymentStatus}' (from '{paymentStatus ?? "NULL"}')");
                        System.Diagnostics.Debug.WriteLine($"  ═> upperTransactionStatus = '{upperTransactionStatus}' (from '{transactionStatus ?? "NULL"}')");
                        System.Diagnostics.Debug.WriteLine($"");
                        
                        // PayOS có thể trả về "PAID", "paid", "Paid", hoặc các giá trị khác
                        // Kiểm tra tất cả các trường hợp có thể
                        System.Diagnostics.Debug.WriteLine($"🔎 CHECKING FOR PAID STATUS:");
                        System.Diagnostics.Debug.WriteLine($"  - upperStatus == 'PAID'? {upperStatus == "PAID"}");
                        System.Diagnostics.Debug.WriteLine($"  - upperPaymentStatus == 'PAID'? {upperPaymentStatus == "PAID"}");
                        System.Diagnostics.Debug.WriteLine($"  - upperTransactionStatus == 'PAID'? {upperTransactionStatus == "PAID"}");
                        
                        if (upperStatus == "PAID" || upperPaymentStatus == "PAID" || upperTransactionStatus == "PAID")
                        {
                            System.Diagnostics.Debug.WriteLine($"");
                            System.Diagnostics.Debug.WriteLine($"✅ RESULT: PAID (detected from status fields)");
                            System.Diagnostics.Debug.WriteLine($"═══════════════════════════════════════════════════════════");
                            System.Diagnostics.Debug.WriteLine($"");
                            return "PAID";
                        }
                        
                        // Nếu không tìm thấy "PAID" ở bất kỳ đâu, có thể PayOS dùng giá trị khác
                        // Kiểm tra xem có amountPaid > 0 không (nếu có field này)
                        System.Diagnostics.Debug.WriteLine($"");
                        System.Diagnostics.Debug.WriteLine($"💰 CHECKING amountPaid field:");
                        if (dataElement.TryGetProperty("amountPaid", out var amountPaidElement))
                        {
                            var amountPaid = amountPaidElement.GetInt32();
                            System.Diagnostics.Debug.WriteLine($"  ═> amountPaid = {amountPaid}");
                            
                            var amount = dataElement.TryGetProperty("amount", out var amountElement) 
                                ? amountElement.GetInt32() 
                                : 0;
                            System.Diagnostics.Debug.WriteLine($"  ═> amount = {amount}");
                            System.Diagnostics.Debug.WriteLine($"  - amountPaid > 0? {amountPaid > 0}");
                            System.Diagnostics.Debug.WriteLine($"  - amountPaid >= amount? {amountPaid >= amount}");
                            
                            if (amountPaid > 0 && amountPaid >= amount)
                            {
                                System.Diagnostics.Debug.WriteLine($"");
                                System.Diagnostics.Debug.WriteLine($"✅ RESULT: PAID (detected from amountPaid >= amount: {amountPaid} >= {amount})");
                                System.Diagnostics.Debug.WriteLine($"═══════════════════════════════════════════════════════════");
                                System.Diagnostics.Debug.WriteLine($"");
                                return "PAID";
                            }
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"  ═> amountPaid = NULL (not found)");
                        }
                        
                        var finalResult = string.IsNullOrEmpty(upperStatus) ? "PENDING" : upperStatus;
                        System.Diagnostics.Debug.WriteLine($"");
                        System.Diagnostics.Debug.WriteLine($"⚠️  RESULT: {finalResult} (not PAID, returning as-is)");
                        System.Diagnostics.Debug.WriteLine($"═══════════════════════════════════════════════════════════");
                        System.Diagnostics.Debug.WriteLine($"");
                        return finalResult;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"PayOS API response missing 'data' property. Full response: {responseContent}");
                    }
                }
                else
                {
                    // Log lỗi chi tiết
                    var errorMsg = $"PayOS API returned error: {response.StatusCode} - {responseContent}";
                    System.Diagnostics.Debug.WriteLine(errorMsg);
                    
                    // Nếu là 404, có thể PayOS Order Code không tồn tại
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        throw new InvalidOperationException($"PayOS Order Code {payOSOrderCode} không tồn tại trên PayOS. Có thể đã bị xóa hoặc chưa được tạo.");
                    }
                }

                return "PENDING";
            }
            catch (System.Net.Http.HttpRequestException httpEx)
            {
                // Lỗi network/DNS - không thể kết nối đến PayOS API
                System.Diagnostics.Debug.WriteLine($"Network error connecting to PayOS API: {httpEx.Message}");
                System.Diagnostics.Debug.WriteLine($"Inner exception: {httpEx.InnerException?.Message}");
                
                // Nếu không thể kết nối PayOS API, không thể xác định status
                // Trả về null để caller biết cần xử lý khác
                throw new InvalidOperationException($"Không thể kết nối đến PayOS API: {httpEx.Message}. " +
                    "Có thể do: (1) Firewall/Proxy chặn, (2) PayOS API đang bảo trì, (3) Lỗi DNS.", httpEx);
            }
            catch (TaskCanceledException timeoutEx)
            {
                // Timeout khi gọi PayOS API
                System.Diagnostics.Debug.WriteLine($"Timeout calling PayOS API: {timeoutEx.Message}");
                throw new InvalidOperationException($"Timeout khi gọi PayOS API. Vui lòng thử lại sau.", timeoutEx);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error checking PayOS status: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                throw new InvalidOperationException($"Lỗi khi kiểm tra PayOS status: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Xử lý webhook từ PayOS qua Stored Procedure với Transaction, Idempotency, Audit log
        /// </summary>
        public async Task<bool> HandleWebhookAsync(PayOSWebhookRequest webhookRequest, string? ipAddress = null, string? userAgent = null)
        {
            try
            {
                if (webhookRequest == null || webhookRequest.Data == null)
                {
                    System.Diagnostics.Debug.WriteLine("Webhook request hoặc data là null");
                    return false;
                }

                var webhookData = webhookRequest.Data;
                
                // CRITICAL: Generate deterministic webhook ID for idempotency
                // Use PaymentLinkId if available, otherwise create deterministic hash
                string webhookId;
                if (!string.IsNullOrEmpty(webhookData.PaymentLinkId))
                {
                    webhookId = webhookData.PaymentLinkId;
                }
                else
                {
                    // Create deterministic hash from OrderCode + Amount + TransactionDateTime
                    // This ensures same webhook always generates same ID (idempotency)
                    var idString = $"{webhookData.OrderCode}|{webhookData.Amount}|{webhookData.TransactionDateTime ?? ""}";
                    webhookId = ComputeSHA256Hash(idString);
                }

                // Serialize raw data để lưu vào database
                var rawDataJson = System.Text.Json.JsonSerializer.Serialize(webhookRequest);

                // Gọi Stored Procedure - xử lý tất cả logic trong database với Transaction
                var result = await Task.Run(() => _orderRepository.ProcessPayOSWebhook(
                    webhookId: webhookId,
                    payOSOrderCode: webhookData.OrderCode,
                    code: webhookRequest.Code,
                    amount: webhookData.Amount,
                    reference: webhookData.Reference,
                    paymentLinkId: webhookData.PaymentLinkId,
                    rawData: rawDataJson,
                    ipAddress: ipAddress,
                    userAgent: userAgent
                ));

                System.Diagnostics.Debug.WriteLine($"ProcessPayOSWebhook result: {result.Result}, Message: {result.Message}, OrderId: {result.OrderId}");

                // Stored Procedure đã xử lý tất cả:
                // - Transaction (atomic)
                // - Idempotency check (chống trùng lặp)
                // - Validation (số tiền, code)
                // - Update Orders table (Status = "Paid")
                // - Insert vào PaymentWebhooks
                // - Insert vào PaymentAuditLog

                // CRITICAL: Nếu webhook update Status = "Paid" thành công, giảm inventory
                // (Vì với VietQR, order được tạo với Status = "Pending" → chưa giảm inventory)
                if (result.Result == "Success" && result.OrderId.HasValue && result.CurrentStatus == "Paid")
                {
                    try
                    {
                        System.Diagnostics.Debug.WriteLine($"✅ Webhook update order {result.OrderId.Value} to Paid. Decreasing inventory...");
                        _orderService.DecreaseInventoryForPaidOrder(result.OrderId.Value);
                        System.Diagnostics.Debug.WriteLine($"✅ Inventory decreased for order {result.OrderId.Value}");
                    }
                    catch (Exception invEx)
                    {
                        // Log error nhưng không fail webhook (order đã Paid rồi)
                        System.Diagnostics.Debug.WriteLine($"⚠️  WARNING: Cannot decrease inventory for order {result.OrderId.Value}: {invEx.Message}");
                        System.Diagnostics.Debug.WriteLine($"Stack trace: {invEx.StackTrace}");
                    }
                }

                // Trả về success nếu kết quả là "Success"
                return result.Result == "Success";
            }
            catch (Exception ex)
            {
                // Log lỗi nhưng không throw để PayOS không retry liên tục
                System.Diagnostics.Debug.WriteLine($"Webhook error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                return false;
            }
        }
        
        /// <summary>
        /// Compute SHA256 hash for deterministic webhook ID generation
        /// </summary>
        private string ComputeSHA256Hash(string input)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hash = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        /// <summary>
        /// Sync PayOSOrderCode tự động - Hiện tại không hỗ trợ vì PayOS API không có endpoint để list payments
        /// PayOSOrderCode phải được lưu khi tạo payment link
        /// </summary>
        public async Task<object> SyncPayOSOrderCodesAsync(int? orderId = null)
        {
            return await Task.FromResult(new
            {
                success = false,
                message = "PayOS API không hỗ trợ query/list payments theo description hoặc amount. PayOSOrderCode phải được lưu khi tạo payment link. Vui lòng cập nhật thủ công bằng endpoint PUT /api/payment/update-payos-code/{orderId} hoặc SQL.",
                syncedCount = 0,
                notFoundCount = 0,
                failedCount = 0
            });
        }

        /// <summary>
        /// Force update payment status khi PayOS API không kết nối được nhưng đã thanh toán trên PayOS web
        /// </summary>
        public async Task<PaymentStatusResponse> ForceUpdatePaidStatusAsync(int orderId)
        {
            try
            {
                var orderEntity = _orderRepository.GetById(orderId);
                if (orderEntity == null)
                {
                    return new PaymentStatusResponse
                    {
                        Success = false,
                        Message = $"Không tìm thấy đơn hàng với ID {orderId}"
                    };
                }

                if (orderEntity.Status == "Paid" && orderEntity.PaidAt.HasValue)
                {
                    return new PaymentStatusResponse
                    {
                        Success = true,
                        Message = "Đơn hàng đã được thanh toán trước đó",
                        Data = new PaymentStatusData
                        {
                            OrderId = orderId,
                            PayOSOrderCode = orderEntity.PayOSOrderCode,
                            Status = orderEntity.Status,
                            PaidAt = orderEntity.PaidAt,
                            TransactionId = orderEntity.TransactionId
                        }
                    };
                }

                if (!orderEntity.PayOSOrderCode.HasValue)
                {
                    return new PaymentStatusResponse
                    {
                        Success = false,
                        Message = "Đơn hàng không có PayOSOrderCode. Không thể cập nhật."
                    };
                }

                // Gọi stored procedure để update
                var webhookId = $"FORCE_UPDATE_{orderEntity.PayOSOrderCode.Value}_{DateTime.Now:yyyyMMddHHmmss}";
                var result = await Task.Run(() => _orderRepository.ProcessPayOSWebhook(
                    webhookId: webhookId,
                    payOSOrderCode: orderEntity.PayOSOrderCode.Value,
                    code: "00",
                    amount: (int)orderEntity.TotalAmount,
                    reference: $"FORCE_UPDATE_{orderId}",
                    paymentLinkId: null,
                    rawData: $"{{\"source\":\"force_update\",\"orderId\":{orderId},\"reason\":\"PayOS API không kết nối được\"}}",
                    ipAddress: "ForceUpdate",
                    userAgent: "ForceUpdateEndpoint"
                ));

                System.Diagnostics.Debug.WriteLine($"ForceUpdate result: {result.Result}, Message: {result.Message}");

                if (result.Result == "Success")
                {
                    // CRITICAL: Nếu force update Paid thành công, giảm inventory
                    try
                    {
                        System.Diagnostics.Debug.WriteLine($"✅ ForceUpdate order {orderId} to Paid. Decreasing inventory...");
                        _orderService.DecreaseInventoryForPaidOrder(orderId);
                        System.Diagnostics.Debug.WriteLine($"✅ Inventory decreased for order {orderId}");
                    }
                    catch (Exception invEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"⚠️  WARNING: Cannot decrease inventory for order {orderId}: {invEx.Message}");
                    }
                    // Reload order để lấy thông tin mới nhất
                    var updatedOrder = _orderRepository.GetById(orderId);
                    return new PaymentStatusResponse
                    {
                        Success = true,
                        Message = "Đã cập nhật trạng thái thanh toán thành công",
                        Data = new PaymentStatusData
                        {
                            OrderId = orderId,
                            PayOSOrderCode = updatedOrder?.PayOSOrderCode,
                            Status = updatedOrder?.Status ?? "Paid",
                            PaidAt = updatedOrder?.PaidAt,
                            TransactionId = updatedOrder?.TransactionId
                        }
                    };
                }
                else
                {
                    return new PaymentStatusResponse
                    {
                        Success = false,
                        Message = result.Message ?? "Không thể cập nhật trạng thái thanh toán"
                    };
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in ForceUpdatePaidStatusAsync: {ex.Message}");
                return new PaymentStatusResponse
                {
                    Success = false,
                    Message = $"Lỗi khi cập nhật: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Recheck payment status từ PayOS API cho invoice đã có PayOSOrderCode
        /// KHÔNG tạo payment link mới, chỉ check status và update nếu cần
        /// </summary>
        public async Task<PaymentStatusResponse> RecheckPaymentAsync(int orderId)
        {
            try
            {
                // Load invoice từ database
                var orderEntity = _orderRepository.GetById(orderId);
                if (orderEntity == null)
                {
                    System.Diagnostics.Debug.WriteLine($"❌ RecheckPayment: Order {orderId} not found");
                    return new PaymentStatusResponse
                    {
                        Success = false,
                        Message = $"Không tìm thấy đơn hàng với ID {orderId}"
                    };
                }

                // Nếu đã Paid, return ngay
                if (orderEntity.Status == "Paid")
                {
                    System.Diagnostics.Debug.WriteLine($"✅ RecheckPayment: Order {orderId} already Paid, returning status");
                    return new PaymentStatusResponse
                    {
                        Success = true,
                        Message = "Đơn hàng đã được thanh toán",
                        Data = new PaymentStatusData
                        {
                            OrderId = orderId,
                            PayOSOrderCode = orderEntity.PayOSOrderCode,
                            Status = orderEntity.Status,
                            PaidAt = orderEntity.PaidAt,
                            TransactionId = orderEntity.TransactionId
                        }
                    };
                }

                // Nếu PayOSOrderCode IS NULL, return error
                if (!orderEntity.PayOSOrderCode.HasValue || orderEntity.PayOSOrderCode.Value <= 0)
                {
                    System.Diagnostics.Debug.WriteLine($"❌ RecheckPayment: Order {orderId} has no PayOSOrderCode (invalid state)");
                    return new PaymentStatusResponse
                    {
                        Success = false,
                        Message = $"Đơn hàng {orderId} chưa có PayOSOrderCode. Không thể recheck payment status."
                    };
                }

                var payOSOrderCode = orderEntity.PayOSOrderCode.Value;
                System.Diagnostics.Debug.WriteLine($"🔄 RecheckPayment: Checking PayOS status for order {orderId}, PayOSOrderCode {payOSOrderCode}");

                // Gọi PayOS API để check status
                try
                {
                    var payOSStatus = await CheckPayOSPaymentStatusAsync(payOSOrderCode);
                    System.Diagnostics.Debug.WriteLine($"📊 RecheckPayment: PayOS returned status '{payOSStatus}' for order {orderId}");

                    // Nếu PayOS status = PAID, update database
                    if (payOSStatus == "PAID" && orderEntity.Status != "Paid")
                    {
                        System.Diagnostics.Debug.WriteLine($"✅ RecheckPayment: PayOS status is PAID, updating order {orderId} via stored procedure");
                        
                        var webhookId = $"RECHECK_{payOSOrderCode}_{DateTime.Now:yyyyMMddHHmmss}";
                        var webhookResult = await Task.Run(() => _orderRepository.ProcessPayOSWebhook(
                            webhookId: webhookId,
                            payOSOrderCode: payOSOrderCode,
                            code: "00",
                            amount: (int)orderEntity.TotalAmount,
                            reference: null,
                            paymentLinkId: null,
                            rawData: $"{{\"source\":\"recheck\",\"orderId\":{orderId},\"payOSOrderCode\":{payOSOrderCode}}}",
                            ipAddress: "RecheckPayment",
                            userAgent: "PaymentService-Recheck"
                        ));

                        System.Diagnostics.Debug.WriteLine($"📝 RecheckPayment: Stored procedure result: {webhookResult.Result}, Message: {webhookResult.Message}");

                        if (webhookResult.Result == "Success")
                        {
                            System.Diagnostics.Debug.WriteLine($"✅ RecheckPayment: Order {orderId} updated to Paid. Decreasing inventory...");
                            try
                            {
                                _orderService.DecreaseInventoryForPaidOrder(orderId);
                                System.Diagnostics.Debug.WriteLine($"✅ Inventory decreased for order {orderId}");
                            }
                            catch (Exception invEx)
                            {
                                System.Diagnostics.Debug.WriteLine($"⚠️  WARNING: Cannot decrease inventory for order {orderId}: {invEx.Message}");
                            }
                            // Reload entity để lấy status mới
                            orderEntity = _orderRepository.GetById(orderId);
                        }
                    }

                    // Return current status
                    return new PaymentStatusResponse
                    {
                        Success = true,
                        Message = "Recheck payment status thành công",
                        Data = new PaymentStatusData
                        {
                            OrderId = orderId,
                            PayOSOrderCode = orderEntity?.PayOSOrderCode,
                            Status = orderEntity?.Status ?? "Pending",
                            PaidAt = orderEntity?.PaidAt,
                            TransactionId = orderEntity?.TransactionId
                        }
                    };
                }
                catch (InvalidOperationException ex)
                {
                    // Network/DNS error khi gọi PayOS API
                    System.Diagnostics.Debug.WriteLine($"⚠️  RecheckPayment: Cannot connect to PayOS API: {ex.Message}");
                    return new PaymentStatusResponse
                    {
                        Success = false,
                        Message = $"Không thể kết nối đến PayOS API để recheck: {ex.Message}"
                    };
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ RecheckPayment error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                return new PaymentStatusResponse
                {
                    Success = false,
                    Message = $"Lỗi khi recheck payment: {ex.Message}"
                };
            }
        }
    }
}

