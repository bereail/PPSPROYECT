namespace MiniMarket_API.Application.Services.Interfaces
{
    public interface IPriceStockService
    {
        Task<decimal?> SetDetailPrice(Guid productId, int orderQuantity);
        Task<int?> HandleDetailDeletion(Guid detailId);
    }
}
