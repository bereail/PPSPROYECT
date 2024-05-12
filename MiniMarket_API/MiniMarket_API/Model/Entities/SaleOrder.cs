using MiniMarket_API.Model.Enums;

namespace MiniMarket_API.Model.Entities
{
    public class SaleOrder
    {
        public Guid Id { get; set; }
        public decimal FinalPrice { get; set; }
        public DateTime OrderTime { get; set; }
        public string DeliveryAddress { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTime? FinishTime { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public ICollection<OrderDetails>? Details { get; set; }      //Principal collective property for OrdDtls
    }
}
