using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GestorFacturas.Domain.Entities
{
    public class InvoiceDetail
    {
        [Key]
        [JsonPropertyName("detail_id")]
        public int DetailId { get; set; }

        [JsonPropertyName("invoice_number")]
        public int InvoiceNumber { get; set; }

        [JsonPropertyName("product_name")]
        public string? ProductName { get; set; }

        [JsonPropertyName("unit_price")]
        public int UnitPrice { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("subtotal")]
        public int Subtotal { get; set; }

        [JsonPropertyName("invoice")]
        public Invoice? Invoice { get; set; }
    }
}
