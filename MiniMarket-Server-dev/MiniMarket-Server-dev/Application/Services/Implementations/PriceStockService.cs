using MiniMarket_Server_dev.Application.Services.Interfaces;
using MiniMarket_Server_dev.Data.Interfaces;
using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Application.Services.Implementations
{
    public class PriceStockService : IPriceStockService
    {
        private readonly IProductRepository productRepository;

        public PriceStockService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<decimal?> SetDetailPrice (Guid productId, int orderQuantity)
        {
            var productDetail = await productRepository.GetProductByIdAsync (productId);
            if (productDetail == null)
            {
                return null;
            }

            var productStockCheck = await HandleProductStock(productDetail, orderQuantity);
            if (productStockCheck == null)
            {
                return null;
            }

            var finalPrice = productDetail.Price * orderQuantity;
            if (finalPrice <= 0)
            {
                return null;
            }
            return finalPrice;
        }

        private async Task<int?> HandleProductStock (Product productDetail, int orderQuantity)
        {
            var checkStock = productDetail.Stock - orderQuantity;
            if (checkStock >= 1) {
                await productRepository.HandleProductStockAsync(productDetail.Id, checkStock);
                return checkStock;
            }
            return null;
        }
    }
}
