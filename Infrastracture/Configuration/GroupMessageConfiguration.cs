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

        builder.Property(m => m.SentAt)
            .IsRequired();

        builder.HasOne(m => m.Sender)
            .WithMany()
            .HasForeignKey(m => m.SenderId);

        builder.HasOne(m => m.Group)
            .WithMany()
            .HasForeignKey(m => m.GroupId);
    }
}
