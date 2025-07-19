using Messenger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastracture.MessageConfiguration;

public class GroupMessageVisibilityConfiguration : IEntityTypeConfiguration<GroupMessageVisibility>
{
    public void Configure(EntityTypeBuilder<GroupMessageVisibility> builder)
    {
        builder.HasKey(v => v.GroupMessageVisibilityMessageId);

        builder.HasOne(v => v.Message)
            .WithMany(m => m.HiddenByUsers)
            .HasForeignKey(v => v.GroupHidMessageId);

        builder.HasOne(v => v.User)
            .WithMany()
            .HasForeignKey(v => v.HidByUserId);
    }
}