using AutoMapper;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Data.Interfaces;
using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Application.Services.Implementations
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

            var detailData = await _priceStockService.FormDetailData(createDetail.ProductId, createDetail.ProductQuantity);
            if (detailData == null)
            {
                return null;
            }

            var detailToCreate = mapper.Map<OrderDetails>(createDetail);

            detailToCreate.OrderId = orderId;
            detailToCreate.DetailPrice = detailData.DetailPrice;
            detailToCreate.ProductQuantity = detailData.FinalQuantity;
            detailToCreate.ProductName = detailData.ProductName;

            return await _orderDetailRepository.CreateOrderDetailAsync(detailToCreate);
        }

        public async Task EraseOrderDetail(Guid id)
        {
            var stockToReturn = await _priceStockService.HandleDetailDeletion(id);
            if (stockToReturn == null)
            {
                return;
            }
            await _orderDetailRepository.EraseDetailAsync(id);
        }
    }
}
