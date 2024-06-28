using MiniMarket_API.Application.Events.Observers.ProductObservers;
using MiniMarket_API.Application.Events.ProductEvents.Subject;
using MiniMarket_API.Data.Interfaces;

namespace MiniMarket_API.Application.Events.ProductEvents
{
    public class ProductEventManager : IProductEventManager
    {
        private readonly IUserRepository userRepository;

        public ProductEventManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task NotifyCustomersOfNewProduct(string productName, string categoryName)
        {
            var emails = await userRepository.GetAllCustomerEmailsAsync();

            if (emails == null || !emails.Any())
            {
                return;
            }

            ProductSubject product = new ProductSubject(productName, categoryName);

            SubscribeEmails(emails, product);

            product.NotifyObservers();
        }

        private void SubscribeEmails(ICollection<string> emails, ProductSubject product)
        {
            foreach (var email in emails)
            {
                Observer emailToSubscribe = new Observer(email);
                emailToSubscribe.Subscribe(product);
            }
        }
    }
}
