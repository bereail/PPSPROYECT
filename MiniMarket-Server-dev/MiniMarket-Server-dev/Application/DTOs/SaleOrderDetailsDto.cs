namespace MiniMarket_Server_dev.Application.DTOs
{
    public class SaleOrderDetailsDto
    {
        public Guid Id { get; set; }
        public decimal FinalPrice { get; set; }
        public DateTime OrderTime { get; set; }
        public string PaymentMethod { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime? DeactivationTime { get; set; }
        public Guid UserId { get; set; }
        public ICollection<OrderDetailsDto>? Details { get; set; }
    }
}
