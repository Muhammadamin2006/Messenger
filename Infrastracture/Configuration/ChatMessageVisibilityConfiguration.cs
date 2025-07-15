using Messenger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastracture.MessageConfiguration;

public class ChatMessageVisibilityConfiguration : IEntityTypeConfiguration<ChatMessageVisibility>
{
    public void Configure(EntityTypeBuilder<ChatMessageVisibility> builder)
    {
        builder.HasKey(v => v.Id);

        builder.HasOne(v => v.Message)
            .WithMany()
            .HasForeignKey(v => v.MessageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(v => v.UserId)
            .IsRequired();
    }
}
