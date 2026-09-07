namespace Synapse.Application.Dtos.Chat;

public class ConversationDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public List<ConversationParticipantDto> Participants { get; set; }
    public List<MessageDto> Messages { get; set; }
}