using System.Text.Json.Serialization;

namespace GestorFacturas.Domain.Entities.DTOs
{
    public class CustomerDTO
    {
        [JsonPropertyName("customer_run")]
        public string? CustomerRun { get; set; }

        [JsonPropertyName("customer_name")]
        public string? CustomerName { get; set; }

        [JsonPropertyName("customer_email")]
        public string? CustomerEmail { get; set; }
    }
}
