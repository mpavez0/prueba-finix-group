using GestorFacturas.Domain.Entities;
using GestorFacturas.Application.Services;
using Microsoft.AspNetCore.Mvc;
using GestorFacturas.Application.Services.Interfaces;
using GestorFacturas.Domain.Entities.DTOs;

namespace GestorFacturas.API.Controllers
{
    public static class InvoiceController
    {
        public static void MapInvoiceEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/invoices");

            group.MapGet("/", async ([FromServices] IInvoiceService service) =>
            {
                var invoices = await service.GetAllAsync();
                return Results.Ok(invoices);
            });

            group.MapGet("/invoiceNumber", async ([FromServices] IInvoiceService service, [FromQuery] int invoiceNumber) =>
            {
                var invoice = await service.GetByIdAsync(invoiceNumber);
                return invoice is not null ? Results.Ok(invoice) : Results.NotFound();
            });

            group.MapGet("/status", async ([FromServices] IInvoiceService service, [FromQuery] string status) =>
            {
                var invoice = await service.GetByInvoiceStatus(status);
                return invoice is not null ? Results.Ok(invoice) : Results.NotFound();
            });

            group.MapGet("/paymentStatus", async ([FromServices] IInvoiceService service, [FromQuery] string paymentStatus) =>
            {
                var invoice = await service.GetByPaymentStatus(paymentStatus);
                return invoice is not null ? Results.Ok(invoice) : Results.NotFound();
            });

            group.MapPost("/creditNote", async ([FromServices] ICreditNoteService service, [FromQuery] int invoiceNumber, [FromBody] CreditNoteDTO creditNoteDTO) =>
            {
                try
                {
                    await service.AddCreditNote(invoiceNumber, creditNoteDTO);
                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });
        }
    }
}
