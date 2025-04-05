using GestorFacturas.Common.Constants.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GestorFacturas.Domain.Entities
{
    public class InvoicePayment
    {
        [Key]
        [JsonPropertyName("invoice_number")]
        public int InvoiceNumber { get; set; }

        [JsonPropertyName("payment_method")]
        public string? PaymentMethod { get; set; }

        [JsonPropertyName("payment_date")]
        public DateTime? PaymentDate { get; set; }

        [JsonPropertyName("invoice")]
        public Invoice? Invoice { get; set; }
    }
}
