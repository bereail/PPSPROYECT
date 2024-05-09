using MiniMarket_API.Application.DTOs;
using MiniMarket_API.Model.Entities;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Model.Enums;

namespace MiniMarket_API.Application.Services.Interfaces
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
        Task<UserProfileDto?> GetUserProfileById(Guid id, string? sortBy, bool? isAscending,
            int pageNumber, int pageSize);
    }
}
