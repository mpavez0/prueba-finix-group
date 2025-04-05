using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GestorFacturas.Domain.Entities
{
    public class Invoice
    {
        [Key]
        [JsonPropertyName("invoice_number")]
        public int InvoiceNumber { get; set; }
        [JsonPropertyName("rejected")]
        public bool Rejected { get; set; }
        [JsonPropertyName("invoice_date")]
        public string InvoiceDate { get; set; }
        [JsonPropertyName("invoice_status")]
        public string? InvoiceStatus { get; set; }
        [JsonPropertyName("total_amount")]
        public int TotalAmount { get; set; }
        [JsonPropertyName("days_to_due")]
        public int DaysToDue { get; set; }
        [JsonPropertyName("payment_due_date")]
        public string PaymentDueDate { get; set; }
        [JsonPropertyName("payment_status")]
        public string? PaymentStatus { get; set; }
        [JsonPropertyName("customer_id")]
        public int CustomerId { get; set; }
        [JsonPropertyName("customer")]
        public Customer? Customer { get; set; }
        [JsonPropertyName("invoice_detail")]
        public ICollection<InvoiceDetail>? InvoiceDetails { get; set; } = new List<InvoiceDetail>();
        [JsonPropertyName("invoice_payment")]
        public InvoicePayment? InvoicePayment { get; set; }
        [JsonPropertyName("invoice_credit_note")]
        public ICollection<CreditNote>? InvoiceCreditNotes { get; set; } = new List<CreditNote>();
    }
}
