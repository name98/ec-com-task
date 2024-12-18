using System.Text.Json;
using System.Text.Json.Serialization;
using EdCom.Business.Exceptions;
using EdCom.Data.Exceptions;

namespace EdCom.WebApi.Middlewares;

public sealed class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;

        var statusCode = GetStatusCode(exception);

        var response = new
        {
            Title = GetTitle(exception),
            Status = statusCode,
        };

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response, JsonSerializerOptions));

    }

    private static int GetStatusCode(Exception exception)
    {
        return exception switch
        {
            NotFoundByIdException => StatusCodes.Status404NotFound,
            BusinessValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
    }

    private static string GetTitle(Exception exception) =>
        exception switch
        {
            NotFoundByIdException e => e.Message,
            BusinessValidationException e => e.Message,
            _ => "Server Error"
        };
}