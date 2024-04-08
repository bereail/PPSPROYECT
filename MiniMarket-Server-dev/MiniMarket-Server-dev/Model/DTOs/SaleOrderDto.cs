using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Model.DTOs
{
    public class SaleOrderDto
    {
        public Guid Id { get; set; }
        public decimal FinalPrice { get; set; }
        public DateTime OrderTime { get; set; } 
        public string PaymentMethod { get; set; }
        public string DeliveryAddress { get; set; }
        public bool IsActive { get; set; } 
        public DateTime? DeactivationTime { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<OrderDetails> Details { get; set; } 
    }
}
