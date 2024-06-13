using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Application.Services.Interfaces
{
    public interface IMpOrderService
    {
        Task<string> HandlePreferenceRequest(ICollection<OrderDetails> details);
    }
}
