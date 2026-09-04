using Microsoft.AspNetCore.Mvc;
using Synapse.Application.Interfaces.ServiceInterfaces;

namespace Synapse.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
   private readonly IUserService _userService;

   public UserController(IUserService userService)
   {
      _userService = userService;
   }

   [HttpGet("GetUserProfile")]
   public async Task<IActionResult> GetUserProfile()
   {
      var userId = int.Parse(User.FindFirst("Id").Value);
      return Ok(await _userService.GetUserById(userId));
   }
   
   [HttpGet("GetUserById")]
   public async Task<IActionResult> GetUserById(int userId)
   {
      return Ok(await _userService.GetUserById(userId));
   }
   
   [HttpGet("GetUserByEmail")]
   public async Task<IActionResult> GetUserByEmail(string userEmail)
   {
      return Ok(await _userService.GetUserByEmail(userEmail));
   }

   [HttpGet("GetUserByUsername")]
   public async Task<IActionResult> GetUserByUsername(string username)
   {
      return Ok(await _userService.GetUserByUsername(username));
   }

   [HttpGet("GetAllUsers")]
   public async Task<IActionResult> GetAllUsers()
   {
      return Ok(await _userService.GetAllUsers());
   }
   
   [HttpGet("GetAllUserFriends")]
   public async Task<IActionResult> GetAllUserFriends()
   {
      var userId = int.Parse(User.FindFirst("Id").Value);
      return Ok(await _userService.GetAllUserFriends(userId));
   }
   
}