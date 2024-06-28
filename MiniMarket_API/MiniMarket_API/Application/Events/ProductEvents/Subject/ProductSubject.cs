using MiniMarket_API.Application.Events.Observers.ProductObservers;

namespace MiniMarket_API.Application.Events.ProductEvents.Subject
{
    public class ProductSubject
    {
        private List<IProductObservers> observers = new List<IProductObservers>();

        private string ProductName { get; set; }
        private string CategoryName { get; set; }

        public ProductSubject(string productName, string categoryName)
        {
            ProductName = productName;
            CategoryName = categoryName;
        }

        public void AddObserver(IProductObservers observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IProductObservers observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (IProductObservers observer in observers)
            {
                observer.Update(ProductName, CategoryName);
            }
        }
    }
}
