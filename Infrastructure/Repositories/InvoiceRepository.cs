using GestorFacturas.Domain.Entities;
using GestorFacturas.Domain.Entities.DTOs;
using GestorFacturas.Infrastructure.Data;
using GestorFacturas.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorFacturas.Infrastructure.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly InvoiceDbContext _context;
        public InvoiceRepository(InvoiceDbContext context)
        {
            _context = context;
        }
        public async Task<PagedResponseDTO<Invoice>> GetAllAsync(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var query = _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.InvoiceDetails)
                .Include(i => i.InvoicePayment)
                .Include(i => i.InvoiceCreditNotes)
                .AsNoTracking();

            if (query.Count() <= 0) {
                throw new Exception("Error interno, no se han encontrado facturas en la base de datos");
            }

            var totalCount = await query.CountAsync();

            var skip = (page - 1) * pageSize;
            var items = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponseDTO<Invoice>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }
        public async Task<Invoice> GetByIdAsync(int invoiceNumber)
        {

            if (invoiceNumber == 0)
            {
                throw new ArgumentException("El número de la factura #{invoiceNumber} no puede ser 0");
            }

            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.InvoiceDetails)
                .Include(i => i.InvoicePayment)
                .Include(i => i.InvoiceCreditNotes)
                .FirstOrDefaultAsync(i => i.InvoiceNumber == invoiceNumber);

            if (invoice == null) {
                throw new KeyNotFoundException("No se encontró ningun factura con el número ingresado");
            }

            return invoice;
        }

        public async Task<List<Invoice>> GetByInvoiceStatus(string invoiceStatus)
        {

            if (invoiceStatus == "")
            {
                throw new ArgumentException("El estado de la factura #{invoiceStatus} no puede estar vacío");
            }

            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.InvoiceDetails)
                .Include(i => i.InvoicePayment)
                .Include(i => i.InvoiceCreditNotes)
                .Where(i => i.InvoiceStatus == invoiceStatus)
                .ToListAsync();

            if (invoice == null)
            {
                throw new KeyNotFoundException("No se encontró ningun factura con el estado ingresado");
            }

            return invoice;
        }

        public async Task<List<Invoice>> GetByPaymentStatus(string paymentStatus)
        {

            if (paymentStatus == "")
            {
                throw new ArgumentException("El estado de pago de la factura #{paymentStatus} no puede estar vacío");
            }

            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.InvoiceDetails)
                .Include(i => i.InvoicePayment)
                .Include(i => i.InvoiceCreditNotes)
                .Where(i => i.PaymentStatus == paymentStatus)
                .ToListAsync();

            if (invoice == null)
            {
                throw new KeyNotFoundException("No se encontró ningun factura con el estado de pago ingresado");
            }

            return invoice;
        }

        public async Task AddAsync(Invoice invoice)
        {

            if (invoice == null){
                throw new ArgumentException("Los datos de la factura no pueden estar vacíos");
            }

            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
        }
        public Task UpdateAsync(Invoice invoice)
        {

            throw new NotImplementedException();
        }

        public Task DeleteAsync(int invoiceNumber)
        {
            throw new NotImplementedException();
        }

    }
}
