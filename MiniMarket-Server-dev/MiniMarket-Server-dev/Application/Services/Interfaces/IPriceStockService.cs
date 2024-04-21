namespace MiniMarket_Server_dev.Application.Services.Interfaces
{
    public interface IPriceStockService
    {
        Task<decimal?> SetDetailPrice(Guid productId, int orderQuantity);
    }
}
