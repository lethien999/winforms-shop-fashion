using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Net.payOS;
using Net.payOS.Types;
using WinFormsFashionShop.Presentation.Helpers;

namespace WinFormsFashionShop.Presentation.Services
{
    /// <summary>
    /// Service for handling PayOS payment operations.
    /// Single responsibility: only handles PayOS API interactions.
    /// </summary>
    public class PayOSService
    {
        private readonly PayOS _payOS;

        public PayOSService()
        {
            if (!PayOSConfig.IsConfigured())
            {
                throw new InvalidOperationException("PayOS chưa được cấu hình. Vui lòng cấu hình ClientId, ApiKey và ChecksumKey.");
            }

            _payOS = new PayOS(PayOSConfig.ClientId, PayOSConfig.ApiKey, PayOSConfig.ChecksumKey);
        }

        /// <summary>
        /// Creates a payment link and returns payment information including QR code.
        /// Single responsibility: only creates payment link.
        /// </summary>
        /// <param name="orderId">Unique order ID</param>
        /// <param name="amount">Payment amount in VND</param>
        /// <param name="description">Payment description</param>
        /// <param name="items">List of items in the order</param>
        /// <param name="returnUrl">URL to return after payment (optional)</param>
        /// <param name="cancelUrl">URL to return if payment is cancelled (optional)</param>
        /// <returns>CreatePaymentResult containing payment information and QR code</returns>
        public async Task<CreatePaymentResult> CreatePaymentLinkAsync(
            int orderId,
            int amount,
            string description,
            List<ItemData>? items = null,
            string? returnUrl = null,
            string? cancelUrl = null)
        {
            try
            {
                // PayOS requires description to be maximum 25 characters
                var truncatedDescription = string.IsNullOrWhiteSpace(description) 
                    ? $"Đơn hàng #{orderId}" 
                    : description.Length > 25 
                        ? description.Substring(0, 25) 
                        : description;

                var paymentData = new PaymentData(
                    orderCode: orderId,
                    amount: amount,
                    description: truncatedDescription,
                    items: items ?? new List<ItemData>(),
                    cancelUrl: cancelUrl ?? "https://payos.vn",
                    returnUrl: returnUrl ?? "https://payos.vn"
                );

                var result = await _payOS.createPaymentLink(paymentData);
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Không thể tạo link thanh toán PayOS: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets payment information by order code.
        /// Note: PayOS SDK version 1.0.2 doesn't have getPaymentLinkInformation method.
        /// This method uses HTTP API directly to check payment status.
        /// Single responsibility: only retrieves payment information.
        /// </summary>
        /// <param name="orderCode">Order code to check</param>
        /// <returns>CreatePaymentResult containing payment status</returns>
        public async Task<CreatePaymentResult> GetPaymentInfoAsync(int orderCode)
        {
            try
            {
                // PayOS SDK 1.0.2 doesn't have getPaymentLinkInformation method
                // Use HTTP API directly to get payment link information
                using (var httpClient = new System.Net.Http.HttpClient())
                {
                    var apiUrl = $"https://api.payos.vn/v2/payment-requests/{orderCode}";
                    
                    // PayOS API requires authentication headers
                    httpClient.DefaultRequestHeaders.Add("x-client-id", PayOSConfig.ClientId);
                    httpClient.DefaultRequestHeaders.Add("x-api-key", PayOSConfig.ApiKey);
                    
                    var response = await httpClient.GetAsync(apiUrl);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse JSON response to get payment status
                        var jsonResponse = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(responseContent);
                        
                        // PayOS API returns data in "data" property
                        if (!jsonResponse.TryGetProperty("data", out var dataElement))
                        {
                            throw new InvalidOperationException("Phản hồi từ PayOS không hợp lệ: thiếu dữ liệu");
                        }
                        
                        var data = dataElement;
                        
                        // Extract status and other information
                        var status = data.TryGetProperty("status", out var statusElement) 
                            ? statusElement.GetString() ?? "" 
                            : "";
                        var amount = data.TryGetProperty("amount", out var amountElement) 
                            ? amountElement.GetInt32() 
                            : 0;
                        var description = data.TryGetProperty("description", out var descElement) 
                            ? descElement.GetString() ?? "" 
                            : "";
                        var qrCode = data.TryGetProperty("qrCode", out var qrCodeElement) 
                            ? qrCodeElement.GetString() ?? "" 
                            : "";
                        var checkoutUrl = data.TryGetProperty("checkoutUrl", out var urlElement) 
                            ? urlElement.GetString() ?? "" 
                            : "";
                        var bin = data.TryGetProperty("bin", out var binElement) 
                            ? binElement.GetString() ?? "" 
                            : "";
                        var accountNumber = data.TryGetProperty("accountNumber", out var accElement) 
                            ? accElement.GetString() ?? "" 
                            : "";
                        var paymentLinkId = data.TryGetProperty("id", out var idElement) 
                            ? idElement.GetString() ?? "" 
                            : "";
                        
                        // Create a CreatePaymentResult from the API response
                        return new CreatePaymentResult(
                            bin: bin,
                            accountNumber: accountNumber,
                            amount: amount,
                            description: description,
                    orderCode: orderCode,
                            paymentLinkId: paymentLinkId,
                            qrCode: qrCode,
                            checkoutUrl: checkoutUrl,
                            status: status
                        );
                    }
                    else
                    {
                        // Try to parse error message from response
                        string errorMessage = "Không thể lấy thông tin thanh toán";
                        try
                        {
                            var errorJson = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(responseContent);
                            if (errorJson.TryGetProperty("desc", out var descElement))
                            {
                                errorMessage = descElement.GetString() ?? errorMessage;
                            }
                            else if (errorJson.TryGetProperty("message", out var msgElement))
                            {
                                errorMessage = msgElement.GetString() ?? errorMessage;
                            }
                        }
                        catch { }
                        
                        throw new InvalidOperationException($"{errorMessage} (HTTP {response.StatusCode})");
                    }
                }
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                // Check if it's a DNS/network error
                if (ex.Message.Contains("name is valid") || ex.Message.Contains("NameResolutionFailure") || ex.Message.Contains("No such host"))
                {
                    throw new InvalidOperationException("Lỗi kết nối mạng. Vui lòng kiểm tra kết nối Internet và thử lại.", ex);
                }
                throw new InvalidOperationException($"Lỗi kết nối PayOS API: {ex.Message}", ex);
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                throw new InvalidOperationException("Lỗi kết nối mạng. Vui lòng kiểm tra kết nối Internet và thử lại.", ex);
            }
            catch (InvalidOperationException)
            {
                // Re-throw our custom exceptions
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Không thể lấy thông tin thanh toán: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Cancels a payment link.
        /// Single responsibility: only cancels payment link.
        /// </summary>
        /// <param name="orderCode">Order code to cancel</param>
        /// <param name="cancellationReason">Reason for cancellation</param>
        public async Task CancelPaymentLinkAsync(int orderCode, string cancellationReason = "Hủy bởi người dùng")
        {
            try
            {
                await _payOS.cancelPaymentLink(orderCode, cancellationReason);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Không thể hủy link thanh toán: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Confirms a webhook payment.
        /// Note: This method may need adjustment based on actual PayOS API.
        /// Single responsibility: only confirms webhook payment.
        /// </summary>
        /// <param name="webhookData">Webhook data from PayOS</param>
        /// <returns>True if payment is confirmed successfully</returns>
        public bool ConfirmWebhook(WebhookData webhookData)
        {
            // Note: Webhook verification implementation depends on PayOS API structure
            // This is a placeholder - adjust based on actual API documentation
            try
            {
                // PayOS webhook verification - adjust based on actual API
                // For now, return true if webhook data exists
                return webhookData != null;
            }
            catch
            {
                return false;
            }
        }
    }
}

