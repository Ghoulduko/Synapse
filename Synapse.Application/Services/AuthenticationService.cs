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

namespace Synapse.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;
    
    // Validators
    private readonly IValidator<RegisterRequestDto> _registerValidator;
    private readonly IValidator<LoginRequestDto> _loginValidator;
    
    public AuthenticationService(
        IUserRepository userRepository,
        IConfiguration configuration,
        ITokenService tokenService,
        IValidator<RegisterRequestDto> registerValidator,
        IValidator<LoginRequestDto> loginValidator)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _tokenService = tokenService;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
    }
    
    public async Task<Result<LoginResponseDto>> Register(RegisterRequestDto request)
    {
        await _registerValidator.ValidateAndThrowAsync(request);
        string normalizedEmail = request.Email.Trim().ToLower();
        var existingUser = await _userRepository.GetUserByEmail(normalizedEmail);
        if (existingUser != null)
        {
            return new Result<LoginResponseDto>
            {
                Success = false,
                Message = "User with that email already exists.",
            };
        }

        var newUser = new User
        {
            Username = request.Username,
            Email = request.Email.Trim().ToLower(),
            PasswordHash = BC.HashPassword(request.Password, 7),
        };
        
        await _userRepository.Create(newUser);
        
        return await _tokenService.GenerateJwtToken(newUser);
    }
    
    public async Task<Result<LoginResponseDto>> Login(LoginRequestDto request)
    {
        await _loginValidator.ValidateAndThrowAsync(request);
        
        var normalizedEmail = request.Email.Trim().ToLower();
        var user = await _userRepository.GetUserByEmail(normalizedEmail);

        if (user == null)
        {
            return new Result<LoginResponseDto>
            {
                Success = false,
                Message = "User not found.",
            };
        }

        return await _tokenService.GenerateJwtToken(user);
    }
    
    public Task<Result<LoginResponseDto>> Logout()
    {
        throw new NotImplementedException();
    }
}