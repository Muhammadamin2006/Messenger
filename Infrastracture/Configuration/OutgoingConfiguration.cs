using Messenger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastracture.MessageConfiguration;

public class OutgoingConfiguration : IEntityTypeConfiguration<OutgoingMessage>
{
    public void Configure(EntityTypeBuilder<OutgoingMessage> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Text)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(m => m.SentAt)
            .IsRequired();

        builder.HasOne(m => m.Sender)
            .WithMany()
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
    }
}   