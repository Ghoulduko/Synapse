using Microsoft.AspNetCore.Mvc;
using Synapse.Application.Interfaces.ServiceInterfaces;

namespace Synapse.Controllers;

[ApiController]
[Route("api/conversation")]
public class ConversationController : Controller
{
    private readonly IConversationService _conversationService;

    public ConversationController(IConversationService conversationService)
    {
        _conversationService = conversationService;
    }

    [HttpPost("CreateConversation")]
    public async Task<IActionResult> CreateConversation(List<int> userIds)
    {
        return Ok(await _conversationService.CreateConversation(userIds));
    }

    [HttpGet("GetConversationById")]
    public async Task<IActionResult> GetConversationById(int userId)
    {
        return Ok(await _conversationService.GetConversationById(userId));
    }
    
    [HttpGet("GetUserConversations")]
    public async Task<IActionResult> GetUserConversations()
    {
        var userId = int.Parse(User.FindFirst("Id").Value);
        return Ok(await _conversationService.GetConversationById(userId));
    }
    
}