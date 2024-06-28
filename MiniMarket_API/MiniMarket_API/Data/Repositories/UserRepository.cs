using Microsoft.EntityFrameworkCore;
using MiniMarket_API.Data.Interfaces;
using MiniMarket_API.Model;
using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MarketDbContext _context;

        public UserRepository(MarketDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            user.Id = Guid.NewGuid();
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateUserAsync(Guid id, User user)
        {
            var getUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (getUser == null)
            {
                return null;
            }

            getUser.Name = user.Name;
            getUser.PhoneNumber = user.PhoneNumber;

            await _context.SaveChangesAsync();

            return getUser;
        }

        public async Task SetNewUserPasswordAsync(Guid id, byte[] password)
        {
            var getUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (getUser == null)
            {
                return;
            }

            getUser.PasswordHash = password;

            await _context.SaveChangesAsync();
        }

        public async Task<User?> DeactivateUserAsync(Guid id)
        {
            var getUserToDeactivate = await _context.Users.FirstOrDefaultAsync(x => x.Id == id && x.IsActive
            && x.UserType != typeof(SuperAdmin).Name);
            if (getUserToDeactivate == null)
            {
                return null;
            }
            getUserToDeactivate.IsActive = false;
            getUserToDeactivate.DeactivationTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return getUserToDeactivate;
        }

        public async Task<User?> RestoreUserAsync(Guid id)
        {
            var getUserToRestore = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (getUserToRestore == null)
            {
                return null;
            }

            getUserToRestore.IsActive = true;
            getUserToRestore.DeactivationTime = null;
            await _context.SaveChangesAsync();
            return getUserToRestore;
        }


        public async Task EraseUserAsync(Guid id)
        {
            var getUserToErase = await _context.Users.FirstOrDefaultAsync(x => x.Id == id && !x.IsActive);
            if (getUserToErase == null)
            {
                return;
            }
            _context.Users.Remove(getUserToErase);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(bool? isActive, string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 30)
        {
            //We define users as Queryable
            var users = _context.Users.AsQueryable();

            //Filtering by isActive using Query. In case of no bool value, it should return all states.
            //Only Admins should be able to set it to anything other than true.
            if (isActive != null)
            {
                users = isActive.Value ? users.Where(x => x.IsActive) : users.Where(x => !x.IsActive);
            }

            //Filtering by name or type using Queryable
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    //Returns all users whose's name contain what we sent in the Filter Query
                    users = users.Where(u => u.Name.Contains(filterQuery));
                }

                else if (filterOn.Equals("Role", StringComparison.OrdinalIgnoreCase))
                {
                    //Returns all users whose's role contains what we sent in the Filter Query
                    users = users.Where(u => u.UserType.Contains(filterQuery));
                }
            }

            //Sorting the users using Queryable
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    //Returns all users in a new order by name. If isAscending is true, they will be by ascending order, else, descending. 
                    users = isAscending ? users.OrderBy(p => p.Name) : users.OrderByDescending(p => p.Name);
                }
            }

            //Pagination of users using Queryable

            var skipResults = (pageNumber - 1) * pageSize;      //If this results in 0, it will skip it. 

            return await users.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public Task<User?> GetUserByIdAsync(Guid id)
        {
            return _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Seller?> GetSellerByIdAsync(Guid id)
        {
            return _context.Sellers
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Guid?> CheckIfUserIdExistsAsync(Guid id)
        {
            var userId = await _context.Users
                .Where(u => u.Id == id)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();
            return userId;                                              //Checks if the User's ID already exists in the db. If it does, it will return the ID, else it will be null.
        }

        public async Task<Guid?> GetUserIdByEmailAsync(string email)
        {
            var userId = await _context.Users
                .Where(u => u.Email == email)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();
            return userId;                                              //Checks if the User's Email already exists in the db. If it does, it will return the User's ID, else it will be null.
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users
                .Where(u => u.Email == email && u.IsActive)
                .FirstOrDefaultAsync();

            return user;                                              
        }

        public async Task<ICollection<string>> GetAllCustomerEmailsAsync()
        {
            var emails = await _context.Users
                .Where(u => u.IsActive && u.UserType == typeof(Customer).Name)
                .Select(u => u.Email)
                .ToListAsync();

            return emails;
        }
    }
}
