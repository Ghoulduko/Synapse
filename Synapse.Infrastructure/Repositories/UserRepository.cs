using Microsoft.EntityFrameworkCore;
using Synapse.Application.Interfaces.RepositoryInterfaces;
using Synapse.Core.Entities;
using Synapse.Infrastructure.Database;

namespace Synapse.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SynapseDbContext _context;

    public UserRepository(SynapseDbContext context)
    {
        _context = context;
    }

    public async Task Create(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserById(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }
}