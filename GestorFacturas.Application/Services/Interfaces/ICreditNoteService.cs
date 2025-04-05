using GestorFacturas.Domain.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorFacturas.Application.Services.Interfaces
{
    public interface ICreditNoteService
    {
        Task AddCreditNote(int invoiceNumber, CreditNoteDTO creditNoteDTO);

        // Otros métodos para gestión de notas de crédito
    }
}
