using Core.Errors;
using System.Text.Json;

namespace E_Commerce.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            _logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = 500;
        var message = "Serverda xatolik yuz berdi. Iltimos, keyinroq urinib ko‘ring.";

        if (exception is EntityNotFoundException)
        {
            code = 404;
            message = "Resurs topilmadi.";
        }
        else if (exception is AuthException || exception is UnauthorizedException)
        {
            code = 401;
            message = "Foydalanuvchi tizimga kirmagan yoki email tasdiqlanmagan.";
        }
        else if (exception is ForbiddenException || exception is NotAllowedException)
        {
            code = 403;
            message = "Sizga bu amalni bajarishga ruxsat berilmagan.";
        }

        context.Response.StatusCode = code;
        context.Response.ContentType = "application/json";

        var response = new
        {
            StatusCode = code,
            Message = message,
            Detail = exception.Message
        };

        var json = JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(json);
    }

}
