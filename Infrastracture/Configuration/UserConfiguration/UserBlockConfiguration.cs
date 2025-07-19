using Messenger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastracture.MessageConfiguration;

public class UserBlockConfiguration : IEntityTypeConfiguration<UserBlock>
{
    public void Configure(EntityTypeBuilder<UserBlock> builder)
    {
        builder.HasKey(x => x.UserBlockId);

        builder.HasOne(x => x.Blocker)
            .WithMany() 
            .HasForeignKey(x => x.BlockerId)
            .OnDelete(DeleteBehavior.Restrict); 

        builder.HasOne(x => x.Blocked)
            .WithMany() 
            .HasForeignKey(x => x.BlockedId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}