using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Configs
{
    public class EventRegistrationConfig
    : IEntityTypeConfiguration<EventRegistration>
    {
        public void Configure(EntityTypeBuilder<EventRegistration> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(er => er.EventId)
                .IsRequired();

            builder.Property(er => er.ParticipantId)
                .IsRequired();

            builder.Property(er => er.RegistrationDate)
                .HasColumnType("timestamp with time zone")
                .HasConversion(
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                )
                .IsRequired();

            builder.HasOne(er => er.Event)
                .WithMany(e => e.EventRegistrations)
                .HasForeignKey(er => er.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(er => er.Participant)
                .WithMany(u => u.EventRegistrations)
                .HasForeignKey(er => er.ParticipantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}