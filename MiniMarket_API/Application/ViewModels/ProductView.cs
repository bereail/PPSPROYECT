namespace MiniMarket_API.Application.ViewModels
{
    public class ProductView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductImageBasicView Image { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int Discount { get; set; }
        public bool IsActive { get; set; }

    }
}
