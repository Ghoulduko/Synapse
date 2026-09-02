using Synapse.Application.Dtos;
using Synapse.Application.Interfaces.ServiceInterfaces;
using Synapse.Core.Models;

namespace Synapse.Application.Services;

public class UserService : IUserService
{
    public Task<Result<UserDto>> GetUserById(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<UserDto>> GetUserByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserDto>> GetAllUserFriends(int userId)
    {
        throw new NotImplementedException();
    }
}