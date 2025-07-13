using Messenger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastracture.MessageConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(u => u.SentMessages)
            .WithOne(m => m.Sender)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.ReceivedMessages)
            .WithOne(m => m.Receiver)
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(p => p.PhoneNumber)
            .IsRequired()
            .HasMaxLength(50);

    }
    
}