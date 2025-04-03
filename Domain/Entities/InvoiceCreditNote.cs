using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorFacturas.Domain.Entities
{
    internal class InvoiceCreditNote
    {
        public int CreditNoteNumber { get; set; }
        public DateTime CreditNoteDate { get; set; }
        public decimal CreditNoteAmount { get; set; }
    }
}
