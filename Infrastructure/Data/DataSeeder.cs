using GestorFacturas.Domain.Entities;
using GestorFacturas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var invoicesRoot = JsonSerializer.Deserialize<BaseInvoice>(jsonString, options);
            if (invoicesRoot == null || invoicesRoot.Invoices == null)
            {
                Console.WriteLine("El JSON no tiene el formato esperado.");
                return;
            }

            foreach (var invoiceDto in invoicesRoot.Invoices)
            {

                var customerEntity = await LoadCustomer(dbContext, invoiceDto);

                var invoiceEntity = LoadInvoice(dbContext, invoiceDto, customerEntity);

                var totalDetails = LoadDetails(dbContext, invoiceDto, invoiceEntity);

                var totalCreditNotes = LoadCreditNote(dbContext, invoiceDto, invoiceEntity);

                if (invoiceEntity.TotalAmount != totalDetails)
                {
                    invoiceEntity.Rejected = true;
                }

                if ((invoiceEntity.InvoiceCreditNotes?.Count ?? 0) == 0)
                {
                    invoiceEntity.InvoiceStatus = "Issued";
                }
                else if (invoiceEntity.TotalAmount == totalCreditNotes)
                {
                    invoiceEntity.InvoiceStatus = "Cancelled";
                }
                else
                {
                    invoiceEntity.InvoiceStatus = "Partial";
                }

                DateTime actualDate = DateTime.Now;
                var paymentDueDate = DateTime.Parse(invoiceDto.PaymentDueDate);

                if (paymentDueDate < actualDate && invoiceDto.InvoicePayment == null)
                {
                    invoiceDto.PaymentStatus = "Overdue";
                }
                else if (paymentDueDate >= actualDate && invoiceDto.InvoicePayment == null)
                {
                    invoiceDto.PaymentStatus = "Pending";
                }
                else if (invoiceDto.InvoicePayment != null)
                {
                    invoiceDto.PaymentStatus = "Paid";
                }
                else
                {
                    invoiceDto.PaymentStatus = "Error";
                    throw new Exception("Error al asignar un estado de pago a la factura");
                }

                LoadPayment(dbContext, invoiceDto, invoiceEntity);

                dbContext.Invoices.Add(invoiceEntity);

            }

            await dbContext.SaveChangesAsync();
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