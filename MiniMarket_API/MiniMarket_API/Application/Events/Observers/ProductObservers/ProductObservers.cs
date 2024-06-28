using MiniMarket_API.Application.Events.ProductEvents.Subject;
using System.Diagnostics;

namespace MiniMarket_API.Application.Events.Observers.ProductObservers
{
    public class Observer : IProductObservers
    {
        public string UserEmail { get; set; }

        public Observer(string userEmail) 
        {
            UserEmail = userEmail;
        }

        public void Subscribe(ProductSubject subject)
        {
            subject.AddObserver(this);
        }

        public void Unsubscribe(ProductSubject subject)
        {
            subject.RemoveObserver(this);
        }

        public void Update(string productName, string categoryName)
        {
            Trace.WriteLine($"{UserEmail}, the product {productName} is now available in the category {categoryName}.");
        }
    }
}
