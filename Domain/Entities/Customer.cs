using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorFacturas.Domain.Entities
{
    public class Customer
    {
        public string CustomerRun { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        // Relación 1:N con Invoice
        public ICollection<Invoice> Invoices { get; set; }
    }
}
