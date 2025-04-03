using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorFacturas.Domain.Entities
{
    internal class Invoice
    {
        public int InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public required string InvoiceStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public int DaysToDue { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public required string PaymentStatus { get; set; }
        public List<InvoiceDetail> InvoiceDetail { get; set; } = new List<InvoiceDetail>();
        public required InvoicePayment InvoicePayment { get; set; }
        public List<InvoiceCreditNote> InvoiceCreditNote { get; set; } = new List<InvoiceCreditNote>();
        public required Customer Customer { get; set; }
    }
}
