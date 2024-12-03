using System.Text.Json;
using EventsWebApplication.Application.Exceptions;

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
            catch (UnauthorizedException ex)
            {
                await GenerateErrorDetails(context, ex.Message, 401);
                throw;
            }
            catch (ExpireException ex)
            {
                await GenerateErrorDetails(context, ex.Message, 401);
                throw;
            }
            catch (NoPermissionException ex)
            {
                await GenerateErrorDetails(context, ex.Message, 403);
                throw;
            }
            catch (AlreadyExistsException ex)
            {
                await GenerateErrorDetails(context, ex.Message, 409);
                throw;
            }
            catch (NotFoundException ex)
            {
                await GenerateErrorDetails(context, ex.Message, 404);
                throw;
            }
            catch (BadRequestException ex)
            {
                await GenerateErrorDetails(context, ex.Message, 400);
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