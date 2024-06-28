using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User?> UpdateUserAsync(Guid id, User user);
        Task SetNewUserPasswordAsync(Guid id, byte[] password);
        Task<User?> DeactivateUserAsync(Guid id);
        Task<User?> RestoreUserAsync(Guid id);
        Task EraseUserAsync(Guid id);
        Task<IEnumerable<User>> GetAllUsersAsync(bool? isActive, string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 30);
        Task<User?> GetUserByIdAsync(Guid id);
        Task<Seller?> GetSellerByIdAsync(Guid id);
        Task<Guid?> CheckIfUserIdExistsAsync(Guid id);
        Task<Guid?> GetUserIdByEmailAsync(string email);
        Task<User?> GetUserByEmailAsync(string email);
        Task<ICollection<string>> GetAllCustomerEmailsAsync();
    }
}
