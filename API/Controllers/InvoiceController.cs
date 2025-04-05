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

            group.MapGet("/", async ([FromServices] IInvoiceService service, [FromQuery] int page, [FromQuery] int pageSize) =>
            {
                var invoices = await service.GetAllAsync(page, pageSize);
                return Results.Ok(invoices);
            })
            .WithName("Obtener facturas")
            .WithTags("Facturas")
            .WithSummary("Lista de todas las facturas disponibles")
            .Produces(StatusCodes.Status200OK, typeof(List<InvoiceDTO>), "application/json")
            .Produces(StatusCodes.Status400BadRequest, typeof(GenericResponse), "application/json")
            .Produces(StatusCodes.Status401Unauthorized, typeof(string), "plain/text")
            .Produces(StatusCodes.Status404NotFound, typeof(GenericResponse), "application/json")
            .Produces(StatusCodes.Status500InternalServerError, typeof(GenericResponse), "application/json");

            group.MapGet("/invoiceNumber", async ([FromServices] IInvoiceService service, [FromQuery] int invoiceNumber) =>
            {
                var invoice = await service.GetByIdAsync(invoiceNumber);
                return invoice is not null ? Results.Ok(invoice) : Results.NotFound();
            })
            .WithName("Obtener factura según su número")
            .WithTags("Facturas")
            .WithSummary("Lista de la factura según su número")
            .Produces(StatusCodes.Status200OK, typeof(InvoiceDTO), "application/json")
            .Produces(StatusCodes.Status400BadRequest, typeof(GenericResponse), "application/json")
            .Produces(StatusCodes.Status401Unauthorized, typeof(string), "plain/text")
            .Produces(StatusCodes.Status404NotFound, typeof(GenericResponse), "application/json")
            .Produces(StatusCodes.Status500InternalServerError, typeof(GenericResponse), "application/json");

            group.MapGet("/status", async ([FromServices] IInvoiceService service, [FromQuery] string status) =>
            {
                var invoice = await service.GetByInvoiceStatus(status);
                return invoice is not null ? Results.Ok(invoice) : Results.NotFound();
            })
            .WithName("Obtener factura según su estado")
            .WithTags("Facturas")
            .WithSummary("Lista de la factura según su estado")
            .Produces(StatusCodes.Status200OK, typeof(List<InvoiceDTO>), "application/json")
            .Produces(StatusCodes.Status400BadRequest, typeof(GenericResponse), "application/json")
            .Produces(StatusCodes.Status401Unauthorized, typeof(string), "plain/text")
            .Produces(StatusCodes.Status404NotFound, typeof(GenericResponse), "application/json")
            .Produces(StatusCodes.Status500InternalServerError, typeof(GenericResponse), "application/json");

            group.MapGet("/paymentStatus", async ([FromServices] IInvoiceService service, [FromQuery] string paymentStatus) =>
            {
                var invoice = await service.GetByPaymentStatus(paymentStatus);
                return invoice is not null ? Results.Ok(invoice) : Results.NotFound();
            })
            .WithName("Obtener factura según su estado de pago")
            .WithTags("Facturas")
            .WithSummary("Lista de la factura según su estado de pago")
            .Produces(StatusCodes.Status200OK, typeof(List<InvoiceDTO>), "application/json")
            .Produces(StatusCodes.Status400BadRequest, typeof(GenericResponse), "application/json")
            .Produces(StatusCodes.Status401Unauthorized, typeof(string), "plain/text")
            .Produces(StatusCodes.Status404NotFound, typeof(GenericResponse), "application/json")
            .Produces(StatusCodes.Status500InternalServerError, typeof(GenericResponse), "application/json");

            group.MapPost("/creditNote", async ([FromServices] ICreditNoteService service, [FromQuery] int invoiceNumber, [FromBody] CreditNoteDTO creditNoteDTO) =>
            {
                await service.AddCreditNote(invoiceNumber, creditNoteDTO);
                return Results.Ok(new GenericResponse("Nota de crédito creada exitosamente"));
            })
            .WithName("Añade una nota de crédito")
            .WithTags("Facturas")
            .WithSummary("Añade una nota de crédito a una factura existente")
            .Produces(StatusCodes.Status200OK, typeof(GenericResponse), "application/json")
            .Produces(StatusCodes.Status400BadRequest, typeof(GenericResponse), "application/json")
            .Produces(StatusCodes.Status401Unauthorized, typeof(string), "plain/text")
            .Produces(StatusCodes.Status404NotFound, typeof(GenericResponse), "application/json")
            .Produces(StatusCodes.Status500InternalServerError, typeof(GenericResponse), "application/json");
        }
    }
}
