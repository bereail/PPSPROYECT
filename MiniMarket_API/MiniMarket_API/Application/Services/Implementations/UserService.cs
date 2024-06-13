using AutoMapper;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Application.ViewModels;
using MiniMarket_API.Data.Interfaces;
using MiniMarket_API.Model.Entities;
using MiniMarket_API.Model.Enums;

namespace MiniMarket_API.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper mapper;
        private readonly ISaleOrderService _saleOrderService;
        private readonly ISaleOrderRepository _saleOrderRepository;

        public UserService(IUserRepository userRepository, IMapper mapper, ISaleOrderService saleOrderService, ISaleOrderRepository saleOrderRepository)
        {
            _userRepository = userRepository;
            this.mapper = mapper;
            _saleOrderService = saleOrderService;
            _saleOrderRepository = saleOrderRepository;
        }

        public async Task<UserView?> CreateUser(User user)
        {
            var checkMail = await _userRepository.GetUserIdByEmailAsync(user.Email);
            if (checkMail == Guid.Empty)
            {
                var newUser = await _userRepository.CreateUserAsync(user);

                return mapper.Map<UserView>(newUser);
            }
            return null;
        }

        public async Task<UserView?> UpdateUser(Guid id, UpdateUserDto updateUserDto)
        {
            //It looks sus, but I'm only using this Customer instance to pass the data to the UserRepository.
            //AutoMapper cannot create an instance of an abstract (in this case, User) class, so I decided to simply pass the data through a children class (Customer).
            var userToUpdate = mapper.Map<Customer>(updateUserDto);

            var updatedUser = await _userRepository.UpdateUserAsync(id, userToUpdate);

            if (updatedUser == null)
            {
                return null;
            }
            return mapper.Map<UserView>(updatedUser);
        }

        public async Task<UserView?> DeactivateUser(Guid id)
        {
            var userToDeactivate = await _userRepository.DeactivateUserAsync(id);


            if (userToDeactivate == null)
            {
                return null;
            }

            var ordersToCancel = await _saleOrderRepository.GetPendingOrderIdsByUserIdAsync(id);

            foreach (var orderId in ordersToCancel)
            {
                await _saleOrderService.CancelOrder(orderId, id);
                continue;
            }

            return mapper.Map<UserView?>(userToDeactivate);
        }

        public async Task<UserView?> EraseUser(Guid id)
        {
            var userToErase = await _userRepository.EraseUserAsync(id);
            if (userToErase == null)
            {
                return null;
            }
            return mapper.Map<UserView?>(userToErase);
        }

        public async Task<IEnumerable<UserView>?> GetAllUsers(bool? isActive, string? filterOn, string? filterQuery,
            string? sortBy, bool? isAscending, int pageNumber, int pageSize)
        {
            var users = await _userRepository.GetAllUsersAsync(isActive, filterOn, filterQuery, sortBy, isAscending ?? true,
                pageNumber, pageSize);
            if (!users.Any())
            {
                return null;
            }
            return mapper.Map<IEnumerable<UserView>>(users);
        }

        public async Task<UserView?> GetUserById(Guid id)
        {
            var getUser = await _userRepository.GetUserByIdAsync(id);
            if (getUser == null)
            {
                return null;
            }
            return mapper.Map<UserView?>(getUser);
        }

        public async Task<UserViewProfile?> GetUserProfileById(Guid id, OrderStatus? status, string? sortBy, bool? isAscending,
            int pageNumber, int pageSize)
        {
            var getUser = await _userRepository.GetUserByIdAsync(id);

            if (getUser == null)
            {
                return null;
            }
            var userProfile = mapper.Map<UserViewProfile?>(getUser);

            var userOrders = await _saleOrderService.GetAllOrdersByUser(id, status, sortBy, isAscending ?? true, pageNumber, pageSize);
            if (userOrders == null)
            {
                return userProfile;
            }

            userProfile.SaleOrders = userOrders.ToList();

            return userProfile;
        }
    }
}
