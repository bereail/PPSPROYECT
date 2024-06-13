namespace MiniMarket_API.Application.ViewModels
{
    public class SaleOrderViewDetails
    {
        public Guid Id { get; set; }
        public decimal FinalPrice { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public string Status { get; set; }
        public DateTime? FinishTime { get; set; }
        public DeliveryAddressView DeliveryAddress { get; set; }
        public ICollection<OrderDetailsView>? Details { get; set; }
    }
}
