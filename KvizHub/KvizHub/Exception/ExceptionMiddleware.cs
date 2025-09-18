using KvizHub.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace KvizHub.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

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
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            switch (ex)
            {
                case EntityNotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    break;
                case EntityConflictException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    break;
                case SaveFailedException:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
                case DeletionFailedException:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
                case AccessDeniedException:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    break;
                case EntityReferenceConflictException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    break;
                case RequiredValuesMissingException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case InvalidRequestException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new { message = ex.Message });

            return context.Response.WriteAsync(result);
        }
    }
}
