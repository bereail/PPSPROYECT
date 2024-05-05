using AutoMapper;
using MiniMarket_API.Application.DTOs;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Data.Interfaces;
using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Application.Services.Implementations
{
    public class SaleOrderService : ISaleOrderService
    {
        private readonly ISaleOrderRepository _saleOrderRepository;
        private readonly IOrderDetailsService _orderDetailsService;
        private readonly IMapper mapper;

        public SaleOrderService(ISaleOrderRepository saleOrderRepository, IMapper mapper, IOrderDetailsService orderDetailsService)
        {
            _saleOrderRepository = saleOrderRepository;
            this.mapper = mapper;
            _orderDetailsService = orderDetailsService;
        }

        public async Task<SaleOrderDetailsDto?> CreateSaleOrder(CreateOrderDto createOrderDto)
        {
            var orderToCreate = mapper.Map<SaleOrder>(createOrderDto);

            orderToCreate = await _saleOrderRepository.CreateOrderAsync(orderToCreate);

            decimal finalOrderPrice = 0;

            //Upon Order creation, the request will include at least 1 Detail. These Details will then be processed here.
            var detailsToCreate = createOrderDto.NewDetails;
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

        public async Task<SaleOrderDetailsDto?> UpdateSaleOrder(Guid orderId, UpdateOrderDto updateOrder)
        {
            var orderToUpdate = mapper.Map<SaleOrder>(updateOrder);
            orderToUpdate = await _saleOrderRepository.UpdateOrderAsync(orderId, orderToUpdate);

            if (orderToUpdate == null)
            {
                return null;
            }

            decimal finalOrderPrice = 0;

            var oldDetails = orderToUpdate.Details;

            var detailsToUpdate = updateOrder.UpdateDetails;

            //Deletes all old details that haven't been sent again in the new update
            foreach (var oldDetail in oldDetails)
            {
                bool remainingDetail = detailsToUpdate.Any(d => d.DetailId == oldDetail.Id);
                if (remainingDetail) { continue; }
                await _orderDetailsService.EraseOrderDetail(oldDetail.Id);
            }

            foreach (var detail in detailsToUpdate)
            {
                //If the detail exists both in the previous version and the update request, it will be updated
                bool existingDetail = oldDetails.Any(d => d.Id == detail.DetailId);
                if (existingDetail)
                {
                    var updatedDetails = await _orderDetailsService.UpdateOrderDetail(detail);
                    if (updatedDetails != null)
                    {
                        finalOrderPrice = finalOrderPrice + updatedDetails.DetailPrice;
                    }
                    continue;
                }
                //If it doesn't, it will be created instead
                var createdDetails = await _orderDetailsService.CreateOrderDetail(detail, orderId);
                if (createdDetails != null)
                {
                    finalOrderPrice = finalOrderPrice + createdDetails.DetailPrice;
                }
                continue;

            }

            await _saleOrderRepository.SetFinalOrderPriceAsync(orderId, finalOrderPrice);
            //This is for the result.
            orderToUpdate.FinalPrice = finalOrderPrice;

            return mapper.Map<SaleOrderDetailsDto>(orderToUpdate);
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
    }
}
