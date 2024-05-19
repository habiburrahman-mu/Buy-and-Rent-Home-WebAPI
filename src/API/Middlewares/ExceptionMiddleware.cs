using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Presentation.Errors;
using System.Net;

namespace API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next,
                                ILogger<ExceptionMiddleware> logger,
                                IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            ApiError response;
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            String message;
            var exceptionType = ex.GetType();

            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                statusCode = HttpStatusCode.Forbidden;
                message = "You are not authorized";
            }
            else if(exceptionType == typeof(BadHttpRequestException))
            {
                statusCode = HttpStatusCode.BadRequest;
                message = ex.Message;
            }
            else if(exceptionType == typeof(InvalidDomainRequestException))
            {
                statusCode = HttpStatusCode.BadRequest;
                message = ex.Message;
            }
            else
            {
                statusCode = HttpStatusCode.InternalServerError;
                message = "Some unknown error occurred";
            }

            if (_env.IsDevelopment())
            {
                response = new ApiError((int)statusCode, ex.Message, ex.StackTrace?.ToString());
            }
            else
            {
                response = new ApiError((int)statusCode, message);
            }
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(response.ToString());
        }
    }

}
