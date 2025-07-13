using Messenger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastracture.MessageConfiguration;

public class IncomingConfiguration : IEntityTypeConfiguration<IncomingMessage>
{
    public void Configure(EntityTypeBuilder<IncomingMessage> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.ReceivedAt)
            .IsRequired();

        builder.Property(m => m.IsRead)
            .HasDefaultValue(false);

        builder.HasOne(m => m.OutgoingMessage)
            .WithMany(m => m.IncomingMessages)
            .HasForeignKey(m => m.OutgoingMessageId)
            .OnDelete(DeleteBehavior.Cascade); 

        builder.HasOne(m => m.Receiver)
            .WithMany()
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}