using GestorFacturas.Common.Constants.Enums;
using GestorFacturas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorFacturas.Infrastructure.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<IEnumerable<Invoice>> GetAllAsync();
        Task<Invoice> GetByIdAsync(int invoiceNumber);
        Task<List<Invoice>> GetByInvoiceStatus(string paymentStatus);
        Task<List<Invoice>> GetByPaymentStatus(string invoiceStatus);
        Task UpdateAsync(Invoice invoice);
        Task DeleteAsync(int invoiceNumber);
    }
}
