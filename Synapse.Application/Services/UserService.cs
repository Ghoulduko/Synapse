using Synapse.Application.Dtos;
using Synapse.Application.Interfaces;
using Synapse.Application.Interfaces.RepositoryInterfaces;
using Synapse.Application.Interfaces.ServiceInterfaces;
using Synapse.Core.Models;

namespace Synapse.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserDto>> GetUserById(int userId)
    {
        var user = await _unitOfWork.UserRepository.GetUserById(userId);
        if (user == null)
        {
            return new Result<UserDto>
            {
                Success = false,
                Message = "User not found"
            };
        }

        return new Result<UserDto>
        {
            Success = true,
            Data = new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
            }
        };
    }

    public async Task<Result<UserDto>> GetUserByEmail(string email)
    {
        var user = await _unitOfWork.UserRepository.GetUserByEmail(email);
        if (user == null)
        {
            return new Result<UserDto>
            {
                Success = false,
                Message = "User not found"
            };
        }

        return new Result<UserDto>
        {
            Success = true,
            Data = new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
            }
        };
    }

    public async Task<Result<UserDto>> GetUserByUsername(string username)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsername(username);
        if (user == null)
        {
            return new Result<UserDto>
            {
                Success = false,
                Message = "User not found"
            };
        }

        return new Result<UserDto>
        {
            Success = true,
            Data = new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
            }
        };
    }

    public async Task<IEnumerable<UserDto>> GetAllUsers()
    {
        var allUsers = await _unitOfWork.UserRepository.GetAllUsers();
        return allUsers.Select(user => new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
        });
    }
    
    public async Task<IEnumerable<UserDto>> GetAllUserFriends(int userId)
    {
        var userFriends = await _unitOfWork.FriendRequestRepository.GetAllUserFriends(userId);
        return userFriends.Select(u => new UserDto
        {
            Id = u.Id,
            Email = u.Email,
            Username = u.Username,
        });
    }
}