using MiniMarket_Server_dev.Application.DTOs.Requests;
using MiniMarket_Server_dev.Application.DTOs;
using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> CreateUser(User user);
        Task<UserDto?> UpdateUser(Guid id, UpdateUserDto updateUserDto);
        Task<UserDto?> DeactivateUser(Guid id);
        Task<UserDto?> EraseUser(Guid id);
        Task<IEnumerable<UserDto>?> GetAllUsers(bool? isActive, string? filterOn, string? filterQuery,
            string? sortBy, bool? isAscending, int pageNumber, int pageSize);
        Task<UserDto?> GetUserById(Guid id);
        Task<UserProfileDto?> GetUserProfileById(Guid id, bool? isActive, string? sortBy, bool? isAscending,
            int pageNumber, int pageSize);
    }
}
