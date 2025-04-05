using GestorFacturas.Common.Constants.Enums;
using GestorFacturas.Domain.Entities;
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
        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await _context.Invoices
                 .Include(i => i.Customer)
                .Include(i => i.InvoiceDetails)
                .Include(i => i.InvoicePayment)
                .Include(i => i.InvoiceCreditNotes)
                .ToListAsync();
        }
        public async Task<Invoice> GetByIdAsync(int invoiceNumber)
        {
            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.InvoiceDetails)
                .Include(i => i.InvoicePayment)
                .Include(i => i.InvoiceCreditNotes)
                .FirstOrDefaultAsync(i => i.InvoiceNumber == invoiceNumber);

            if (invoice == null) {
                throw new NullReferenceException();
            }

            return invoice;
        }

        public async Task<List<Invoice>> GetByInvoiceStatus(string invoiceStatus)
        {
            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.InvoiceDetails)
                .Include(i => i.InvoicePayment)
                .Include(i => i.InvoiceCreditNotes)
                .Where(i => i.InvoiceStatus == invoiceStatus)
                .ToListAsync();

            if (invoice == null)
            {
                throw new NullReferenceException();
            }

            return invoice;
        }

        public async Task<List<Invoice>> GetByPaymentStatus(string paymentStatus)
        {
            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.InvoiceDetails)
                .Include(i => i.InvoicePayment)
                .Include(i => i.InvoiceCreditNotes)
                .Where(i => i.PaymentStatus == paymentStatus)
                .ToListAsync();

            if (invoice == null)
            {
                throw new NullReferenceException();
            }

            return invoice;
        }

        public async Task AddAsync(Invoice invoice)
        {

            if (invoice == null){
                throw new NullReferenceException();
            }

            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Invoice invoice)
        {

            if (invoice == null)
            {
                throw new NullReferenceException();
            }

            _context.Invoices.Update(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int invoiceNumber)
        {
            var invoice = await GetByIdAsync(invoiceNumber);
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
                await _context.SaveChangesAsync();
            }
        }

    }
}
