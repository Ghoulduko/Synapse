using Synapse.Core.Entities;

namespace Synapse.Application.Interfaces.RepositoryInterfaces;

public interface IRefreshTokenRepository
{
    Task CreateRefreshTokenAsync(RefreshToken refreshToken);
    Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken);
    Task DeleteRefreshTokenAsync(RefreshToken refreshToken);
}