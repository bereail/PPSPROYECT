namespace MiniMarket_API.Application.Events.Observers.ProductObservers
{
    public interface IProductObservers
    {
        void Update(string productName, string categoryName);
    }
}
