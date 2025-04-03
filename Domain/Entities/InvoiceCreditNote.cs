using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorFacturas.Domain.Entities
{
    public class InvoiceCreditNote
    {
        public int CreditNoteId { get; set; }
        public int InvoiceNumber { get; set; }
        public int CreditNoteNumber { get; set; }
        public DateTime CreditNoteDate { get; set; }
        public int CreditNoteAmount { get; set; }

        public Invoice Invoice { get; set; }
    }
}
