using Messenger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastracture.MessageConfiguration;

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.ToTable("Chats");
        builder.HasKey(c => c.ChatId);

        builder.HasOne(c => c.FirstUser)
            .WithMany()
            .HasForeignKey(c => c.FirstUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.SecondUser)
            .WithMany()
            .HasForeignKey(c => c.SecondUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(c => c.ChatCreatedAt)
            .IsRequired();
    }
}