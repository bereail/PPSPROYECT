using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Application.DTOs
{
    public class SaleOrderDto
    {
        public Guid Id { get; set; }
        public decimal FinalPrice { get; set; }
        public DateTime OrderTime { get; set; }
        public string PaymentMethod { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime? DeactivationTime { get; set; }
        public Guid UserId { get; set; }
    }
}
