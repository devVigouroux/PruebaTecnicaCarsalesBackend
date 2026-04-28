using System.Net;
using System.Text.Json;

namespace PruebaTecnicaCarsales.BFF.Middleware;

public class ErrorMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorMiddleware> _logger;

    public ErrorMiddleware(
        RequestDelegate next,
        ILogger<ErrorMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Error de negocio");

            context.Response.StatusCode =
                (int)HttpStatusCode.BadRequest;

            context.Response.ContentType =
                "application/json";

            var result = JsonSerializer.Serialize(new
            {
                message = ex.Message
            });

            await context.Response.WriteAsync(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado");

            context.Response.StatusCode =
                (int)HttpStatusCode.InternalServerError;

            context.Response.ContentType =
                "application/json";

            var result = JsonSerializer.Serialize(new
            {
                message = "Error interno del servidor"
            });

            await context.Response.WriteAsync(result);
        }
    }
}