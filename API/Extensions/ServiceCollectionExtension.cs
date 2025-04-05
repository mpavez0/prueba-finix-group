using GestorFacturas.API.Middlewares;
using GestorFacturas.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorFacturas.Domain.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ServiceGeneric(this IServiceCollection services)
        {
            services.AddTransient<IDataSeeder, DataSeeder>();
            return services;
        }
        public static IApplicationBuilder UseAuthorizationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
