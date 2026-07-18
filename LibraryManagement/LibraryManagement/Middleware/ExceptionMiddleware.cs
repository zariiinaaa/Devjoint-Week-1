using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context,Exception exception)
    {
        _logger.LogError( exception,"An unhandled exception occurred.");

        var (statusCode, title) = exception switch
        {
            KeyNotFoundException =>
                (StatusCodes.Status404NotFound, "Resource not found"),

            InvalidOperationException =>
                (StatusCodes.Status409Conflict, "Conflict"),

            ArgumentException =>
                (StatusCodes.Status400BadRequest, "Bad request"),

            _ =>
                (StatusCodes.Status500InternalServerError,
                    "Internal server error")
        };

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = statusCode ==StatusCodes.Status500InternalServerError? 
            "An unexpected error occurred."
                : exception.Message,
            Instance = context.Request.Path
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}