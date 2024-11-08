using Serilog;
using Serilog.Events;
using EventsWebApplication.Domain.Exceptions.Bases;

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
            catch (BaseException ex)
            {
                LogException(context, ex.GetErrorDetails(), ex.GetType().Name);
            }
            catch (Exception ex)
            {
                var error = new
                {
                    ex.Message,
                    Inner = ex.InnerException?.Message,
                    Status = 500
                };

                LogException(context, error, ex.GetType().Name);
            }
        }

        private void LogException(HttpContext context, object error, string exceptionType)
        {
            var errorDetails = new
            {
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