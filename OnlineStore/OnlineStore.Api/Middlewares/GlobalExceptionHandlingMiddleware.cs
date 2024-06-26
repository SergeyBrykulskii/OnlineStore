using Serilog.Core;

namespace OnlineStore.Api.Middlewares;
public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Logger _logger;
    public GlobalExceptionHandlingMiddleware(RequestDelegate next, Logger logger)
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
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var logMessage = $"---> Request {context.Request.Path} failed with error: {ex.Message}";
        _logger.Information(logMessage);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync("error");
    }
}