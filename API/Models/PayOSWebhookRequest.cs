using System.Text.Json.Serialization;

namespace API.Models
{
    /// <summary>
    /// Webhook request model từ PayOS
    /// </summary>
    public class PayOSWebhookRequest
    {
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty; // Mã trạng thái (00 = thành công)

        [JsonPropertyName("desc")]
        public string Desc { get; set; } = string.Empty; // Mô tả

        [JsonPropertyName("data")]
        public WebhookData? Data { get; set; }
    }

    public class WebhookData
    {
        [JsonPropertyName("orderCode")]
        public int OrderCode { get; set; } // PayOS order code

        [JsonPropertyName("amount")]
        public int Amount { get; set; } // Số tiền

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("accountNumber")]
        public string? AccountNumber { get; set; }

        [JsonPropertyName("reference")]
        public string? Reference { get; set; }

        [JsonPropertyName("transactionDateTime")]
        public string? TransactionDateTime { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("paymentLinkId")]
        public string? PaymentLinkId { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty; // Mã trạng thái

        [JsonPropertyName("desc")]
        public string Desc { get; set; } = string.Empty; // Mô tả

        [JsonPropertyName("counterAccountBankId")]
        public string? CounterAccountBankId { get; set; }

        [JsonPropertyName("counterAccountBankName")]
        public string? CounterAccountBankName { get; set; }

        [JsonPropertyName("counterAccountName")]
        public string? CounterAccountName { get; set; }

        [JsonPropertyName("counterAccountNumber")]
        public string? CounterAccountNumber { get; set; }

        [JsonPropertyName("virtualAccountName")]
        public string? VirtualAccountName { get; set; }

        [JsonPropertyName("virtualAccountNumber")]
        public string? VirtualAccountNumber { get; set; }
    }
}

