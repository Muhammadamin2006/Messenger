using Messenger.Models;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Infrastractures.Database;

public class MessengerContext : DbContext
{
    
    public DbSet<User> Users { get; set; }
    public DbSet<IncomingMessage> IncomingMessages { get; set; }
    public DbSet<OutcomingMessage> OutcomingMessages { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    
    public MessengerContext(DbContextOptions<MessengerContext> options) : base(options)
    {
        
    }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MessengerContext).Assembly);
        
        
        base.OnModelCreating(modelBuilder);
        
        
    }


    
}




