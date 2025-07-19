using Messenger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastracture.MessageConfiguration;

public class ChatMessageVisibilityConfiguration : IEntityTypeConfiguration<ChatMessageVisibility>
{
    public void Configure(EntityTypeBuilder<ChatMessageVisibility> builder)
    {
        builder.HasKey(v => v.ChatMessageVisibilityId);

        builder.HasOne(v => v.User)
            .WithMany()
            .HasForeignKey(v => v.HidUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(v => v.Message)
            .WithMany(m => m.Visibilities)
            .HasForeignKey(v => v.HiddenMessageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
