using Microsoft.OpenApi.Models;
using EventsWebApplication.Presentation.Middlewares;

namespace EventsWebApplication.Presentation
{
    public static class PresentationInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.SwaggerConfigure();
            services.MiddlewareConfigure();
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

        private static IServiceCollection MiddlewareConfigure(this IServiceCollection services)
        {
            services.AddScoped<ExceptionHandlingMiddleware>();

            return services;
        }
    }
}