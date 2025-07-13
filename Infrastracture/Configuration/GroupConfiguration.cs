using Messenger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastracture.MessageConfiguration;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(g => g.Messages)
            .WithOne(m => m.ReceiverGroup)
            .HasForeignKey(m => m.ReceiverGroupId)
            .OnDelete(DeleteBehavior.Restrict);
    }
    
}