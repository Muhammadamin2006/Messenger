using Messenger.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Infrastracture.Database;

public class MessengerContext : DbContext
{
    
    public DbSet<User> Users { get; set; }
    public DbSet<UserBlock> UserBlocks { get; set; }

    public DbSet<Group> Groups { get; set; }
    public DbSet<GroupUser> GroupUsers { get; set; }
    public DbSet<GroupMessage> GroupMessages { get; set; }
    public DbSet<GroupMessageVisibility> GroupMessageVisibilities { get; set; }
    
    public DbSet<OutgoingMessage> OutgoingMessages { get; set; }
    public DbSet<IncomingMessage> IncomingMessages { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<ChatMessageEditHistory> ChatMessageEditHistories { get; set; }
    public DbSet<ChatMessageVisibility> ChatMessageVisibilities { get; set; }
    

    
    

    public MessengerContext(DbContextOptions<MessengerContext> options) : base(options)
    {
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // if (optionsBuilder.IsConfigured)
        // {
        //     optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=MessengerAppDb; Trusted_Connection=True; TrustServerCertificate=True;");
        // }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MessengerContext).Assembly);
        
        
        base.OnModelCreating(modelBuilder);
        
        
    }


    
}