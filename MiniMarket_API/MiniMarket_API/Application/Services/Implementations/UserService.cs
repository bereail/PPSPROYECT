using AutoMapper;
using MiniMarket_API.Application.DTOs;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Data.Interfaces;
using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper mapper;
        private readonly ISaleOrderService _saleOrderService;

        public UserService(IUserRepository userRepository, IMapper mapper, ISaleOrderService saleOrderService)
        {
            _userRepository = userRepository;
            this.mapper = mapper;
            _saleOrderService = saleOrderService;
        }

        public async Task<UserDto?> CreateUser(User user)
        {
            var checkMail = await _userRepository.GetUserIdByEmailAsync(user.Email);
            if (checkMail == Guid.Empty)
            {
                var newUser = await _userRepository.CreateUserAsync(user);

                return mapper.Map<UserDto>(newUser);
            }
            return null;
        }

        public async Task<UserDto?> UpdateUser(Guid id, UpdateUserDto updateUserDto)
        {
            var userToUpdate = mapper.Map<User>(updateUserDto);

            userToUpdate = await _userRepository.UpdateUserAsync(id, userToUpdate);

            if (userToUpdate == null)
            {
                return null;
            }
            return mapper.Map<UserDto>(userToUpdate);
        }

        public async Task<UserDto?> DeactivateUser(Guid id)
        {
            var userToDeactivate = await _userRepository.DeactivateUserAsync(id);
            if (userToDeactivate == null)
            {
                return null;
            }
            return mapper.Map<UserDto?>(userToDeactivate);
        }

        public async Task<UserDto?> EraseUser(Guid id)
        {
            var userToErase = await _userRepository.EraseUserAsync(id);
            if (userToErase == null)
            {
                return null;
            }
            return mapper.Map<UserDto?>(userToErase);
        }

        public async Task<IEnumerable<UserDto>?> GetAllUsers(bool? isActive, string? filterOn, string? filterQuery,
            string? sortBy, bool? isAscending, int pageNumber, int pageSize)
        {
            var users = await _userRepository.GetAllUsersAsync(isActive, filterOn, filterQuery, sortBy, isAscending ?? true,
                pageNumber, pageSize);
            if (!users.Any())
            {
                return null;
            }
            return mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto?> GetUserById(Guid id)
        {
            var getUser = await _userRepository.GetUserByIdAsync(id);
            if (getUser == null)
            {
                return null;
            }
            return mapper.Map<UserDto?>(getUser);
        }

        public async Task<UserProfileDto?> GetUserProfileById(Guid id, string? sortBy, bool? isAscending,
            int pageNumber, int pageSize)
        {
            var getUser = await _userRepository.GetUserByIdAsync(id);

            if (getUser == null)
            {
                return null;
            }
            var userProfile = mapper.Map<UserProfileDto?>(getUser);

            var userOrders = await _saleOrderService.GetAllOrdersByUser(id, sortBy, isAscending ?? true, pageNumber, pageSize);
            if (userOrders == null)
            {
                return userProfile;
            }

            userProfile.SaleOrders = userOrders.ToList();

            return userProfile;
        }
    }
}
