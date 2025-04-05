using GestorFacturas.Domain.Entities;
using GestorFacturas.Domain.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorFacturas.Application.Mappers
{
    public class Mapper
    {
        public InvoiceDTO MapInvoiceToDTO(Invoice invoice)
        {
            var detailDTOList = new List<InvoiceDetailDTO>();
            foreach (InvoiceDetail detail in invoice.InvoiceDetails) {

                var detailDTO = new InvoiceDetailDTO
                {
                    ProductName = detail.ProductName,
                    UnitPrice = detail.UnitPrice,
                    Quantity = detail.Quantity,
                    Subtotal = detail.Subtotal,
                };

                detailDTOList.Add(detailDTO);

            }

            var creditNoteDTOList = new List<CreditNoteDTO>();
            foreach (CreditNote creditNote in invoice.InvoiceCreditNotes)
            {
                var creditNoteDTO = new CreditNoteDTO
                {
                    CreditNoteNumber = creditNote.CreditNoteNumber,
                    CreditNoteAmount = creditNote.CreditNoteAmount,
                };

                creditNoteDTOList.Add(creditNoteDTO);
            }


            var invoiceDTO = new InvoiceDTO
            {
                InvoiceDate = invoice.InvoiceDate,
                Rejected = invoice.Rejected,
                InvoiceStatus = invoice.InvoiceStatus,
                TotalAmount = invoice.TotalAmount,
                DaysToDue = invoice.DaysToDue,
                PaymentDueDate = invoice.PaymentDueDate,
                PaymentStatus = invoice.PaymentStatus,
                Customer = new CustomerDTO
                {
                    CustomerRun = invoice.Customer.CustomerRun,
                    CustomerName = invoice.Customer.CustomerName,
                    CustomerEmail = invoice.Customer.CustomerEmail
                },
                InvoiceDetails = detailDTOList,
                InvoicePayment = new InvoicePaymentDTO
                {
                    PaymentMethod = invoice.InvoicePayment.PaymentMethod,
                    PaymentDate = invoice.InvoicePayment.PaymentDate,
                },
                InvoiceCreditNotes = creditNoteDTOList,

            };

            return invoiceDTO;
        }

        public Invoice MapDTOToInvoice(InvoiceDTO invoiceDTO) {

            var invoice = new Invoice();

            return invoice;

        }

        public CreditNote MapDTOToCreditNote(CreditNoteDTO creditNoteDTO)
        {
            var creditNote = new CreditNote
            {
                CreditNoteNumber = creditNoteDTO.CreditNoteNumber,
                CreditNoteAmount = creditNoteDTO.CreditNoteAmount,
            };

            return creditNote;
        }

    }
}
