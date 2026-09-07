using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Synapse.Core.Entities;

namespace Synapse.Infrastructure.Database;

public class SynapseDbContext : DbContext
{
    public SynapseDbContext(DbContextOptions<SynapseDbContext> options) : base(options) {}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<FriendRequest> FriendRequests { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ConversationParticipant> ConversationParticipants { get; set; }
}