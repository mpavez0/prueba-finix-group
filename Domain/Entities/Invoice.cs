using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorFacturas.Domain.Entities
{
    public class Invoice
    {
        public int InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceStatus { get; set; }
        public int TotalAmount { get; set; }
        public int DaysToDue { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public string PaymentStatus { get; set; }

        // Clave foránea
        public string CustomerRun { get; set; }
        public Customer Customer { get; set; }

        // Relaciones
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public InvoicePayment InvoicePayment { get; set; }
        public ICollection<InvoiceCreditNote> InvoiceCreditNotes { get; set; }
    }
}
