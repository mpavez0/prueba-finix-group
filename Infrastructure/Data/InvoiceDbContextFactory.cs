using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorFacturas.Infrastructure.Data
{
    internal class InvoiceDbContextFactory : IDesignTimeDbContextFactory<InvoiceDbContext>
    {
        public InvoiceDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InvoiceDbContext>();
            // Configura la cadena de conexión adecuada para tu entorno
            optionsBuilder.UseSqlite("Data Source=Invoices.db");

            return new InvoiceDbContext(optionsBuilder.Options);
        }
    }
}
