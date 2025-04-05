using System.Text.Json.Serialization;

namespace GestorFacturas.Domain.Entities.DTOs
{
    public class CreditNoteDTO
    {
        [JsonPropertyName("credit_note_number")]
        public int CreditNoteNumber { get; set; }

        [JsonPropertyName("credit_note_amount")]
        public int CreditNoteAmount { get; set; }

    }
}
