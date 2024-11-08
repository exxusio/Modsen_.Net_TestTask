using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Json;
using Microsoft.OpenApi.Models;
using EventsWebApplication.Presentation.Middlewares;

namespace EventsWebApplication.Presentation
{
    public static class PresentationInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.SwaggerConfigure();
            services.LoggerConfigure(configuration);
            services.MiddlewareScoped();
            return services;
        }

        private static IServiceCollection SwaggerConfigure(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EventsWebApplication", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Enter your access token",
                    Name = "Authorization",
                    Scheme = "bearer",
                    Type = SecuritySchemeType.Http
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            return services;
        }

        private static IServiceCollection LoggerConfigure(this IServiceCollection services, IConfiguration configuration)
        {
            var loggingSettings = configuration.GetSection("LoggingSettings");

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.File(
                    new JsonFormatter(),
                    loggingSettings["Path"]!,
                    rollingInterval: Enum.Parse<RollingInterval>(loggingSettings["RollingInterval"]!),
                    retainedFileCountLimit: int.Parse(loggingSettings["RetainedFileCountLimit"]!),
                    fileSizeLimitBytes: long.Parse(loggingSettings["FileSizeLimitBytes"]!))
                .CreateLogger();

            return services;
        }

        private static IServiceCollection MiddlewareScoped(this IServiceCollection services)
        {
            services.AddScoped<LoggingExceptionsMiddleware>();
            services.AddScoped<ExceptionHandlingMiddleware>();

            return services;
        }
    }
}