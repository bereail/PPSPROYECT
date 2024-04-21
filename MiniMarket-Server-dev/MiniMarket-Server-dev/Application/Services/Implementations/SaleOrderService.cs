using AutoMapper;
using MiniMarket_Server_dev.Application.DTOs;
using MiniMarket_Server_dev.Application.DTOs.Requests;
using MiniMarket_Server_dev.Application.Services.Interfaces;
using MiniMarket_Server_dev.Data.Interfaces;
using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Application.Services.Implementations
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

        public async Task<SaleOrderDetailsDto?> DeactivateOrder(Guid id)
        {
            var orderToDeactivate = await _saleOrderRepository.DeactivateOrderAsync(id);
            if (orderToDeactivate == null) 
            { 
                return null; 
            }
            foreach (var detail in orderToDeactivate.Details)
            {
                await _orderDetailsService.DeactivateDetail(detail.Id);
            }
            return mapper.Map<SaleOrderDetailsDto?>(orderToDeactivate);
        }

        public async Task<SaleOrderDto?> EraseOrder (Guid id)
        {
            var orderToErase = await _saleOrderRepository.EraseOrderAsync(id);
            if(orderToErase == null)
            {
                return null;
            }
            return mapper.Map<SaleOrderDto?>(orderToErase);
        }

        public async Task<IEnumerable<SaleOrderDto>?> GetAllOrders(bool? isActive, string? sortBy, bool? isAscending,
            int pageNumber, int pageSize)
        {
            var getOrders = await _saleOrderRepository.GetAllOrders(isActive, sortBy, isAscending ?? true, pageNumber, pageSize);
            if (!getOrders.Any())
            {
                return null;
            }
            return mapper.Map<IEnumerable<SaleOrderDto>>(getOrders);
        }

        public async Task<IEnumerable<SaleOrderDto>?> GetAllOrdersByUser(Guid id, bool? isActive, string? sortBy, bool? isAscending,
            int pageNumber, int pageSize)
        {
            var getOrders = await _saleOrderRepository.GetAllOrdersByUserAsync(id, isActive, sortBy, isAscending ?? true, pageNumber, pageSize);
            if (!getOrders.Any())
            {
                return null;
            }
            return mapper.Map<IEnumerable<SaleOrderDto>>(getOrders);
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
