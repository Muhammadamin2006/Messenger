using Messenger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastracture.MessageConfiguration;

public class GroupMessageVisibilityConfiguration : IEntityTypeConfiguration<GroupMessageVisibility>
{
    public void Configure(EntityTypeBuilder<GroupMessageVisibility> builder)
    {
        builder.HasKey(v => v.Id);

        builder.HasOne(v => v.Message)
            .WithMany(m => m.GroupMessageVisibilities)
            .HasForeignKey(v => v.MessageId);

        builder.HasOne(v => v.User)
            .WithMany()
            .HasForeignKey(v => v.UserId);
    }
}