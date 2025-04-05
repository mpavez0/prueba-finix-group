using GestorFacturas.Domain.Entities;
using GestorFacturas.Domain.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorFacturas.Infrastructure.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<PagedResponseDTO<Invoice>> GetAllAsync(int page, int pageSize);
        Task<Invoice> GetByIdAsync(int invoiceNumber);
        Task<List<Invoice>> GetByInvoiceStatus(string paymentStatus);
        Task<List<Invoice>> GetByPaymentStatus(string invoiceStatus);
        Task UpdateAsync(Invoice invoice);
        Task DeleteAsync(int invoiceNumber);
    }
}
