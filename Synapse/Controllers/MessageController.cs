using Microsoft.AspNetCore.Mvc;
using Synapse.Application.Dtos.Chat;
using Synapse.Application.Interfaces.ServiceInterfaces;

namespace Synapse.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : Controller
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpPost("SendMessage")]
    public async Task<IActionResult> SendMessage([FromBody] AddMessageDto messageDto)
    {
        var userId = int.Parse(User.FindFirst("Id").Value);
        return Ok(await _messageService.AddMessageAsync(messageDto, userId));
    }
    
    [HttpGet("GetMessageByIdAsync/{messageId}")]
    public async Task<IActionResult> GetMessageByIdAsync(int messageId)
    {
        return Ok(await _messageService.GetMessageByIdAsync(messageId));
    }
    
    [HttpGet("GetConversationMessages/{conversationId}")]
    public async Task<IActionResult> GetConversationMessages(int conversationId)
    {
        var userId = int.Parse(User.FindFirst("Id").Value);
        return Ok(await _messageService.GetConversationMessages(conversationId, userId));
    }
}