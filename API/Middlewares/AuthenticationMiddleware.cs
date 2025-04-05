using System.Text;

namespace GestorFacturas.API.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ValidUsername = "admin";
        private const string ValidPassword = "password";
        private const string AuthorizationHeaderName = "Authorization";

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(AuthorizationHeaderName, out var authHeader))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Error al recuperar header de autorización");
                return;
            }

            if (!authHeader.ToString().StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Header de autorización inválido");
                return;
            }

            var encodedCredentials = authHeader.ToString().Substring("Basic ".Length).Trim();
            string decodedCredentials;
            try
            {
                var credentialBytes = Convert.FromBase64String(encodedCredentials);
                decodedCredentials = Encoding.UTF8.GetString(credentialBytes);
            }
            catch (FormatException)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Credenciales inválidas");
                return;
            }

            var credentialsParts = decodedCredentials.Split(':', 2);
            if (credentialsParts.Length != 2)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Formato de credenciales inválido");
                return;
            }

            var username = credentialsParts[0];
            var password = credentialsParts[1];

            if (username != ValidUsername || password != ValidPassword)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Credenciales inválidas");
                return;
            }

            await _next(context);
        }
    }
}
