using Microsoft.EntityFrameworkCore;
using MiniMarket_Server_dev.Data.Interfaces;
using MiniMarket_Server_dev.Model;
using MiniMarket_Server_dev.Model.DTOs;
using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MarketDbContext _context;

        public UserRepository (MarketDbContext context)
        {
            _context = context;
        }

        public int CreateUser(UserDto userDto)

        {
            try
            {
                //verifica si el usuario ya existe
                
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == userDto.Email);

                if (existingUser == null)
                {
                    User newUser = new User
                    {
                        Name = userDto.Name,
                        Email = userDto.Email,
                        PhoneNumber = userDto.PhoneNumber,
                        Address = userDto.Address,
                        UserType = userDto.UserType
                    };
                    _context.Users.Add(newUser);
                    _context.SaveChanges();
                    return existingUser.Id;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error al crear un usuario: {ex.Message}");
                throw;
            }
         }
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
            getUser.Email = user.Email;
            getUser.PhoneNumber = user.PhoneNumber;
            getUser.Address = user.Address;

            await _context.SaveChangesAsync();

            return getUser;
        }

        public async Task<User?> DeactivateUserAsync(Guid id)
        {
            var getUserToDeactivate = await _context.Users.FirstOrDefaultAsync (x => x.Id == id);
            if (getUserToDeactivate == null)
            {
                return null;
            }
            getUserToDeactivate.IsActive = false;
            getUserToDeactivate.DeactivationTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return getUserToDeactivate;
        }


        public async Task<User?> EraseUserAsync(Guid id)
        {
            var getUserToErase = await _context.Users.FirstOrDefaultAsync (x =>x.Id == id && !x.IsActive);
            if (getUserToErase == null)
            {
                return null;
            }
            _context.Users.Remove(getUserToErase);
            await _context.SaveChangesAsync();
            return getUserToErase;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await
                _context.Users
                .ToListAsync();
        }

        public Task<User?> GetByIdAsync(Guid id)
        {
            return _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }

