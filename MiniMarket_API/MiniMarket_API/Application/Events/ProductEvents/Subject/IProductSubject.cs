using MiniMarket_API.Application.Events.Observers.ProductObservers;

namespace MiniMarket_API.Application.Events.ProductEvents.Subject
{
    public interface IProductSubject
    {
        void AddObserver(IProductObservers observer);

        void RemoveObserver(IProductObservers observer);

        void NotifyObservers();
    }
}
