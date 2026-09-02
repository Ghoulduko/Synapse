using Synapse.Application.Dtos.Authentication;
using Synapse.Core.Models;

namespace Synapse.Application.Interfaces.ServiceInterfaces;

public interface IAuthenticationService
{
    Task<Result<LoginResponseDto>> Register(RegisterRequestDto request);
    Task<Result<LoginResponseDto>> Login(LoginRequestDto request);
    Task<Result<LoginResponseDto>> Logout();
}