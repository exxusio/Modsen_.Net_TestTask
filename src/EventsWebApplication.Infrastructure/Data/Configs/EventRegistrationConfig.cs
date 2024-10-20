using EventsWebApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApplication.DataAccess.Configurations
{
    public class EventRegistrationConfig : IEntityTypeConfiguration<EventRegistration>
    {
        public void Configure(EntityTypeBuilder<EventRegistration> builder)
        {
            builder.HasKey(er => new { er.EventId, er.ParticipantId });

            builder.Property(er => er.EventId)
                .IsRequired();

            builder.Property(er => er.ParticipantId)
                .IsRequired();

            builder.Property(er => er.RegistrationDate)
                .IsRequired();

            builder.HasOne(er => er.Event)
                .WithMany(e => e.EventRegistrations)
                .HasForeignKey(er => er.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(er => er.Participant)
                .WithMany(u => u.EventRegistrations)
                .HasForeignKey(er => er.ParticipantId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
