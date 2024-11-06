using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Configs
{
    public class EventConfig
    : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(r => r.Name)
                .IsUnique();

            builder.Property(e => e.Description)
                .HasMaxLength(500);

            builder.Property(e => e.Date)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(e => e.Time)
                .HasColumnType("time")
                .IsRequired();

            builder.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.ImageUrl)
                .HasMaxLength(250);

            builder.Property(e => e.MaxParticipants)
                .IsRequired();

            builder.Property(e => e.CategoryId)
                .IsRequired();

            builder.HasOne(e => e.Category)
                .WithMany(ec => ec.Events)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.EventRegistrations)
                .WithOne(er => er.Event)
                .HasForeignKey(er => er.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}