using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GestorFacturas.Domain.Entities
{
    public class BaseInvoice
    {
        [JsonPropertyName("invoices")]
        public List<Invoice> Invoices { get; set; }
    }
}
