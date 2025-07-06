using Messenger.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastractures.EntityConfiguration;

public class UserGroupConfiguration  : IEntityTypeConfiguration<UserGroup>
{
    public void Configure(EntityTypeBuilder<UserGroup> modelBuilder)
    {
        modelBuilder.HasKey(ug => new { ug.UserId, ug.GroupId });
    }
}