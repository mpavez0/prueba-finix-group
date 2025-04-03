using GestorFacturas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<InvoiceDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=invoices.db"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/invoices", async(InvoiceDbContext db) =>
{
    var invoices = await db.Invoices
         .Include(i => i.Customer)
         .Include(i => i.InvoiceDetails)
         .Include(i => i.InvoicePayment)
         .Include(i => i.InvoiceCreditNotes)
         .ToListAsync();
    return Results.Ok(invoices);
})
.WithName("Aaaa")
.WithOpenApi();

app.Run();
