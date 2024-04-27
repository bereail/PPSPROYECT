using MiniMarket_Server_dev.Model.Entities;
using MiniMarket_Server_dev.Model.Enums;

namespace MiniMarket_Server_dev.Application.DTOs
{
    public class SaleOrderDto
    {
        public Guid Id { get; set; }
        public decimal FinalPrice { get; set; }
        public DateTime OrderTime { get; set; }
        public string DeliveryAddress { get; set; }
        public OrderStatus Status { get; set; }
        public Guid UserId { get; set; }
    }
}
