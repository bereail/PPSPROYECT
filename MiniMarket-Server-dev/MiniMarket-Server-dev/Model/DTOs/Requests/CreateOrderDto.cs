using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Model.DTOs.Requests
{
    public class CreateOrderDto
    {
        public decimal FinalPrice { get; set; }
        public DateTime OrderTime { get; set; }
        public string PaymentMethod { get; set; }
        public string DeliveryAddress { get; set; }
        public bool IsActive { get; set; }
        public Guid UserId { get; set; }
        public ICollection<OrderDetails> Details { get; set; } 
    }
}
