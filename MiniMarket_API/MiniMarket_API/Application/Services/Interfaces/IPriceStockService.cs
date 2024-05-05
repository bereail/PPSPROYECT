namespace MiniMarket_API.Application.Services.Interfaces
{
    public interface IPriceStockService
    {
        Task<decimal?> SetDetailPrice(Guid productId, int orderQuantity);
        Task<decimal?> UpdateDetailPrice(Guid productId, int oldOrderQuantity, int newOrderQuantity, decimal oldPrice);
        Task<int?> HandleDetailDeletion(Guid detailId);
    }
}
