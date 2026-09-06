using Microsoft.AspNetCore.Mvc;
using Synapse.Application.Dtos.Authentication;
using Synapse.Application.Interfaces.ServiceInterfaces;

namespace Synapse.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ITokenService _tokenService;

    public AuthController(IAuthenticationService authenticationService, ITokenService tokenService)
    {
        _authenticationService = authenticationService;
        _tokenService = tokenService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        return Ok(await _authenticationService.Register(request));
    }
    
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        return Ok(await _authenticationService.Login(request));
    }
    
    [HttpPost("RotateRefreshToken")]
    public async Task<IActionResult> RotateRefreshToken(string refreshToken)
    {
        return Ok(await _tokenService.RotateRefreshToken(refreshToken));
    }
}

// {
// "password": "Yaryara910",
// "email": "L.Karkarashvili8@gmail.com"
// }

// =*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=

// {
// "password": "Saba11_11",
// "email": "sabakapanadze80@gmail.com"
// }