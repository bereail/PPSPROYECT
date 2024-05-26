namespace MiniMarket_API.Application.ViewModels
{
    public class OrderDetailsView
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }
        public ProductBasicView Product { get; set; }
        public int ProductQuantity { get; set; }
        public decimal DetailPrice { get; set; }
    }
}
