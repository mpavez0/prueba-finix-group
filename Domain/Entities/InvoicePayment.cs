using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorFacturas.Domain.Entities
{
    public class InvoicePayment
    {
        // Relación 1:1 (InvoiceNumber como clave primaria)
        public int InvoiceNumber { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime? PaymentDate { get; set; }

        public Invoice Invoice { get; set; }
    }
}
