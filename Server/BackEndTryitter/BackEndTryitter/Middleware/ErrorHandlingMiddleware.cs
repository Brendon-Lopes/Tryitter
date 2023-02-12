using System.Net;
using System.Text.Json;
using BackEndTryitter.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BackEndTryitter.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
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

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;

        if (exception is CustomException e)
        {
            code = e.StatusCode;
        }
        else if (exception is DbUpdateException)
        {
            code = HttpStatusCode.BadRequest;
        }


        var result = JsonSerializer.Serialize(new { error = new { message = exception.Message}});
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}