using EventsWebApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApplication.Infrastructure.Data
{
    public class AppDbContext(
        DbContextOptions<AppDbContext> options)
        : DbContext(options)
    {
        public DbSet<Event> Events => Set<Event>();
        public DbSet<EventCategory> EventCategories => Set<EventCategory>();
        public DbSet<EventRegistration> EventRegistrations => Set<EventRegistration>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}