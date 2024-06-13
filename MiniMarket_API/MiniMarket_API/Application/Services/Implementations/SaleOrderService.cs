using AutoMapper;
using MiniMarket_API.Application.DTOs.Preferences;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Application.ViewModels;
using MiniMarket_API.Data.Interfaces;
using MiniMarket_API.Model.Entities;
using MiniMarket_API.Model.Enums;

namespace MiniMarket_API.Application.Services.Implementations
{
    public class SaleOrderService : ISaleOrderService
    {
        private readonly ISaleOrderRepository _saleOrderRepository;
        private readonly IDeliveryAddressRepository deliveryAddressRepository;
        private readonly IOrderDetailsService _orderDetailsService;
        private readonly IMpOrderService _mpOrderService;
        private readonly IMapper mapper;

        public SaleOrderService(ISaleOrderRepository saleOrderRepository, IMapper mapper,
            IOrderDetailsService orderDetailsService,
            IMpOrderService mpOrderService,
            IDeliveryAddressRepository deliveryAddressRepository)
        {
            _saleOrderRepository = saleOrderRepository;
            this.mapper = mapper;
            _orderDetailsService = orderDetailsService;
            _mpOrderService = mpOrderService;
            this.deliveryAddressRepository = deliveryAddressRepository;
        }

        public async Task<SaleOrderViewDetails?> CreateSaleOrder(CreateOrderDto createOrderDto, Guid userId)
        {
            //If the user doesn't have an existing address currently, he cannot generate a new order.
            var checkExistingAddress = await deliveryAddressRepository.GetAddressIdByUserId(userId);

            if (checkExistingAddress == Guid.Empty)
            {
                return null;
            }

            var orderToCreate = mapper.Map<SaleOrder>(createOrderDto);

            orderToCreate.UserId = userId;
            orderToCreate.AddressId = checkExistingAddress;

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

            return mapper.Map<SaleOrderViewDetails>(orderToCreate);
        }

        public async Task<SaleOrderViewDetails?> CancelOrder(Guid id, Guid userId)
        {
            int cancelStatus = 2;
            var orderToCancel = await _saleOrderRepository.SetOrderStatusAsync(id, userId, cancelStatus);
            if (orderToCancel == null)
            {
                return null;
            }
            foreach (var detail in orderToCancel.Details)
            {
                await _orderDetailsService.EraseOrderDetail(detail.Id);
            }
            return mapper.Map<SaleOrderViewDetails?>(orderToCancel);
        }

        public async Task<PreferenceData?> HandlePaymentRequest(Guid id, Guid userId)
        {
            // Ensures that the order is eligible for payment.
            var orderToPay = await ValidateOrderInfo(id, userId);

            if (orderToPay == null)
            {
                return null;
            }

            // Generates the PreferenceId
            string preferenceId = await _mpOrderService.HandlePreferenceRequest(orderToPay.Details);

            // Object with the data from a successfull preference request
            var requestResult = new PreferenceData
            {
                PreferenceId = preferenceId,
                OrderId = orderToPay.Id,
                UserId = orderToPay.UserId,
            };

            return requestResult;
        }

        public async Task<SaleOrderView?> SetPaidOrderStatus(Guid id, Guid userId)
        {
            int paymentStatus = 1;

            var paidOrder = await ValidateOrderInfo(id, userId);

            if (paidOrder == null)
            {
                return null;
            }

            paidOrder = await _saleOrderRepository.SetOrderStatusAsync(id, userId, paymentStatus);

            return mapper.Map<SaleOrderView?>(paidOrder);
        }

        public async Task<SaleOrderView?> EraseOrder(Guid id)
        {
            var orderToErase = await _saleOrderRepository.EraseOrderAsync(id);
            if (orderToErase == null)
            {
                return null;
            }
            return mapper.Map<SaleOrderView?>(orderToErase);
        }

        public async Task<IEnumerable<SaleOrderView>?> GetAllOrders(OrderStatus? status, string? sortBy, bool? isAscending,
            int pageNumber, int pageSize)
        {
            if (pageNumber < 1) { pageNumber = 1; }
            if (pageSize < 1 || pageSize > 15) { pageSize = 7; }

            var getOrders = await _saleOrderRepository.GetAllOrders(status, sortBy, isAscending ?? true, pageNumber, pageSize);
            if (!getOrders.Any())
            {
                return null;
            }
            return mapper.Map<IEnumerable<SaleOrderView>>(getOrders);
        }

        public async Task<IEnumerable<SaleOrderView>?> GetAllOrdersByUser(Guid id, OrderStatus? status, string? sortBy, bool? isAscending,
            int pageNumber, int pageSize)
        {
            if (pageNumber < 1) { pageNumber = 1; }
            if (pageSize < 1 || pageSize > 15) { pageSize = 7; }

            var getOrders = await _saleOrderRepository.GetAllOrdersByUserAsync(id, status, sortBy, isAscending ?? true, pageNumber, pageSize);
            if (!getOrders.Any())
            {
                return null;
            }
            return mapper.Map<IEnumerable<SaleOrderView>>(getOrders);
        }

        public async Task<IEnumerable<SaleOrderView>?> GetAllOrdersByTimeframe(int? filterDays, OrderStatus? status,
            string? sortBy, bool? isAscending,
            int pageNumber, int pageSize)
        {
            if (pageNumber < 1) { pageNumber = 1; }
            if (pageSize < 1 || pageSize > 15) { pageSize = 7; }

            if (filterDays == null || filterDays < 1 || filterDays > 31)
            {
                var defaultTimeFrame = DateTime.UtcNow.AddDays(-1);

                var getOrdersDefault = await _saleOrderRepository.GetAllOrdersFromTimeframeAsync(defaultTimeFrame, status, sortBy, isAscending ?? true, pageNumber, pageSize);

                if (!getOrdersDefault.Any())
                {
                    return null;
                }
                return mapper.Map<IEnumerable<SaleOrderView>?>(getOrdersDefault);
            }

            var timeFrame = DateTime.UtcNow.AddDays(-filterDays.Value);

            var getOrders = await _saleOrderRepository.GetAllOrdersFromTimeframeAsync(timeFrame, status, sortBy, isAscending ?? true, pageNumber, pageSize);

            if (!getOrders.Any())
            {
                return null;
            }

            return mapper.Map<IEnumerable<SaleOrderView>?>(getOrders);
        }

        public async Task<SaleOrderViewDetails?> GetOrderById(Guid id)
        {
            var getOrder = await _saleOrderRepository.GetOrderByIdAsync(id);
            if (getOrder == null)
            {
                return null;
            }
            return mapper.Map<SaleOrderViewDetails>(getOrder);
        }

        public async Task<Guid> CheckOrderUserId(Guid orderId)
        {
            return await _saleOrderRepository.CheckOrderUserIdAsync(orderId);
        }

        private async Task<SaleOrder?> ValidateOrderInfo (Guid orderId, Guid userId)
        {
            DateTime currentTime = DateTime.UtcNow;

            var orderToPay = await _saleOrderRepository.GetOrderByIdAsync(orderId);

            // If the order doesn't exist, doesn't match our user, or is finished, it returns a null.
            if (orderToPay == null || orderToPay.UserId != userId || orderToPay.Status != 0)
            {
                return null;
            }

            // If the order is expired, or lacks any content, it's considered invalid and thus cancelled.
            if (orderToPay.ExpirationTime <= currentTime || orderToPay.Details == null || orderToPay.Details.Count == 0)
            {
                await CancelOrder(orderId, userId);
                throw new BadHttpRequestException("Preference Creation Failed: Order has already expired or is Invalid.");
            }

            return orderToPay;
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
