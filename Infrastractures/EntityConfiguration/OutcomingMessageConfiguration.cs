using Messenger.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastractures.EntityConfiguration;

public class OutcomingMessageConfiguration : IEntityTypeConfiguration<OutcomingMessage>
{
    public void Configure(EntityTypeBuilder<OutcomingMessage> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Text)
            .IsRequired();

        builder.Property(m => m.SentAt)
            .IsRequired();

        builder.HasOne(m => m.Sender)
            .WithMany(u => u.SentMessages)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.ReceiverUser)
            .WithMany()
            .HasForeignKey(m => m.ReceiverUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.ReceiverGroup)
            .WithMany()
            .HasForeignKey(m => m.ReceiverGroupId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(m => m.IncomingMessages)
            .WithOne(i => i.OutcomingMessage)
            .HasForeignKey(i => i.OutcomingMessageId);
        
    }
}