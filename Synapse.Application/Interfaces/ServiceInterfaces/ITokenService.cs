using Synapse.Application.Dtos.Authentication;
using Synapse.Core.Entities;
using Synapse.Core.Models;

namespace Synapse.Application.Interfaces.ServiceInterfaces;

public interface ITokenService
{
    Task<LoginResponseDto> GenerateJwtToken(User user);
    Task<Result<LoginResponseDto>> RotateRefreshToken(string refreshToken);
}