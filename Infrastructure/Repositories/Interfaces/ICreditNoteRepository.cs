using GestorFacturas.Domain.Entities;
using GestorFacturas.Domain.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorFacturas.Infrastructure.Repositories.Interfaces
{
    public interface ICreditNoteRepository
    {
        Task AddCreditNote(Invoice invoice, CreditNote creditNote);

        // Otros métodos para gestión de notas de crédito
    }
}
