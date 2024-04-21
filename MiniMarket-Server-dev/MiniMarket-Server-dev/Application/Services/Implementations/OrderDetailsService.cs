using AutoMapper;
using MiniMarket_Server_dev.Application.DTOs.Requests;
using MiniMarket_Server_dev.Application.Services.Interfaces;
using MiniMarket_Server_dev.Data.Interfaces;
using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Application.Services.Implementations
{
    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IPriceStockService _priceStockService;
        private readonly IMapper mapper;

        public OrderDetailsService(IOrderDetailRepository orderDetailRepository, IPriceStockService priceStockService, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _priceStockService = priceStockService;
            this.mapper = mapper;
        }

        public async Task<OrderDetails?> CreateOrderDetail(CreateDetailDto createDetail, Guid orderId)             //Won't operate with DTOs directly as mappings are taken care of by SaleOrderService.
        {
            
            var detailPrice = await _priceStockService.SetDetailPrice(createDetail.ProductId, createDetail.ProductQuantity);
            if (detailPrice == null)
            {
                return null;
            }
            var detailToCreate = mapper.Map<OrderDetails>(createDetail);
            detailToCreate.OrderId = orderId;
            detailToCreate.DetailPrice = detailPrice.Value;

            return await _orderDetailRepository.CreateOrderDetailAsync(detailToCreate);                                                           
        }

        public async Task<OrderDetails?> DeactivateDetail(Guid id)                          //Same case as the previous one. DTOs won't be needed here as it's exclusive for DeactivateOrder();
        {
            return await _orderDetailRepository.DeactivateDetailAsync(id);
        }
    }
}
