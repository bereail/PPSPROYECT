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

        public async Task<OrderDetails?> UpdateOrderDetail(CreateDetailDto updateDetail)
        {
            var detailToUpdate = await _orderDetailRepository.GetDetailByIdAsync(updateDetail.DetailId.Value);

            if (detailToUpdate == null)
            {
                return null;
            }

            decimal? newDetailPrice = await _priceStockService.UpdateDetailPrice(detailToUpdate.ProductId, detailToUpdate.ProductQuantity, updateDetail.ProductQuantity, detailToUpdate.DetailPrice);

            if (newDetailPrice == null)
            {
                return null;
            }

            var updateValues = mapper.Map<OrderDetails>(updateDetail);
            updateValues.DetailPrice = newDetailPrice.Value;

            return await _orderDetailRepository.UpdateDetailAsync(detailToUpdate.Id, updateValues);
        }

        public async Task<Guid?> EraseOrderDetail(Guid id)
        {
            var stockToReturn = await _priceStockService.HandleDetailDeletion(id);
            if (stockToReturn == null)
            {
                return null;
            }
            return await _orderDetailRepository.EraseDetailAsync(id);
        }
    }
}
