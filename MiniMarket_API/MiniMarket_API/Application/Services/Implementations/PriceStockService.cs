using MiniMarket_API.Application.DTOs.DetailData;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Data.Interfaces;
using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Application.Services.Implementations
{
    public class PriceStockService : IPriceStockService
    {
        private readonly IProductRepository productRepository;
        private readonly IOrderDetailRepository orderDetailRepository;

        public PriceStockService(IProductRepository productRepository, IOrderDetailRepository orderDetailRepository)
        {
            this.productRepository = productRepository;
            this.orderDetailRepository = orderDetailRepository;
        }

        public async Task<NewDetailResultsDto?> FormDetailData(Guid productId, int orderQuantity)
        {
            var productDetail = await productRepository.GetProductByIdAsync(productId);
            if (productDetail == null)
            {
                return null;
            }

            var productStockCheck = await HandleProductStock(productDetail, orderQuantity);
            if (productStockCheck == null)
            {
                //If the method returns null, it means the requested quantity exceeds our stock. Instead of outright denying the request, we use up all existing stock.
                var fullStock = productDetail.Stock;
                var emptyStock = 0;
                await productRepository.HandleProductStockAsync(productDetail.Id, emptyStock);
                orderQuantity = fullStock;
                //And we use this new orderQuantity instead of the old one.
            }

            decimal discountAmount = productDetail.Discount / 100m * productDetail.Price;
            decimal finalPrice = productDetail.Price - discountAmount;
            finalPrice = finalPrice * orderQuantity;
            if (finalPrice < 0)
            {
                return null;
            }

            var detailResult = new NewDetailResultsDto
            {
                ProductName = productDetail.Name,
                DetailPrice = finalPrice,
                FinalQuantity = orderQuantity,
            };

            return detailResult;
        }

        private async Task<int?> HandleProductStock(Product productDetail, int orderQuantity)
        {
            var checkStock = productDetail.Stock - orderQuantity;
            if (checkStock >= 0)
            {
                await productRepository.HandleProductStockAsync(productDetail.Id, checkStock);
                return checkStock;
            }
            return null;
        }

        public async Task<int?> HandleDetailDeletion(Guid detailId)
        {
            var getDetail = await orderDetailRepository.GetDetailByIdAsync(detailId);
            if (getDetail == null) { return null; }

            int? quantityToReturn = -getDetail.ProductQuantity;

            quantityToReturn = await HandleProductStock(getDetail.Product, quantityToReturn.Value);

            return quantityToReturn;
        }
    }
}
