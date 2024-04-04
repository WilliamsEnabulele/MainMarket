using MainMarket.Services.MediaAPI.Exceptions;
using MainMarket.Services.MediaAPI.Models;
using Newtonsoft.Json;

namespace MainMarket.Services.MediaAPI.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private Task HandleException(HttpContext context, Exception ex)
    {
        _logger.LogError(ex.Message);

        var code = StatusCodes.Status500InternalServerError;
        var errors = new List<string> { ex.Message };

        code = ex switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            UnauthorizedException => StatusCodes.Status401Unauthorized,
            UploadFailedException => StatusCodes.Status422UnprocessableEntity,
            BadRequestException => StatusCodes.Status400BadRequest,
            _ => code
        };

        var response = JsonConvert.SerializeObject(ApiResponse<IEnumerable<string>>.Failure(errors, code));

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = code;

        return context.Response.WriteAsync(response);
    }
}