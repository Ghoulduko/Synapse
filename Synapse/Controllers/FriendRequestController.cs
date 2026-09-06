using Microsoft.AspNetCore.Mvc;
using Synapse.Application.Dtos;
using Synapse.Application.Interfaces.ServiceInterfaces;
using Synapse.Core.Entities;

namespace Synapse.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FriendRequestController : Controller
{
    private readonly IFriendRequestService _friendRequestService;

    public FriendRequestController(IFriendRequestService friendRequestService)
    {
        _friendRequestService = friendRequestService;
    }

    [HttpPost("SendFriendRequest/{receiverId}")]
    public async Task<IActionResult> SendFriendRequest(int receiverId)
    {
        var senderId = int.Parse(User.FindFirst("Id").Value);
        return Ok(await _friendRequestService.CreateFriendRequest(senderId, receiverId));
    }
    
    [HttpGet("GetUserFriendRequests")]
    public async Task<IActionResult> GetUserFriendRequests()
    {
        var userId = int.Parse(User.FindFirst("Id").Value);
        return Ok(await _friendRequestService.GetUserFriendRequestSenderAsync(userId));
    }
    
    [HttpPut("AcceptFriendRequest/{senderId}")]
    public async Task<IActionResult> AcceptFriendRequest(int senderId)
    {
        var receiverId = int.Parse(User.FindFirst("Id").Value);
        return Ok(await _friendRequestService.UpdateFriendRequest(senderId, receiverId));
    }
    
    [HttpDelete("DeclineFriendRequest/{senderId}")]
    public async Task<IActionResult> DeclineFriendRequest(int senderId)
    {
        var receiverId = int.Parse(User.FindFirst("Id").Value);
        return Ok(await _friendRequestService.DeclineFriendRequest(senderId, receiverId));
    }
}