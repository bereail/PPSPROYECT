namespace MiniMarket_Server_dev.Model.Entities
{
    public class SaleOrder
    {
        public Guid Id { get; set; }
        public decimal FinalPrice { get; set; }
        public DateTime OrderTime { get; set; } 
        public string PaymentMethod { get; set; }   
        public string DeliveryAddress { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? DeactivationTime {  get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public ICollection<OrderDetails> Details { get; set; } = new List<OrderDetails>();       //Principal collective property for OrdDtls
    }
}
