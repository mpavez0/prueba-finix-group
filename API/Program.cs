using GestorFacturas.API.Controllers;
using GestorFacturas.Domain.Extensions;
using GestorFacturas.Domain.Mappers;
using GestorFacturas.Application.Services;
using GestorFacturas.Application.Services.Interfaces;
using GestorFacturas.Infrastructure.Data;
using GestorFacturas.Infrastructure.Repositories;
using GestorFacturas.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using GestorFacturas.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("basic", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "basic",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ingrese las credenciales de autenticación"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "basic"
                }
            },
            new List<string>()
        }
    });
});

builder.Services.ServiceGeneric();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost5173", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<InvoiceDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=Invoices.db"));

builder.Services.AddScoped<Mapper>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

builder.Services.AddScoped<ICreditNoteRepository, CreditNoteRepository>();
builder.Services.AddScoped<ICreditNoteService, CreditNoteService>();

var app = builder.Build();

app.UseCors("AllowLocalhost5173");

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<InvoiceDbContext>();

    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();

    var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();

    var basePath = AppContext.BaseDirectory;
    var jsonFilePath = Path.Combine(basePath, "Data", "bd_exam.json");
    await seeder.SeedDataAsync(dbContext, jsonFilePath);
}

app.MapInvoiceEndpoints();


app.UseSwagger();
app.UseSwaggerUI();


app.UseMiddleware<ExceptionsMiddleware>();

app.UseAuthorizationMiddleware();

app.Run();
