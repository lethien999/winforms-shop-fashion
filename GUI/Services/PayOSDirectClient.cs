using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WinFormsFashionShop.Presentation.Services
{
    /// <summary>
    /// Client để gọi trực tiếp PayOS API từ GUI (không qua backend)
    /// Sử dụng cho Hybrid Polling - hoạt động độc lập không cần webhook
    /// </summary>
    public class PayOSDirectClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _clientId;
        private readonly string _apiKey;

        public PayOSDirectClient(string clientId, string apiKey)
        {
            _clientId = clientId;
            _apiKey = apiKey;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api-merchant.payos.vn"),
                Timeout = TimeSpan.FromSeconds(15)
            };
            _httpClient.DefaultRequestHeaders.Add("x-client-id", _clientId);
            _httpClient.DefaultRequestHeaders.Add("x-api-key", _apiKey);
        }

        /// <summary>
        /// Kiểm tra trạng thái thanh toán trực tiếp từ PayOS API
        /// </summary>
        /// <param name="payOSOrderCode">Mã đơn hàng PayOS</param>
        /// <returns>Trạng thái: PENDING, PAID, CANCELLED, EXPIRED</returns>
        public async Task<PayOSPaymentStatus> CheckPaymentStatusAsync(long payOSOrderCode)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/v2/payment-requests/{payOSOrderCode}");
                var content = await response.Content.ReadAsStringAsync();

                System.Diagnostics.Debug.WriteLine($"[PayOSDirectClient] Check status for {payOSOrderCode}: {response.StatusCode}");

                if (!response.IsSuccessStatusCode)
                {
                    System.Diagnostics.Debug.WriteLine($"[PayOSDirectClient] Error response: {content}");
                    return new PayOSPaymentStatus
                    {
                        Success = false,
                        Status = "ERROR",
                        Message = $"PayOS API error: {response.StatusCode}"
                    };
                }

                var json = JsonSerializer.Deserialize<JsonElement>(content);
                
                if (json.TryGetProperty("data", out var data))
                {
                    var status = data.TryGetProperty("status", out var statusElement) 
                        ? statusElement.GetString() ?? "UNKNOWN" 
                        : "UNKNOWN";

                    var amount = data.TryGetProperty("amount", out var amountElement) 
                        ? amountElement.GetInt64() 
                        : 0;

                    var amountPaid = data.TryGetProperty("amountPaid", out var amountPaidElement) 
                        ? amountPaidElement.GetInt64() 
                        : 0;

                    System.Diagnostics.Debug.WriteLine($"[PayOSDirectClient] Status: {status}, Amount: {amount}, AmountPaid: {amountPaid}");

                    return new PayOSPaymentStatus
                    {
                        Success = true,
                        Status = status.ToUpper(),
                        Amount = amount,
                        AmountPaid = amountPaid,
                        Message = "OK"
                    };
                }

                return new PayOSPaymentStatus
                {
                    Success = false,
                    Status = "ERROR",
                    Message = "Invalid response format"
                };
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine($"[PayOSDirectClient] Network error: {ex.Message}");
                return new PayOSPaymentStatus
                {
                    Success = false,
                    Status = "NETWORK_ERROR",
                    Message = $"Lỗi kết nối: {ex.Message}"
                };
            }
            catch (TaskCanceledException)
            {
                System.Diagnostics.Debug.WriteLine($"[PayOSDirectClient] Timeout");
                return new PayOSPaymentStatus
                {
                    Success = false,
                    Status = "TIMEOUT",
                    Message = "Request timeout"
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[PayOSDirectClient] Error: {ex.Message}");
                return new PayOSPaymentStatus
                {
                    Success = false,
                    Status = "ERROR",
                    Message = ex.Message
                };
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }

    /// <summary>
    /// Kết quả kiểm tra trạng thái thanh toán từ PayOS
    /// </summary>
    public class PayOSPaymentStatus
    {
        public bool Success { get; set; }
        public string Status { get; set; } = "UNKNOWN";
        public long Amount { get; set; }
        public long AmountPaid { get; set; }
        public string? Message { get; set; }

        public bool IsPaid => Status == "PAID";
        public bool IsPending => Status == "PENDING";
        public bool IsCancelled => Status == "CANCELLED";
        public bool IsExpired => Status == "EXPIRED";
    }
}
