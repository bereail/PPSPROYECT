namespace MiniMarket_API.Application.ViewModels
{
    public class OrderDetailsView
    {
        public ProductBasicView Product { get; set; }
        public int ProductQuantity { get; set; }
        public decimal DetailPrice { get; set; }
    }
}
