using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorFacturas.Domain.Entities
{
    internal class InvoicePayment
    {
        public string PaymentMethod { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
