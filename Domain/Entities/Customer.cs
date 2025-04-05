using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GestorFacturas.Domain.Entities
{
    public class Customer
    {
        [Key]
        [JsonPropertyName("customer_id")]
        public int CustomerId { get; set; }
        [JsonPropertyName("customer_run")]
        public string? CustomerRun { get; set; }
        [JsonPropertyName("customer_name")]
        public string? CustomerName { get; set; }
        [JsonPropertyName("customer_email")]
        public string? CustomerEmail { get; set; }
        [JsonPropertyName("invoice")]
        public ICollection<Invoice>? Invoices { get; set; }
    }
}
