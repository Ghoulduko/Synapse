namespace Synapse.Application.Dtos.Chat;

public class AddMessageDto
{
    public int ConversationId { get; set; }
    public string Content { get; set; }
}