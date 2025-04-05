using GestorFacturas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace GestorFacturas.Infrastructure.Data
{
    public class DataSeeder : IDataSeeder
    {
        public async Task SeedDataAsync(InvoiceDbContext dbContext, string jsonFilePath)
        {
            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine("No se encontró el archivo JSON.");
                return;
            }

            var jsonString = await File.ReadAllTextAsync(jsonFilePath);

            var invoicesRoot = JsonSerializer.Deserialize<BaseInvoice>(jsonString);
            if (invoicesRoot == null || invoicesRoot.Invoices == null)
            {
                Console.WriteLine("El JSON no tiene el formato esperado.");
                return;
            }

            foreach (var originalInvoice in invoicesRoot.Invoices)
            {

                var customerEntity = await LoadCustomer(dbContext, originalInvoice);

                var fixedInvoice = LoadInvoice(dbContext, originalInvoice, customerEntity);

                var totalDetails = LoadDetails(dbContext, originalInvoice, fixedInvoice);

                var totalCreditNotes = LoadCreditNote(dbContext, originalInvoice, fixedInvoice);

                CalculateInvoiceStatus(fixedInvoice, totalDetails, totalCreditNotes);

                CalculateInvoicePaymentStatus(originalInvoice, fixedInvoice);

                LoadPayment(dbContext, originalInvoice, fixedInvoice);

                dbContext.Invoices.Add(fixedInvoice);

            }

            await dbContext.SaveChangesAsync();
        }

        internal static void CalculateInvoiceStatus(Invoice fixedInvoice, int totalDetails, int totalCreditNotes)
        {
            if (fixedInvoice.TotalAmount != totalDetails)
            {
                fixedInvoice.Rejected = true;
            }

            if ((fixedInvoice.InvoiceCreditNotes?.Count ?? 0) == 0)
            {
                fixedInvoice.InvoiceStatus = "Issued";
            }
            else if (fixedInvoice.TotalAmount == totalCreditNotes)
            {
                fixedInvoice.InvoiceStatus = "Cancelled";
            }
            else
            {
                fixedInvoice.InvoiceStatus = "Partial";
            }
        }
        
        internal static void CalculateInvoicePaymentStatus(Invoice originalInvoice, Invoice fixedInvoice)
        {
            DateTime actualDate = DateTime.Now;
            var paymentDueDate = DateTime.Parse(originalInvoice.PaymentDueDate);

            if (paymentDueDate < actualDate && originalInvoice.InvoicePayment.PaymentDate == null)
            {
                fixedInvoice.PaymentStatus = "Overdue";
            }
            else if (paymentDueDate >= actualDate && originalInvoice.InvoicePayment.PaymentDate == null)
            {
                fixedInvoice.PaymentStatus = "Pending";
            }
            else if (originalInvoice.InvoicePayment.PaymentDate != null)
            {
                fixedInvoice.PaymentStatus = "Paid";
            }
            else
            {
                fixedInvoice.PaymentStatus = "Error";
                throw new Exception("Error al asignar un estado de pago a la factura");
            }
        }

        internal static Invoice LoadInvoice(InvoiceDbContext dbContext, Invoice? invoiceDto, Customer customerEntity)
        {
            var invoiceEntity = new Invoice
            {
                InvoiceNumber = invoiceDto.InvoiceNumber,
                InvoiceDate = invoiceDto.InvoiceDate,
                InvoiceStatus = char.ToUpper(invoiceDto.InvoiceStatus[0]) + invoiceDto.InvoiceStatus.Substring(1).ToLower(),
                TotalAmount = invoiceDto.TotalAmount,
                DaysToDue = invoiceDto.DaysToDue,
                PaymentDueDate = invoiceDto.PaymentDueDate,
                PaymentStatus = invoiceDto.PaymentStatus,
                Customer = customerEntity
            };

            return invoiceEntity;
        }

        internal static async Task<Customer> LoadCustomer(InvoiceDbContext dbContext, Invoice? invoiceDto)
        {
            var customerEntity = new Customer
            {
                CustomerRun = invoiceDto.Customer.CustomerRun,
                CustomerName = invoiceDto.Customer.CustomerName,
                CustomerEmail = invoiceDto.Customer.CustomerEmail
            };

            var existingCustomer = await dbContext.Customers
                .FirstOrDefaultAsync(c => c.CustomerRun == customerEntity.CustomerRun);
            if (existingCustomer == null)
            {
                dbContext.Customers.Add(customerEntity);
            }
            else
            {
                customerEntity = existingCustomer;
            }

            return customerEntity;

        }

        internal static int LoadDetails(InvoiceDbContext dbContext, Invoice? invoiceDto, Invoice invoiceEntity)
        {

            var totalDetails = 0;

            if (invoiceDto.InvoiceDetails != null)
            {
                foreach (var detailDto in invoiceDto.InvoiceDetails)
                {
                    var detailEntity = new InvoiceDetail
                    {
                        ProductName = detailDto.ProductName,
                        UnitPrice = detailDto.UnitPrice,
                        Quantity = detailDto.Quantity,
                        Subtotal = detailDto.Subtotal,
                        InvoiceNumber = invoiceEntity.InvoiceNumber,
                        Invoice = invoiceEntity
                    };

                    totalDetails += detailDto.Subtotal;

                    dbContext.InvoiceDetails.Add(detailEntity);
                }
            }

            return totalDetails;
        }

        internal static void LoadPayment(InvoiceDbContext dbContext, Invoice? invoiceDto, Invoice invoiceEntity)
        {
            if (invoiceDto.InvoicePayment != null)
            {
                var paymentEntity = new InvoicePayment
                {
                    InvoiceNumber = invoiceEntity.InvoiceNumber,
                    PaymentMethod = invoiceDto.InvoicePayment.PaymentMethod,
                    PaymentDate = invoiceDto.InvoicePayment.PaymentDate,
                    Invoice = invoiceEntity
                };
                dbContext.InvoicePayments.Add(paymentEntity);
            }
        }

        internal static int LoadCreditNote(InvoiceDbContext dbContext, Invoice? invoiceDto, Invoice invoiceEntity)
        {

            var totalCreditNotes = 0;

            if (invoiceDto.InvoiceCreditNotes != null)
            {
                foreach (var creditDto in invoiceDto.InvoiceCreditNotes)
                {
                    var creditEntity = new CreditNote
                    {
                        InvoiceNumber = invoiceEntity.InvoiceNumber,
                        CreditNoteNumber = creditDto.CreditNoteNumber,
                        CreditNoteDate = creditDto.CreditNoteDate,
                        CreditNoteAmount = creditDto.CreditNoteAmount,
                        Invoice = invoiceEntity
                    };

                    totalCreditNotes += creditDto.CreditNoteAmount;

                    dbContext.InvoiceCreditNotes.Add(creditEntity);
                }
            }

            return totalCreditNotes;
        }
    }
}