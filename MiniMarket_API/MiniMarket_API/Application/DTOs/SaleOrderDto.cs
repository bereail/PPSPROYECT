using MiniMarket_API.Model.Enums;

namespace MiniMarket_API.Application.DTOs
{
    public class SaleOrderDto
    {
        public Guid Id { get; set; }
        public decimal FinalPrice { get; set; }
        public DateTime OrderTime { get; set; }
        public string DeliveryAddress { get; set; }
        public OrderStatus Status { get; set; }
    }
}
