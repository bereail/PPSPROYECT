using MiniMarket_API.Model.Entities;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Model.Enums;
using MiniMarket_API.Application.ViewModels;
using MiniMarket_API.Application.DTOs.Requests.Credentials;

namespace MiniMarket_API.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserView?> CreateUser(User user, string passwordToHash);
        Task<UserView?> UpdateUser(Guid id, UpdateUserDto updateUserDto);
        Task<UserView?> DeactivateUser(Guid id);
        Task<UserView?> RestoreUser(Guid id);
        Task SetNewUserPassword(Guid id, NewPasswordRequestDto newPasswordRequest);
        Task EraseUser(Guid id);
        Task<IEnumerable<UserView>?> GetAllUsers(bool? isActive, string? filterOn, string? filterQuery,
            string? sortBy, bool? isAscending, int pageNumber, int pageSize);
        Task<UserView?> GetUserById(Guid id);
        Task<UserViewProfile?> GetUserProfileById(Guid id, OrderStatus? status, string? sortBy, bool? isAscending,
            int pageNumber, int pageSize);
    }
}
