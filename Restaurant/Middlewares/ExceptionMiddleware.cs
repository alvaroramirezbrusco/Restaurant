using Application.Exceptions;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace EventService.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception ex,
            ILogger logger)
        {
            context.Response.ContentType = "application/json";

            HttpStatusCode statusCode;
            object response;

            switch (ex)
            {
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;

                    response = new
                    {
                        error = validationException.Errors.First().ErrorMessage
                    };
                    break;

                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    response = new { error = ex.Message };
                    break;

                case ArgumentException:
                    statusCode = HttpStatusCode.BadRequest;
                    response = new { error = ex.Message };
                    break;

                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    response = new { error = ex.Message };
                    break;

                case ConflictException:
                    statusCode = HttpStatusCode.Conflict;
                    response = new { error = ex.Message };
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    response = new { error = "Ocurrió un error inesperado" };
                    break;
            }

            logger.LogError(ex, "Ocurrió un error: {Message}", ex.Message);

            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
