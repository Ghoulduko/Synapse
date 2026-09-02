using Microsoft.AspNetCore.Mvc;
using Synapse.Application.Dtos.Authentication;
using Synapse.Application.Interfaces.ServiceInterfaces;

namespace Synapse.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly IAuthenticationService _authenticationService;

    public AuthController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        return Ok(await _authenticationService.Register(request));
    }
}