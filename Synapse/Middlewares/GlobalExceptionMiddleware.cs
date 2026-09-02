using FluentValidation;
using Synapse.Core.Exceptions;

namespace Synapse.Middlewares;

public class GlobalExceptionMiddleware
{
    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var statusCode = ex switch
        {
            ValidationException => 400,
            NotFoundException => 404,
            UnauthorizedAccessException => 401,
            ArgumentException => 400,
            _ => 500
        };

        if (statusCode == 500)
            _logger.LogError(ex, ex.Message);
        else
            _logger.LogWarning(ex, ex.Message);

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        return context.Response.WriteAsJsonAsync(new
        {
            Error = statusCode == 500 ? "An unexpected error occured." : ex.Message
        });
    }
}