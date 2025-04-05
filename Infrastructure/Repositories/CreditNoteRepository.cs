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
    public class CreditNoteRepository : ICreditNoteRepository
    {
        private readonly InvoiceDbContext _context;

        public CreditNoteRepository(InvoiceDbContext context)
        {
            _context = context;
        }
        public async Task AddCreditNote(Invoice invoice, CreditNote creditNote)
        {
            if (invoice == null)
            {
                throw new NullReferenceException("La factura no puede estar vacía");
            }

            if (creditNote == null) {
                throw new NullReferenceException("La nota de crédito no puede estar vacía");
            }

            invoice.InvoiceCreditNotes.Add(creditNote);

            await _context.SaveChangesAsync();
        }
    }
}
