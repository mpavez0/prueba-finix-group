using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorFacturas.Domain.Entities
{
    public class InvoiceDetail
    {
        public int DetailId { get; set; }
        public int InvoiceNumber { get; set; }
        public string ProductName { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int Subtotal { get; set; }

        public Invoice Invoice { get; set; }
    }
}
