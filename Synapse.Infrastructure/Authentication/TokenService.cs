using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Synapse.Application.Dtos.Authentication;
using Synapse.Application.Interfaces.RepositoryInterfaces;
using Synapse.Application.Interfaces.ServiceInterfaces;
using Synapse.Core.Entities;
using Synapse.Core.Models;

namespace Synapse.Infrastructure.Authentication;

public class TokenService : ITokenService
{
    
    // private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    
    public TokenService(
        // IUserRepository userRepository,
        IConfiguration configuration)
    {
        // _userRepository = userRepository;
        _configuration = configuration;
    }
    
    public async Task<LoginResponseDto> GenerateJwtToken(User user)
    {
        var key = Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]);
        var issuer = _configuration["JwtConfig:Issuer"];
        var audience = _configuration["JwtConfig:Audience"];
        var tokenValidityMins = int.Parse(_configuration["JwtConfig:JwtTokenValidityMins"]);
        var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

        var claims = new List<Claim>
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("Email", user.Email),
            new Claim("Username", user.Username),
        };
        
        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: tokenExpiryTimeStamp,
            signingCredentials: credentials
        );
        
        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new LoginResponseDto
        {
            Username = user.Username,
            AccessToken = accessToken,
            Expires = tokenExpiryTimeStamp,
        };
    }

    public async Task<string> GenerateRefreshToken()
    {
        throw new NotImplementedException();
    }
    
    public Task<Result<LoginResponseDto>> RotateRefreshToken(string refreshToken)
    {
        throw new NotImplementedException();
    }
}