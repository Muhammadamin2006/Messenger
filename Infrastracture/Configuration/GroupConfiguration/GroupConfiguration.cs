using Messenger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastracture.MessageConfiguration;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(g => g.GroupId);
        builder.Property(g => g.GroupName)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(g => g.GroupCreatedAt)
            .IsRequired();
    }
}