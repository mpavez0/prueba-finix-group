using GestorFacturas.Application.Services.Interfaces;
using GestorFacturas.Domain.Entities.DTOs;
using GestorFacturas.Domain.Mappers;
using GestorFacturas.Infrastructure.Repositories.Interfaces;

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
        public async Task<PagedResponseDTO<InvoiceDTO>> GetAllAsync(int page, int pageSize)
        {
            var pagedInvoices = await _repository.GetAllAsync(page, pageSize);

            var invoiceDTOList = pagedInvoices.Items
                .Select(inv => _mapper.MapInvoiceToDTO(inv))
                .ToList();

            var response = new PagedResponseDTO<InvoiceDTO>
            {
                Items = invoiceDTOList,
                Page = pagedInvoices.Page,
                PageSize = pagedInvoices.PageSize,
                TotalCount = pagedInvoices.TotalCount
            };

            return response;
        }

        public async Task<InvoiceDTO> GetByIdAsync(int invoiceNumber)
        {
            if (invoiceNumber < 0) {
                throw new ArgumentException("El número de la factura no puede ser menor a 0");
            }

            var invoice = await _repository.GetByIdAsync(invoiceNumber);

            if (invoice == null)
            {
                throw new KeyNotFoundException("Factura #{invoiceNumber} no encontrada");
            }

            return _mapper.MapInvoiceToDTO(invoice);
        }
        public async Task<IEnumerable<InvoiceDTO>> GetByInvoiceStatus(string invoiceStatus)
        {

            if (invoiceStatus == "")
            {
                throw new ArgumentException("El estado de la factura no puede estar vacío");
            }

            var invoiceDTOList = new List<InvoiceDTO>();

            var invoiceList = await _repository.GetByInvoiceStatus(invoiceStatus);

            if (invoiceList == null)
            {
                throw new KeyNotFoundException("Facturas con estado #{invoiceStatus} no encontradas");
            }

            foreach (var invoice in invoiceList)
            {
                invoiceDTOList.Add(_mapper.MapInvoiceToDTO(invoice));
            }

            return invoiceDTOList;
        }

        public async Task<IEnumerable<InvoiceDTO>> GetByPaymentStatus(string paymentStatus)
        {
            if (paymentStatus == "")
            {
                throw new ArgumentException("El estado de pago la factura no puede estar vacío");
            }

            var invoiceDTOList = new List<InvoiceDTO>();

            var invoiceList = await _repository.GetByPaymentStatus(paymentStatus);

            if (invoiceList == null)
            {
                throw new KeyNotFoundException("Facturas con estado #{paymentStatus} no encontradas");
            }


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
