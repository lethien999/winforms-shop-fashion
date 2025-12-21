using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WinFormsFashionShop.Presentation.Models;
using System.Net;

namespace WinFormsFashionShop.Presentation.Services
{
    /// <summary>
    /// PaymentApiClient với retry logic và offline detection cho môi trường POS
    /// </summary>
    public class PaymentApiClientWithRetry : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private const int MaxRetries = 3;
        private const int BaseDelayMs = 1000; // 1 giây

        public PaymentApiClientWithRetry(string baseUrl = "https://localhost:7000")
        {
            _baseUrl = baseUrl.TrimEnd('/');
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl),
                Timeout = TimeSpan.FromSeconds(10) // Timeout ngắn hơn để phát hiện mất mạng nhanh
            };
        }

        /// <summary>
        /// Kiểm tra kết nối mạng
        /// </summary>
        public bool IsOnline()
        {
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Head, "/api/payment/status/1");
                var response = _httpClient.Send(request, HttpCompletionOption.ResponseHeadersRead);
                return response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NotFound;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Tạo payment link với retry logic
        /// </summary>
        public async Task<CreatePaymentResponse> CreatePaymentAsync(CreatePaymentRequest request)
        {
            return await ExecuteWithRetryAsync(async () =>
            {
                var response = await _httpClient.PostAsJsonAsync("/api/payment/create", request);
                
                // Nếu là lỗi HTTP 4xx (client error), không retry và đọc error message từ response
                if ((int)response.StatusCode >= 400 && (int)response.StatusCode < 500)
                {
                    string errorMessage = $"Lỗi từ API (HTTP {(int)response.StatusCode})";
                    try
                    {
                        var errorResponse = await response.Content.ReadFromJsonAsync<CreatePaymentResponse>();
                        if (errorResponse != null && !string.IsNullOrEmpty(errorResponse.Message))
                        {
                            errorMessage = errorResponse.Message;
                        }
                    }
                    catch
                    {
                        // Nếu không parse được JSON, đọc raw content
                        try
                        {
                            var errorContent = await response.Content.ReadAsStringAsync();
                            if (!string.IsNullOrWhiteSpace(errorContent) && errorContent.Length < 500)
                            {
                                errorMessage = $"Lỗi từ API (HTTP {(int)response.StatusCode}): {errorContent}";
                            }
                        }
                        catch
                        {
                            // Nếu không đọc được content, dùng message mặc định
                        }
                    }
                    throw new InvalidOperationException(errorMessage);
                }
                
                // Nếu thành công (2xx), parse response
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<CreatePaymentResponse>() 
                    ?? throw new InvalidOperationException("Không thể parse response");
            });
        }

        /// <summary>
        /// Kiểm tra trạng thái thanh toán với retry logic
        /// </summary>
        public async Task<PaymentStatusResponse> GetPaymentStatusAsync(int orderId)
        {
            return await ExecuteWithRetryAsync(async () =>
            {
                var response = await _httpClient.GetAsync($"/api/payment/status/{orderId}");
                
                // Nếu 404 (order không tồn tại), không retry
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return new PaymentStatusResponse
                    {
                        Success = false,
                        Message = "Không tìm thấy đơn hàng"
                    };
                }

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<PaymentStatusResponse>() 
                    ?? throw new InvalidOperationException("Không thể parse response");
            });
        }

        /// <summary>
        /// Execute với exponential backoff retry
        /// Chỉ retry cho lỗi mạng (connection refused, timeout, DNS), không retry cho HTTP errors (4xx, 5xx)
        /// </summary>
        private async Task<T> ExecuteWithRetryAsync<T>(Func<Task<T>> operation)
        {
            Exception? lastException = null;

            for (int attempt = 0; attempt <= MaxRetries; attempt++)
            {
                try
                {
                    return await operation();
                }
                catch (InvalidOperationException)
                {
                    // InvalidOperationException thường là lỗi business logic hoặc HTTP 4xx
                    // Không retry cho những lỗi này
                    throw;
                }
                catch (HttpRequestException ex)
                {
                    lastException = ex;
                    
                    // Chỉ retry cho lỗi mạng thực sự (connection refused, DNS error, etc.)
                    // Không retry cho HTTP errors (4xx, 5xx) vì chúng được xử lý trong operation
                    if (attempt < MaxRetries && IsNetworkError(ex))
                    {
                        var delay = BaseDelayMs * (int)Math.Pow(2, attempt); // Exponential backoff: 1s, 2s, 4s
                        await Task.Delay(delay);
                        continue;
                    }
                    
                    // Không phải lỗi mạng hoặc đã hết số lần retry
                    throw new InvalidOperationException($"Lỗi kết nối API{(attempt > 0 ? $" (đã thử {attempt + 1} lần)" : "")}: {ex.Message}", ex);
                }
                catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
                {
                    lastException = ex;
                    
                    // Timeout có thể do mạng chậm, nên retry
                    if (attempt < MaxRetries)
                    {
                        var delay = BaseDelayMs * (int)Math.Pow(2, attempt);
                        await Task.Delay(delay);
                        continue;
                    }
                    
                    throw new InvalidOperationException($"Timeout kết nối API{(attempt > 0 ? $" (đã thử {attempt + 1} lần)" : "")}", ex);
                }
            }

            throw lastException ?? new InvalidOperationException("Lỗi không xác định");
        }

        /// <summary>
        /// Kiểm tra xem exception có phải lỗi mạng không
        /// </summary>
        private bool IsNetworkError(HttpRequestException ex)
        {
            var message = ex.Message.ToLower();
            return message.Contains("network") ||
                   message.Contains("connection") ||
                   message.Contains("timeout") ||
                   message.Contains("refused") ||
                   message.Contains("name resolution") ||
                   message.Contains("no such host");
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}

