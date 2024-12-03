using Serilog;
using Serilog.Events;
using EventsWebApplication.Application.Exceptions;

namespace EventsWebApplication.Presentation.Middlewares
{
    public class LoggingExceptionsMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (UnauthorizedException ex)
            {
                LogException(context, ex.GetErrorDetails(), 401, ex.GetType().Name);
            }
            catch (ExpireException ex)
            {
                LogException(context, ex.GetErrorDetails(), 401, ex.GetType().Name);
            }
            catch (NoPermissionException ex)
            {
                LogException(context, ex.GetErrorDetails(), 403, ex.GetType().Name);
            }
            catch (AlreadyExistsException ex)
            {
                LogException(context, ex.GetErrorDetails(), 409, ex.GetType().Name);
            }
            catch (NotFoundException ex)
            {
                LogException(context, ex.GetErrorDetails(), 404, ex.GetType().Name);
            }
            catch (BadRequestException ex)
            {
                LogException(context, ex.GetErrorDetails(), 400, ex.GetType().Name);
            }
            catch (Exception ex)
            {
                var error = new
                {
                    ex.Message,
                    Inner = ex.InnerException?.Message
                };

                LogException(context, error, 500, ex.GetType().Name);
            }
        }

        private void LogException(HttpContext context, object error, int statusCode, string exceptionType)
        {
            var errorDetails = new
            {
                Status = statusCode,
                Error = error,
                Instance = new { context.Request.Path.Value, context.Request.Path.HasValue }
            };

            Log.Logger.Write(
                LogEventLevel.Error,
                "{Method}{RemoteIpAddress}{ExceptionType}{@Information}",
                context.Request.Method,
                context.Connection.RemoteIpAddress,
                exceptionType,
                errorDetails
            );
        }
    }
}