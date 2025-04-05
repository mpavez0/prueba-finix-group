using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorFacturas.Infrastructure.Data
{
    public interface IDataSeeder
    {
        Task SeedDataAsync(InvoiceDbContext dbContext, string jsonFilePath);
    }
}
