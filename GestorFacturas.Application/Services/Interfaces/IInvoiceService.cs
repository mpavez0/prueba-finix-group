using GestorFacturas.Common.Constants.Enums;
using GestorFacturas.Domain.Entities;
using GestorFacturas.Domain.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorFacturas.Application.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceDTO>> GetAllAsync();
        Task<InvoiceDTO> GetByIdAsync(int invoiceNumber);
        Task<IEnumerable<InvoiceDTO>> GetByInvoiceStatus(string paymentStatus);
        Task<IEnumerable<InvoiceDTO>> GetByPaymentStatus(string invoiceStatus);
        Task UpdateAsync(InvoiceDTO invoice);
        Task DeleteAsync(int invoiceNumber);
    }
}
