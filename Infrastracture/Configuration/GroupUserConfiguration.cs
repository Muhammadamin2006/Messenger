using Messenger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastracture.MessageConfiguration;

public class GroupUserConfiguration : IEntityTypeConfiguration<GroupUser>
{
    public void Configure(EntityTypeBuilder<GroupUser> builder)
    {
        builder.HasKey(ug => new { ug.UserId, ug.GroupId });

        builder.HasOne(ug => ug.User)
            .WithMany(u => u.UserGroups)
            .HasForeignKey(ug => ug.UserId);

        builder.HasOne(ug => ug.Group)
            .WithMany(g => g.GroupUsers)
            .HasForeignKey(ug => ug.GroupId);
    }
}
