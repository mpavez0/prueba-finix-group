using System.Net;
using System.Text.Json;

namespace GestorFacturas.API.Middlewares
{

    public class ExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionsMiddleware> _logger;

        public ExceptionsMiddleware(RequestDelegate next, ILogger<ExceptionsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió una excepción no manejada");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "Ocurrió un error inesperado.";

            // Mapeo de excepciones a códigos HTTP
            switch (exception)
            {
                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = exception.Message;
                    break;
                case InvalidOperationException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = exception.Message;
                    break;
                case ArgumentException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = exception.Message;
                    break;
                case NullReferenceException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = exception.Message;
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = exception.Message;
                    break;
            }

            var payload = JsonSerializer.Serialize(new { message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(payload);
        }
    }

}
