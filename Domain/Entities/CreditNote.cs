using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GestorFacturas.Domain.Entities
{
    public class CreditNote
    {
        [Key]
        [JsonPropertyName("credit_note_id")]
        public int CreditNoteId { get; set; }
        [JsonPropertyName("invoice_number")]
        public int InvoiceNumber { get; set; }
        [JsonPropertyName("credit_note_number")]
        public int CreditNoteNumber { get; set; }
        [JsonPropertyName("credit_note_date")]
        public string CreditNoteDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        [JsonPropertyName("credit_note_amount")]
        public int CreditNoteAmount { get; set; }
        [JsonPropertyName("invoice")]
        public Invoice? Invoice { get; set; }
    }
}
