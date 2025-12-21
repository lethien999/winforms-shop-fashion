using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WinFormsFashionShop.Presentation.Models;
using System.Net.Http.Headers;

namespace WinFormsFashionShop.Presentation.Services
{
    /// <summary>
    /// Client để gọi Backend API cho thanh toán PayOS
    /// </summary>
    public class PaymentApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public PaymentApiClient(string baseUrl = "https://localhost:7000") // Default API URL
        {
            _baseUrl = baseUrl.TrimEnd('/');
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl),
                Timeout = TimeSpan.FromSeconds(30)
            };
        }

        /// <summary>
        /// Tạo payment link từ Backend API
        /// </summary>
        public async Task<CreatePaymentResponse> CreatePaymentAsync(CreatePaymentRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/payment/create", request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<CreatePaymentResponse>() 
                    ?? throw new InvalidOperationException("Không thể parse response");
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException($"Lỗi kết nối API: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Kiểm tra trạng thái thanh toán từ Backend API
        /// </summary>
        public async Task<PaymentStatusResponse> GetPaymentStatusAsync(int orderId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/payment/status/{orderId}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<PaymentStatusResponse>() 
                    ?? throw new InvalidOperationException("Không thể parse response");
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException($"Lỗi kết nối API: {ex.Message}", ex);
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}

