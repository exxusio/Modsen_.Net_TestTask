using EventsWebApplication.Application.Configs.Policies;
using EventsWebApplication.Application.Abstractions.Data;
using EventsWebApplication.Application.Abstractions.Auth;
using EventsWebApplication.Infrastructure.Notify.SignalR.Hubs;
using EventsWebApplication.Infrastructure.Data;
using EventsWebApplication.Infrastructure;
using EventsWebApplication.Presentation.Middlewares;

namespace EventsWebApplication.Presentation
{
    public static class AppInitializer
    {
        public static async Task<WebApplication> StartApplication(this WebApplication webApplication)
        {
            webApplication.UseCors(Policies.CORS);

            webApplication.UseRouting();

            webApplication.UseAuthentication();
            webApplication.UseAuthorization();

            webApplication.UseMiddleware<LoggingExceptionsMiddleware>();
            webApplication.UseMiddleware<ExceptionHandlingMiddleware>();

            webApplication.UseHttpsRedirection();

            webApplication.MapHub<EventNotificationHub>("/notify/event");

            webApplication.MapControllers();

            await webApplication.DbInitialize();
            await webApplication.SeedDataAsync();

            webApplication.SwaggerStart();

            webApplication.Run();

            return webApplication;
        }

        private static async Task<WebApplication> DbInitialize(this WebApplication webApplication)
        {
            using var scope = webApplication.Services.CreateScope();
            var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await DbInitializer.Initialize(appDbContext);

            return webApplication;
        }

        private static async Task<WebApplication> SeedDataAsync(this WebApplication webApplication)
        {
            using var scope = webApplication.Services.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
            await DbInitializer.SeedAsync(unitOfWork, passwordHasher);

            return webApplication;
        }

        private static WebApplication SwaggerStart(this WebApplication webApplication)
        {
            if (webApplication.Environment.IsDevelopment())
            {
                webApplication.UseSwagger();
                webApplication.UseSwaggerUI();
            }

            return webApplication;
        }
    }
}