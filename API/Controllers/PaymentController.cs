using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    /// <summary>
    /// Controller xử lý thanh toán PayOS
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// Tạo payment link từ PayOS
        /// POST /api/payment/create
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<CreatePaymentResponse>> CreatePayment([FromBody] CreatePaymentRequest request)
        {
            if (request == null || request.OrderId <= 0 || request.Amount <= 0)
            {
                return BadRequest(new CreatePaymentResponse
                {
                    Success = false,
                    Message = "Dữ liệu không hợp lệ"
                });
            }

            var result = await _paymentService.CreatePaymentLinkAsync(request);
            
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Kiểm tra trạng thái thanh toán từ database
        /// GET /api/payment/status/{orderId}
        /// </summary>
        [HttpGet("status/{orderId}")]
        public async Task<ActionResult<PaymentStatusResponse>> GetPaymentStatus(int orderId)
        {
            if (orderId <= 0)
            {
                return BadRequest(new PaymentStatusResponse
                {
                    Success = false,
                    Message = "OrderId không hợp lệ"
                });
            }

            try
            {
                var result = await _paymentService.GetPaymentStatusAsync(orderId);
                
                // Log để debug
                System.Diagnostics.Debug.WriteLine($"GetPaymentStatus API - OrderId: {orderId}, Success: {result.Success}, Status: {result.Data?.Status ?? "null"}");
                
                if (!result.Success)
                {
                    return NotFound(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception in GetPaymentStatus API: {ex.Message}");
                return StatusCode(500, new PaymentStatusResponse
                {
                    Success = false,
                    Message = $"Lỗi khi lấy trạng thái: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Nhận webhook từ PayOS khi thanh toán thành công
        /// POST /api/payment/webhook
        /// CRITICAL: Must read raw body BEFORE deserialization for signature verification
        /// </summary>
        [HttpPost("webhook")]
        public async Task<IActionResult> HandleWebhook()
        {
            try
            {
                // CRITICAL STEP 1: Enable buffering to allow reading raw body multiple times
                HttpContext.Request.EnableBuffering();
                
                // CRITICAL STEP 2: Read raw body BEFORE deserialization (required for signature verification)
                using var reader = new StreamReader(
                    HttpContext.Request.Body, 
                    System.Text.Encoding.UTF8, 
                    leaveOpen: true
                );
                var rawBody = await reader.ReadToEndAsync();
                HttpContext.Request.Body.Position = 0; // Reset stream for [FromBody] if needed
                
                // Log webhook receipt
                System.Diagnostics.Debug.WriteLine($"=== WEBHOOK RECEIVED at {DateTime.Now} ===");
                System.Diagnostics.Debug.WriteLine($"Raw body length: {rawBody.Length}");
                System.Diagnostics.Debug.WriteLine($"Raw body preview: {rawBody.Substring(0, Math.Min(200, rawBody.Length))}...");
                
                // CRITICAL STEP 3: Verify signature (HMAC SHA256)
                var signatureHeader = HttpContext.Request.Headers["x-payos-signature"].ToString();
                System.Diagnostics.Debug.WriteLine($"Signature header: {signatureHeader ?? "NULL"}");
                
                if (string.IsNullOrEmpty(signatureHeader))
                {
                    System.Diagnostics.Debug.WriteLine("WARNING: x-payos-signature header missing");
                    // Still process but log warning - some PayOS configurations may not send signature
                    // Return success to prevent PayOS from retrying
                    return Ok(new { code = "01", desc = "Missing signature header" });
                }
                
                // Get checksum key from configuration
                var checksumKey = HttpContext.RequestServices
                    .GetRequiredService<IConfiguration>()["PayOS:ChecksumKey"];
                
                if (string.IsNullOrEmpty(checksumKey))
                {
                    System.Diagnostics.Debug.WriteLine("ERROR: PayOS:ChecksumKey not configured");
                    return Ok(new { code = "01", desc = "Checksum key not configured" });
                }
                
                // Compute expected signature
                var expectedSignature = ComputeHMACSHA256(rawBody, checksumKey);
                System.Diagnostics.Debug.WriteLine($"Expected signature: {expectedSignature}");
                System.Diagnostics.Debug.WriteLine($"Received signature: {signatureHeader}");
                
                // Compare signatures (case-insensitive)
                if (!string.Equals(signatureHeader, expectedSignature, StringComparison.OrdinalIgnoreCase))
                {
                    System.Diagnostics.Debug.WriteLine($"ERROR: Signature mismatch! Webhook may be tampered or ChecksumKey incorrect.");
                    // Return success with error code to prevent PayOS from retrying
                    return Ok(new { code = "01", desc = "Invalid signature" });
                }
                
                System.Diagnostics.Debug.WriteLine("✓ Signature verified successfully");
                
                // CRITICAL STEP 4: Deserialize AFTER signature verification
                PayOSWebhookRequest? webhookRequest;
                try
                {
                    webhookRequest = System.Text.Json.JsonSerializer.Deserialize<PayOSWebhookRequest>(rawBody);
                }
                catch (Exception deserializeEx)
                {
                    System.Diagnostics.Debug.WriteLine($"ERROR: Failed to deserialize webhook: {deserializeEx.Message}");
                    return Ok(new { code = "01", desc = "Invalid payload format" });
                }
                
                if (webhookRequest == null)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR: Webhook request is null after deserialization");
                    return Ok(new { code = "01", desc = "Invalid payload" });
                }

                // Lấy IP address và User Agent từ HttpContext
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();

                // CRITICAL STEP 5: Process webhook ASYNC và return HTTP 200 IMMEDIATELY
                // PayOS requires response within < 5 seconds
                // Process in background to ensure fast response
                var webhookProcessingTask = Task.Run(async () =>
                {
                    try
                    {
                        var success = await _paymentService.HandleWebhookAsync(webhookRequest, ipAddress, userAgent);
                        if (success)
                        {
                            System.Diagnostics.Debug.WriteLine("✓ Webhook processed successfully (background)");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("✗ Webhook processing failed (background)");
                        }
                    }
                    catch (Exception bgEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"ERROR in background webhook processing: {bgEx.Message}");
                        System.Diagnostics.Debug.WriteLine($"Stack trace: {bgEx.StackTrace}");
                    }
                });
                
                // CRITICAL: Return HTTP 200 IMMEDIATELY after signature verification
                // This ensures PayOS gets response within < 5 seconds requirement
                System.Diagnostics.Debug.WriteLine("✓ Webhook accepted, processing in background");
                return Ok(new { code = "00", desc = "Accepted" });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"EXCEPTION in HandleWebhook: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                // Always return 200 to prevent PayOS from retrying
                return Ok(new { code = "01", desc = $"Error: {ex.Message}" });
            }
        }
        
        /// <summary>
        /// Compute HMAC SHA256 signature for webhook verification
        /// </summary>
        private string ComputeHMACSHA256(string data, string key)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA256(System.Text.Encoding.UTF8.GetBytes(key));
            var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(data));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        /// <summary>
        /// Cập nhật PayOSOrderCode cho order (dùng khi PayOSOrderCode bị NULL)
        /// PUT /api/payment/update-payos-code/{orderId}
        /// Body: { "payOSOrderCode": 6169 }
        /// </summary>
        [HttpPut("update-payos-code/{orderId}")]
        public IActionResult UpdatePayOSOrderCode(int orderId, [FromBody] UpdatePayOSCodeRequest request)
        {
            try
            {
                var orderRepo = HttpContext.RequestServices.GetRequiredService<WinFormsFashionShop.Data.Repositories.IOrderRepository>();
                var orderEntity = orderRepo.GetById(orderId);
                
                if (orderEntity == null)
                {
                    return NotFound(new { error = $"Order {orderId} not found" });
                }

                orderEntity.PayOSOrderCode = request.PayOSOrderCode;
                orderRepo.Update(orderEntity);

                return Ok(new { 
                    success = true, 
                    message = $"Updated PayOSOrderCode for order {orderId} to {request.PayOSOrderCode}" 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Sync PayOSOrderCode tự động cho các orders có PayOSOrderCode = NULL
        /// POST /api/payment/sync-payos-codes
        /// </summary>
        [HttpPost("sync-payos-codes")]
        public async Task<IActionResult> SyncPayOSOrderCodes([FromQuery] int? orderId = null)
        {
            try
            {
                var result = await _paymentService.SyncPayOSOrderCodesAsync(orderId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        /// <summary>
        /// Force update payment status khi PayOS API không kết nối được nhưng đã thanh toán trên PayOS web
        /// POST /api/payment/force-update-paid/{orderId}
        /// </summary>
        [HttpPost("force-update-paid/{orderId}")]
        public async Task<IActionResult> ForceUpdatePaidStatus(int orderId)
        {
            try
            {
                var result = await _paymentService.ForceUpdatePaidStatusAsync(orderId);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        /// <summary>
        /// Debug endpoint để kiểm tra trạng thái PayOS trực tiếp với phân tích chi tiết
        /// GET /api/payment/debug/{payOSOrderCode}
        /// </summary>
        [HttpGet("debug/{payOSOrderCode}")]
        public async Task<IActionResult> DebugPayOSStatus(int payOSOrderCode)
        {
            try
            {
                using var httpClient = new System.Net.Http.HttpClient();
                var apiUrl = $"https://api.payos.vn/v2/payment-requests/{payOSOrderCode}";

                var clientId = HttpContext.RequestServices.GetRequiredService<IConfiguration>()["PayOS:ClientId"];
                var apiKey = HttpContext.RequestServices.GetRequiredService<IConfiguration>()["PayOS:ApiKey"];

                if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(apiKey))
                {
                    return BadRequest(new { error = "PayOS chưa được cấu hình" });
                }

                httpClient.DefaultRequestHeaders.Add("x-client-id", clientId);
                httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);
                httpClient.Timeout = TimeSpan.FromSeconds(30);

                System.Net.Http.HttpResponseMessage response;
                string responseContent;
                
                try
                {
                    response = await httpClient.GetAsync(apiUrl);
                    responseContent = await response.Content.ReadAsStringAsync();
                }
                catch (System.Net.Http.HttpRequestException httpEx)
                {
                    return StatusCode(503, new
                    {
                        error = "Không thể kết nối đến PayOS API",
                        details = httpEx.Message,
                        innerException = httpEx.InnerException?.Message,
                        possibleCauses = new[]
                        {
                            "Firewall/Proxy đang chặn kết nối đến api.payos.vn",
                            "PayOS API đang bảo trì",
                            "Lỗi DNS - không thể resolve api.payos.vn",
                            "Mạng internet không ổn định"
                        },
                        suggestion = "Kiểm tra kết nối internet, firewall settings, hoặc thử lại sau. " +
                                   "Nếu đã thanh toán trên PayOS web, có thể update thủ công qua stored procedure."
                    });
                }
                catch (TaskCanceledException timeoutEx)
                {
                    return StatusCode(504, new
                    {
                        error = "Timeout khi gọi PayOS API",
                        details = timeoutEx.Message,
                        suggestion = "PayOS API phản hồi chậm. Vui lòng thử lại sau."
                    });
                }

                // Parse và phân tích chi tiết response
                var analysis = new Dictionary<string, object>();
                try
                {
                    var jsonResponse = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(responseContent);
                    analysis["hasDataProperty"] = jsonResponse.TryGetProperty("data", out var dataElement);
                    
                    if (jsonResponse.TryGetProperty("data", out var data))
                    {
                        var dataFields = new Dictionary<string, object>();
                        
                        // Kiểm tra các field có thể chứa status
                        if (data.TryGetProperty("status", out var statusElement))
                            dataFields["status"] = statusElement.GetString() ?? "";
                        
                        if (data.TryGetProperty("paymentStatus", out var paymentStatusElement))
                            dataFields["paymentStatus"] = paymentStatusElement.GetString() ?? "";
                        
                        if (data.TryGetProperty("transactionStatus", out var transactionStatusElement))
                            dataFields["transactionStatus"] = transactionStatusElement.GetString() ?? "";
                        
                        if (data.TryGetProperty("amount", out var amountElement))
                            dataFields["amount"] = amountElement.GetInt32();
                        
                        if (data.TryGetProperty("amountPaid", out var amountPaidElement))
                            dataFields["amountPaid"] = amountPaidElement.GetInt32();
                        
                        if (data.TryGetProperty("transactions", out var transactionsElement))
                        {
                            if (transactionsElement.ValueKind == System.Text.Json.JsonValueKind.Array && transactionsElement.GetArrayLength() > 0)
                            {
                                var firstTrans = transactionsElement[0];
                                var transFields = new Dictionary<string, object>();
                                if (firstTrans.TryGetProperty("status", out var transStatus))
                                    transFields["status"] = transStatus.GetString() ?? "";
                                if (firstTrans.TryGetProperty("amount", out var transAmount))
                                    transFields["amount"] = transAmount.GetInt32();
                                dataFields["transactions[0]"] = transFields;
                            }
                        }
                        
                        analysis["dataFields"] = dataFields;
                        
                        // Phân tích status
                        var statusAnalysis = new Dictionary<string, object>();
                        var detectedStatus = data.TryGetProperty("status", out var s) ? s.GetString()?.ToUpper() ?? "" : "";
                        statusAnalysis["detectedStatus"] = detectedStatus;
                        statusAnalysis["isPaid"] = detectedStatus == "PAID" || 
                                                   (data.TryGetProperty("amountPaid", out var ap) && 
                                                    data.TryGetProperty("amount", out var a) && 
                                                    ap.GetInt32() > 0 && ap.GetInt32() >= a.GetInt32());
                        analysis["statusAnalysis"] = statusAnalysis;
                    }
                }
                catch (Exception parseEx)
                {
                    analysis["parseError"] = parseEx.Message;
                }

                return Ok(new
                {
                    httpStatus = (int)response.StatusCode,
                    httpStatusText = response.StatusCode.ToString(),
                    rawResponse = responseContent,
                    parsedJson = System.Text.Json.JsonSerializer.Deserialize<object>(responseContent),
                    analysis = analysis,
                    orderCode = payOSOrderCode
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message, stackTrace = ex.StackTrace });
            }
        }
    }

    public class UpdatePayOSCodeRequest
    {
        public int PayOSOrderCode { get; set; }
    }
}

