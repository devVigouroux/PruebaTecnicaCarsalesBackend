using System.Net;
using System.Text.Json;
using PruebaTecnicaCarsales.BFF.Exceptions;

namespace PruebaTecnicaCarsales.BFF.Middleware;

public class ErrorMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorMiddleware> _logger;

    public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger)
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
        catch (ConflictException ex)
        {
            _logger.LogWarning(ex, "Conflicto de negocio");

            await WriteResponse(context, HttpStatusCode.Conflict, ex.Message);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, "Recurso no encontrado");

            await WriteResponse(context, HttpStatusCode.NotFound, ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Error de negocio");

            await WriteResponse(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado");

            await WriteResponse(
                context,
                HttpStatusCode.InternalServerError,
                "Error interno del servidor"
            );
        }
    }

    private static async Task WriteResponse(
        HttpContext context,
        HttpStatusCode statusCode,
        string message)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var result = JsonSerializer.Serialize(new
        {
            statusCode = (int)statusCode,
            message
        });

        await context.Response.WriteAsync(result);
    }
}