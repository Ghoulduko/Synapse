namespace Synapse.Application.Dtos.Chat;

public class ConversationParticipantDto
{
    public int Id { get; set; }
    public int ConversationId { get; set; }
    public int UserId { get; set; }
}