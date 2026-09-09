using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Synapse.Application.Dtos.Authentication;
using Synapse.Application.Interfaces.RepositoryInterfaces;
using Synapse.Application.Interfaces.ServiceInterfaces;
using Synapse.Core.Entities;
using Synapse.Core.Models;
using Synapse.Infrastructure.ExtensionMethods;

namespace Synapse.Infrastructure.Authentication;

public class TokenService : ITokenService
{
    
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    
    public TokenService(IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository, IConfiguration configuration)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userRepository;
        _configuration = configuration;
    }
    
    public async Task<Result<LoginResponseDto>> GenerateJwtToken(User user)
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

        return new Result<LoginResponseDto>
        {
            Success = true,
            Data = new LoginResponseDto
            {
                Username = user.Username,
                AccessToken = accessToken,
                Expires = tokenExpiryTimeStamp,
                RefreshToken = await GenerateRefreshToken(user.Id)
            }
        };
    }

    public async Task<string> GenerateRefreshToken(int userId)
    {
        var refreshTokenValidityMins = int.Parse(_configuration["JwtConfig:RefreshTokenValidityMins"]);

        var bytes = RandomNumberGenerator.GetBytes(64);
        var token = Convert.ToBase64String(bytes);

        var refreshToken = new RefreshToken
        {
            TokenHash = token.HashToken(),
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(refreshTokenValidityMins),
        };
        
        await _refreshTokenRepository.CreateRefreshTokenAsync(refreshToken);
        return token;
    }
    
    public async Task<Result<LoginResponseDto>> RotateRefreshToken(string refreshToken)
    {
        var hashedToken = refreshToken.HashToken();
        var token = await _refreshTokenRepository.GetRefreshTokenAsync(hashedToken);

        if (token == null)
        {
            return new Result<LoginResponseDto>
            {
                Success = false,
                Message = "Invalid refresh token"
            };
        }
        
        var user = await _userRepository.GetUserById(token.UserId);
        if (user == null)
        {
            return new Result<LoginResponseDto>
            {
                Success = false,
                Message = "User not found"
            };
        }
        
        await _refreshTokenRepository.DeleteRefreshTokenAsync(token);
        return await GenerateJwtToken(user);
    }
}