using GestorFacturas.Application.Services.Interfaces;
using GestorFacturas.Common.Constants.Enums;
using GestorFacturas.Domain.Entities;
using GestorFacturas.Domain.Entities.DTOs;
using GestorFacturas.Domain.Mappers;
using GestorFacturas.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorFacturas.Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _repository;
        private readonly Mapper _mapper;

        public InvoiceService(IInvoiceRepository repository, Mapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<InvoiceDTO>> GetAllAsync()
        {
            var invoiceDTOList = new List<InvoiceDTO>();
            
            var invoicesList = await _repository.GetAllAsync();

            foreach (var invoice in invoicesList)
            {
                invoiceDTOList.Add(_mapper.MapInvoiceToDTO(invoice));
            }

            return invoiceDTOList;
        }

        public async Task<InvoiceDTO> GetByIdAsync(int invoiceNumber)
        {
            var invoice = await _repository.GetByIdAsync(invoiceNumber);

            return _mapper.MapInvoiceToDTO(invoice);
        }
        public async Task<IEnumerable<InvoiceDTO>> GetByInvoiceStatus(string invoiceStatus)
        {
            var invoiceDTOList = new List<InvoiceDTO>();

            var invoiceList = await _repository.GetByInvoiceStatus(invoiceStatus);

            foreach (var invoice in invoiceList)
            {
                invoiceDTOList.Add(_mapper.MapInvoiceToDTO(invoice));
            }

            return invoiceDTOList;
        }

        public async Task<IEnumerable<InvoiceDTO>> GetByPaymentStatus(string paymentStatus)
        {
            var invoiceDTOList = new List<InvoiceDTO>();

            var invoiceList = await _repository.GetByPaymentStatus(paymentStatus);

            foreach (var invoice in invoiceList) {
                invoiceDTOList.Add(_mapper.MapInvoiceToDTO(invoice));
            }

            return invoiceDTOList;
        }


        public Task UpdateAsync(InvoiceDTO invoice)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int invoiceNumber)
        {
            throw new NotImplementedException();
        }
    }
}
