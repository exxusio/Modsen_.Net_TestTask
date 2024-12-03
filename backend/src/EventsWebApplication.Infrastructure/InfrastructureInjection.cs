
using System.Text;
using StackExchange.Redis;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using EventsWebApplication.Domain.Abstractions.Data.Repositories;
using EventsWebApplication.Domain.Abstractions.Caching;
using EventsWebApplication.Domain.Abstractions.Notify;
using EventsWebApplication.Domain.Abstractions.Auth;
using EventsWebApplication.Domain.Abstractions.Data;
using EventsWebApplication.Domain.Consts;
using EventsWebApplication.Application.Configs.Policies;
using EventsWebApplication.Infrastructure.Data.Repositories;
using EventsWebApplication.Infrastructure.Notify.SignalR.Services;
using EventsWebApplication.Infrastructure.Caching;
using EventsWebApplication.Infrastructure.Auth;
using EventsWebApplication.Infrastructure.Data;

namespace EventsWebApplication.Infrastructure
{
    public static class InfrastructureInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.DbConfigure(configuration);
            services.RedisConfigure(configuration);
            services.AuthConfigure(configuration);
            services.RepositoriesConfigure();
            services.UnitOfWorkConfigure();
            services.SignalRConfigure();

            return services;
        }

        private static IServiceCollection DbConfigure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgreDbConnection");

            services.AddDbContext<AppDbContext>(options => options
                .UseNpgsql(connectionString)
                .UseLazyLoadingProxies());

            return services;
        }

        private static IServiceCollection RedisConfigure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RedisConnection");
            var redis = ConnectionMultiplexer.Connect(connectionString!);

            services.AddSingleton<IConnectionMultiplexer>(redis);
            services.AddScoped<ICacheService, RedisCacheService>();

            return services;
        }

        private static IServiceCollection AuthConfigure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITokensGenerator, TokensGenerator>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)),
                    ValidateActor = true,
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/notify/event"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.AdminOnlyActions, policy =>
                    policy.RequireRole(BaseRoles.Admin));

                options.AddPolicy(Policies.CreateEvents, policy =>
                    policy.RequireRole(BaseRoles.Admin));

                options.AddPolicy(Policies.UpdateEvents, policy =>
                    policy.RequireRole(BaseRoles.Admin));

                options.AddPolicy(Policies.DeleteEvents, policy =>
                    policy.RequireRole(BaseRoles.Admin));

                options.AddPolicy(Policies.RegisterForEvent, policy =>
                    policy.RequireRole(BaseRoles.User, BaseRoles.Admin));

                options.AddPolicy(Policies.UnregisterFromEvent, policy =>
                    policy.RequireRole(BaseRoles.User, BaseRoles.Admin));
            });

            return services;
        }

        private static IServiceCollection RepositoriesConfigure(this IServiceCollection services)
        {
            services.AddScoped<IEventCategoryRepository, EventCategoryRepository>();
            services.AddScoped<IEventRegistrationRepository, EventRegistrationRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        private static IServiceCollection UnitOfWorkConfigure(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        private static IServiceCollection SignalRConfigure(this IServiceCollection services)
        {
            services.AddSignalR();
            services.AddScoped<INotificationService, SignalRNotificationService>();

            return services;
        }
    }
}