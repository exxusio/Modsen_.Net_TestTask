using EventsWebApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApplication.Infrastructure.Data.Configs
{
    public class EventCategoryConfig : IEntityTypeConfiguration<EventCategory>
    {
        public void Configure(EntityTypeBuilder<EventCategory> builder)
        {
            builder.HasKey(ec => ec.Id);

            builder.Property(ec => ec.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ec => ec.Description)
                .HasMaxLength(500);

            builder.HasMany(ec => ec.Events)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}