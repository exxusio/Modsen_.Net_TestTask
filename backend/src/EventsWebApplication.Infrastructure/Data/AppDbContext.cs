using Microsoft.EntityFrameworkCore;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data
{
    public class AppDbContext(
        DbContextOptions<AppDbContext> options
    ) : DbContext(options)
    {
        public DbSet<EventRegistration> EventRegistrations => Set<EventRegistration>();
        public DbSet<EventCategory> EventCategories => Set<EventCategory>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}