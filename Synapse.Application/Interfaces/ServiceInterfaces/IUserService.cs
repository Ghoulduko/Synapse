using Synapse.Application.Dtos;
using Synapse.Core.Models;

namespace Synapse.Application.Interfaces.ServiceInterfaces;

public interface IUserService
{
    Task<Result<UserDto>> GetUserById(int userId);
    Task<Result<UserDto>> GetUserByEmail(string email);
    Task<Result<UserDto>> GetUserByUsername(string username);
    Task<IEnumerable<UserDto>> GetAllUsers();
    Task<IEnumerable<UserDto>> GetAllUserFriends(int userId);
}