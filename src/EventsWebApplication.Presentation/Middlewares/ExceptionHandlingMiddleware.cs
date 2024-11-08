using System.Text.Json;
using EventsWebApplication.Domain.Exceptions.Bases;

namespace EventsWebApplication.Presentation.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (BaseException ex)
            {
                await GenerateErrorDetails(context, ex.Message, ex.Status);
                throw;
            }
            catch (Exception ex)
            {
                await GenerateErrorDetails(context, "An unhandled exception occurred during the request", 500);
                throw;
            }
        }

        private async Task GenerateErrorDetails(HttpContext context, string message, int statusCode)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json+error";

            var errorDetails = new
            {
                Message = message,
                Status = statusCode,
                Instance = context.Request.Path,
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorDetails));
        }
    }
}