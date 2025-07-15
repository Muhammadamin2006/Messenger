using Messenger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastracture.MessageConfiguration;

public class ChatMessageConfiguation : IEntityTypeConfiguration<ChatMessage>
{
    public void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        builder.ToTable("ChatMessages");
        builder.HasKey(m => m.MessageId);

        builder.HasOne(m => m.Chat)
            .WithMany(c => c.ChatMessages)
            .HasForeignKey(m => m.ChatId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.Sender)
            .WithMany()
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(m => m.Text)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(m => m.SentAt)
            .IsRequired();
        
        builder.Property(m => m.IsDeleted)
            .HasDefaultValue(false);
    }
}
