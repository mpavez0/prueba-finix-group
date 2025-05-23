﻿using GestorFacturas.Application.Services.Interfaces;
using GestorFacturas.Domain.Entities.DTOs;
using GestorFacturas.Domain.Mappers;
using GestorFacturas.Infrastructure.Repositories.Interfaces;

namespace GestorFacturas.Application.Services
{
    public class CreditNoteService : ICreditNoteService
    {
        private readonly ICreditNoteRepository _creditNoteRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly Mapper _mapper;

        public CreditNoteService(ICreditNoteRepository creditNoteRepository, IInvoiceRepository invoiceRepository, Mapper mapper)
        {
            _creditNoteRepository = creditNoteRepository;
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }

        public async Task AddCreditNote(int invoiceNumber, CreditNoteDTO creditNoteDTO)
        {

            if (creditNoteDTO == null)
            {
                throw new ArgumentNullException("La nota de crédito no puede ser nula");
            }

            var invoice = await _invoiceRepository.GetByIdAsync(invoiceNumber);
            if (invoice == null)
            {
                throw new KeyNotFoundException("No se encontró ninguna factura con el número ingresado");
            }

            var creditNote = _mapper.MapDTOToCreditNote(creditNoteDTO);

            var creditNotesAmount = creditNoteDTO.CreditNoteAmount;

            foreach (var cn in invoice.InvoiceCreditNotes)
            {
                creditNotesAmount += cn.CreditNoteAmount;
            }

            var remainingAmount = invoice.TotalAmount - creditNotesAmount;

            if (remainingAmount < 0)
            {
                throw new Exception("Monto de la nota de crédito es mayor al saldo pendiente de la factura");
            }

            await _creditNoteRepository.AddCreditNote(invoice, creditNote);
        }
    }
}
