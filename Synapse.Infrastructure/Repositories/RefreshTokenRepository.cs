using Microsoft.EntityFrameworkCore;
using Synapse.Application.Interfaces.RepositoryInterfaces;
using Synapse.Core.Entities;
using Synapse.Infrastructure.Database;

namespace Synapse.Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly SynapseDbContext _context;

    public RefreshTokenRepository(SynapseDbContext context)
    {
        _context = context;
    }

    public async Task CreateRefreshTokenAsync(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken)
    {
        return await _context.RefreshTokens.SingleOrDefaultAsync(t => t.TokenHash == refreshToken);
    }

    public async Task DeleteRefreshTokenAsync(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Remove(refreshToken);
        await _context.SaveChangesAsync();
    }
}