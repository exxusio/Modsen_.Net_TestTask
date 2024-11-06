using System.Text.Json;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Exceptions.Bases;

namespace EventsWebApplication.Presentation.Middlewares
{
    //TODO:
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            // catch (NotFoundException ex)
            // {
            //     await context.Response.WriteAsync(await GenerateErrorDetails(context, ex));
            // }
            // catch (AlreadyExistsException ex)
            // {
            //     await context.Response.WriteAsync(await GenerateErrorDetails(context, ex));
            // }
            // catch (DuplicateRegistrationException ex)
            // {
            //     await context.Response.WriteAsync(await GenerateErrorDetails(context, ex));
            // }
            // catch (NoAvailableSeatsException ex)
            // {
            //     await context.Response.WriteAsync(await GenerateErrorDetails(context, ex));
            // }
            // catch (UnauthorizedException ex)
            // {
            //     await context.Response.WriteAsync(await GenerateErrorDetails(context, ex));
            // }
            // catch (NoPermissionException ex)
            // {
            //     await context.Response.WriteAsync(await GenerateErrorDetails(context, ex));
            // }
            // catch (ExpireException ex)
            // {
            //     await context.Response.WriteAsync(await GenerateErrorDetails(context, ex));
            // }
            catch (BaseException ex)
            {
                await GenerateErrorDetails(context, ex.GetErrorDetails(), ex.Status);
            }
            catch (Exception ex)
            {
                await GenerateErrorDetails(context, new { ex.Message, Status = 500 }, 500);
            }
        }

        private async Task GenerateErrorDetails(HttpContext context, object error, int statusCode)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json+error";

            var errorDetails = new
            {
                Error = error,
                Instance = context.Request.Path,
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorDetails));
        }

        // private async Task<string> GenerateErrorDetails(HttpContext context, Exception ex)
        // {
        //     context.Response.ContentType = "application/json+error";

        //     var errorDetails = new
        //     {
        //         ex.Message,
        //         //ex.Status,
        //         Instance = context.Request.Path
        //     };

        //     return JsonSerializer.Serialize(errorDetails);
        // }
    }
}