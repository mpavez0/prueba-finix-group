using System.Text.Json.Serialization;

namespace GestorFacturas.Domain.Entities.DTOs
{
    public class InvoiceDetailDTO
    {

        [JsonPropertyName("product_name")]
        public string ProductName { get; set; }

        [JsonPropertyName("unit_price")]
        public int UnitPrice { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("subtotal")]
        public int Subtotal { get; set; }
    }
}
