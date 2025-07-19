using Messenger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastracture.MessageConfiguration;

public class GroupMessageConfiguration : IEntityTypeConfiguration<GroupMessage>
{
    public void Configure(EntityTypeBuilder<GroupMessage> builder)
    {
        builder.HasKey(m => m.GroupMessageId);

        builder.Property(m => m.Text)
            .IsRequired();

        builder.Property(m => m.GroupMessageSentAt)
            .IsRequired();

        builder.HasOne(m => m.GroupSender)
            .WithMany()
            .HasForeignKey(m => m.GroupSenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Group)
            .WithMany()
            .HasForeignKey(m => m.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
