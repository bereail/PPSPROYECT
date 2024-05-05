using MiniMarket_API.Model.Enums;

namespace MiniMarket_API.Application.DTOs
{
    public class SaleOrderDetailsDto
    {
        public Guid Id { get; set; }
        public decimal FinalPrice { get; set; }
        public DateTime OrderTime { get; set; }
        public string DeliveryAddress { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime? FinishTime { get; set; }
        public Guid UserId { get; set; }
        public ICollection<OrderDetailsDto>? Details { get; set; }
    }
}
