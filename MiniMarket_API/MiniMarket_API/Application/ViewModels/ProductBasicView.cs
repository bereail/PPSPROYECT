namespace MiniMarket_API.Application.ViewModels
{
    public class ProductBasicView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
    }
}
