using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Configs
{
    public class RefreshTokenConfig
    : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(rt => rt.Key);

            builder.Property(rt => rt.UserId)
                .IsRequired();

            builder.Property(rt => rt.CreationTime)
                .IsRequired();

            builder.Property(rt => rt.ExpirationTime)
                .IsRequired();

            builder.HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}