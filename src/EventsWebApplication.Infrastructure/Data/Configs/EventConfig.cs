using EventsWebApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApplication.DataAccess.Configurations
{
    public class EventConfig : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Description)
                .HasMaxLength(500);

            builder.Property(e => e.Date)
                .IsRequired();

            builder.Property(e => e.Time)
                .IsRequired();

            builder.Property(e => e.Location)
                .HasMaxLength(200);

            builder.Property(e => e.Category)
                .HasMaxLength(50);

            builder.Property(e => e.ImageUrl)
                .HasMaxLength(250);

            builder.Property(e => e.MaxParticipants)
                .IsRequired();

            builder.HasMany(e => e.EventRegistrations)
                .WithOne(er => er.Event)
                .HasForeignKey(er => er.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
