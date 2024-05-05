using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Data.Interfaces;
using MiniMarket_API.Model.Entities;
using System.Diagnostics;

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

        public async Task<decimal?> SetDetailPrice(Guid productId, int orderQuantity)
        {
            var productDetail = await productRepository.GetProductByIdAsync(productId);
            if (productDetail == null)
            {
                return null;
            }

            var productStockCheck = await HandleProductStock(productDetail, orderQuantity);
            if (productStockCheck == null)
            {
                return null;
            }

            decimal discountAmount = productDetail.Discount / 100m * productDetail.Price;
            decimal finalPrice = productDetail.Price - discountAmount;
            finalPrice = finalPrice * orderQuantity;
            if (finalPrice < 0)
            {
                return null;
            }
            return finalPrice;
        }

        private async Task<int?> HandleProductStock(Product productDetail, int orderQuantity)
        {
            var checkStock = productDetail.Stock - orderQuantity;
            if (checkStock >= 1)
            {
                await productRepository.HandleProductStockAsync(productDetail.Id, checkStock);
                return checkStock;
            }
            return null;
        }

        public async Task<decimal?> UpdateDetailPrice(Guid productId, int oldOrderQuantity, int newOrderQuantity, decimal oldPrice)
        {
            var productDetail = await productRepository.GetProductByIdAsync(productId);
            if (productDetail == null)
            {
                return null;
            }

            if (oldOrderQuantity == newOrderQuantity)
            {
                return oldPrice;
            }

            int quantityDifference = newOrderQuantity - oldOrderQuantity;

            var productStockDifference = await HandleProductStock(productDetail, quantityDifference);
            if (productStockDifference == null)
            {
                return null;
            }

            decimal discountAmount = productDetail.Discount / 100m * productDetail.Price;
            decimal finalPrice = productDetail.Price - discountAmount;
            finalPrice = finalPrice * newOrderQuantity;
            if (finalPrice < 0)
            {
                return null;
            }
            return finalPrice;
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
