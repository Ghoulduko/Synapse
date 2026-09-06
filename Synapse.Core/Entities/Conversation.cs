namespace Synapse.Core.Entities;

public class Conversation
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public List<ConversationParticipant> Participants { get; set; }
    public List<Message> Messages { get; set; }
}