namespace MiniMarket_API.Application.Events.ProductEvents
{
    public interface IProductEventManager
    {
        Task NotifyCustomersOfNewProduct(string productName, string categoryName);
    }
}
