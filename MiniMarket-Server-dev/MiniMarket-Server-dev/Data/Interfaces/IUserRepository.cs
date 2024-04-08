using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User?> UpdateUserAsync(Guid id, User user);
        Task<User?> DeactivateUserAsync(Guid id);
        Task<User?> EraseUserAsync(Guid id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetByIdAsync(Guid id);


    }
}
