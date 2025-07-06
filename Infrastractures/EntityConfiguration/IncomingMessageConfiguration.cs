using Messenger.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastractures.EntityConfiguration;

public class IncomingMessageConfiguration : IEntityTypeConfiguration<IncomingMessage>
{
    public void Configure(EntityTypeBuilder<IncomingMessage> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.IsRead)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne(i => i.OutcomingMessage)
            .WithMany(m => m.IncomingMessages)
            .HasForeignKey(i => i.OutcomingMessageId);

        builder.HasOne(i => i.Receiver)
            .WithMany(u => u.ReceivedMessages)
            .HasForeignKey(i => i.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
