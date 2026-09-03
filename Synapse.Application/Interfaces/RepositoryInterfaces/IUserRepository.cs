using Synapse.Core.Entities;

namespace Synapse.Application.Interfaces.RepositoryInterfaces;

public interface IUserRepository
{
    Task Create(User user);
    Task<User?> GetUserById(int userId);
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserByUsername(string username);
    Task<IEnumerable<User>> GetAllUsers();
}