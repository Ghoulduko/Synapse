namespace Synapse.Core.Entities;

public class ConversationParticipant
{
    public int Id { get; set; }
    public int ConversationId { get; set; }
    public int UserId { get; set; }
    
    public Conversation Conversation { get; set; }
    public User User { get; set; }
}