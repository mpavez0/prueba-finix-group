using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestorFacturas.Domain.Entities.DTOs
{
    public class InvoicePaymentDTO
    {
        [JsonPropertyName("payment_method")]
        public string? PaymentMethod { get; set; }

        [JsonPropertyName("payment_date")]
        public string? PaymentDate { get; set; }
    }
}
