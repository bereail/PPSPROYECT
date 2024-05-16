using AutoMapper;
using MiniMarket_API.Application.DTOs;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Data.Interfaces;
using MiniMarket_API.Model.Entities;
using MiniMarket_API.Model.Enums;

namespace MiniMarket_API.Application.Services.Implementations
{
    public class SaleOrderService : ISaleOrderService
    {
        private readonly ISaleOrderRepository _saleOrderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrderDetailsService _orderDetailsService;
        private readonly IMapper mapper;

        public SaleOrderService(ISaleOrderRepository saleOrderRepository, IMapper mapper, IOrderDetailsService orderDetailsService, IUserRepository userRepository)
        {
            _saleOrderRepository = saleOrderRepository;
            this.mapper = mapper;
            _orderDetailsService = orderDetailsService;
            _userRepository = userRepository;
        }

        public async Task<SaleOrderDetailsDto?> CreateSaleOrder(CreateOrderDto createOrderDto, Guid userId)
        {
            var orderToCreate = mapper.Map<SaleOrder>(createOrderDto);

            //This check should maybe be removed once claims are activated.
            Guid? userCheck = await _userRepository.CheckIfUserIdExistsAsync(userId);

            if (userCheck == null)
            {
                return null;
            }

            orderToCreate.UserId = userId;

            orderToCreate = await _saleOrderRepository.CreateOrderAsync(orderToCreate);

            decimal finalOrderPrice = 0;

            //Upon Order creation, the request will include at least 1 Detail. These Details will then be processed here.
            var detailsToCreate = createOrderDto.NewDetails;

            detailsToCreate = GroupDetailsByProductId(detailsToCreate);

            foreach (var detail in detailsToCreate)
            {
                var createdDetails = await _orderDetailsService.CreateOrderDetail(detail, orderToCreate.Id);
                if (createdDetails != null)
                {
                    finalOrderPrice = finalOrderPrice + createdDetails.DetailPrice;
                }
                continue;
            }
            //Performs the price update on the recently created order. 
            await _saleOrderRepository.SetFinalOrderPriceAsync(orderToCreate.Id, finalOrderPrice);
            orderToCreate.FinalPrice = finalOrderPrice;                                              //This is for the result.

            return mapper.Map<SaleOrderDetailsDto>(orderToCreate);
        }

        public async Task<SaleOrderDetailsDto?> CancelOrder(Guid id, int cancelStatus = 2)
        {
            var orderToCancel = await _saleOrderRepository.SetOrderStatusAsync(id, cancelStatus);
            if (orderToCancel == null)
            {
                return null;
            }
            foreach (var detail in orderToCancel.Details)
            {
                await _orderDetailsService.EraseOrderDetail(detail.Id);
            }
            return mapper.Map<SaleOrderDetailsDto?>(orderToCancel);
        }

        public async Task<SaleOrderDetailsDto?> PayOrder(Guid id, int paymentStatus = 1)
        {
            var orderToPay = await _saleOrderRepository.GetOrderByIdAsync(id);
            if (orderToPay == null || orderToPay.Details.Count == 0)
            {
                return null;
            }

            //MP Service Logic goes here.

            //When successful, the SetOrderStatus method will go through
            orderToPay = await _saleOrderRepository.SetOrderStatusAsync(id, paymentStatus);

            return mapper.Map<SaleOrderDetailsDto>(orderToPay);
        }

        public async Task<SaleOrderDto?> EraseOrder(Guid id)
        {
            var orderToErase = await _saleOrderRepository.EraseOrderAsync(id);
            if (orderToErase == null)
            {
                return null;
            }
            return mapper.Map<SaleOrderDto?>(orderToErase);
        }

        public async Task<IEnumerable<SaleOrderDto>?> GetAllOrders(string? sortBy, bool? isAscending,
            int pageNumber, int pageSize)
        {
            if (pageNumber < 1) { pageNumber = 1; }
            if (pageSize < 1) { pageSize = 1; }

            var getOrders = await _saleOrderRepository.GetAllOrders(sortBy, isAscending ?? true, pageNumber, pageSize);
            if (!getOrders.Any())
            {
                return null;
            }
            return mapper.Map<IEnumerable<SaleOrderDto>>(getOrders);
        }

        public async Task<IEnumerable<SaleOrderDto>?> GetAllOrdersByUser(Guid id, string? sortBy, bool? isAscending,
            int pageNumber, int pageSize)
        {
            if (pageNumber < 1) { pageNumber = 1; }
            if (pageSize < 1) { pageSize = 1; }

            var getOrders = await _saleOrderRepository.GetAllOrdersByUserAsync(id, sortBy, isAscending ?? true, pageNumber, pageSize);
            if (!getOrders.Any())
            {
                return null;
            }
            return mapper.Map<IEnumerable<SaleOrderDto>>(getOrders);
        }

        public async Task<IEnumerable<SaleOrderDto>?> GetAllOrdersByTimeframe(int? filterDays,
            string? sortBy, bool? isAscending,
            int pageNumber, int pageSize)
        {
            if (pageNumber < 1) { pageNumber = 1; }
            if (pageSize < 1 || pageSize > 15) { pageSize = 7; }

            if (filterDays == null || filterDays < 1 || filterDays > 31)
            {
                var defaultTimeFrame = DateTime.UtcNow.AddDays(-1);

                var getOrdersDefault = await _saleOrderRepository.GetAllOrdersFromTimeframeAsync(defaultTimeFrame, sortBy, isAscending ?? true, pageNumber, pageSize);

                if (!getOrdersDefault.Any())
                {
                    return null;
                }
                return mapper.Map<IEnumerable<SaleOrderDto>?>(getOrdersDefault);
            }

            var timeFrame = DateTime.UtcNow.AddDays(-filterDays.Value);

            var getOrders = await _saleOrderRepository.GetAllOrdersFromTimeframeAsync(timeFrame, sortBy, isAscending ?? true, pageNumber, pageSize);

            if (!getOrders.Any())
            {
                return null;
            }

            return mapper.Map<IEnumerable<SaleOrderDto>?>(getOrders);
        }

        public async Task<SaleOrderDetailsDto?> GetOrderById(Guid id)
        {
            var getOrder = await _saleOrderRepository.GetOrderByIdAsync(id);
            if (getOrder == null)
            {
                return null;
            }
            return mapper.Map<SaleOrderDetailsDto>(getOrder);
        }

        public async Task<Guid> CheckOrderUserId(Guid orderId)
        {
            return await _saleOrderRepository.CheckOrderUserIdAsync(orderId);
        }

        private ICollection<CreateDetailDto> GroupDetailsByProductId(ICollection<CreateDetailDto> details)
        {
            var groupedDetails = details
                .GroupBy(d => d.ProductId)
                .Select(group => new CreateDetailDto { ProductId = group.Key, ProductQuantity = group.Sum(d => d.ProductQuantity) })
                .ToList();

            return groupedDetails;
        }
    }
}
