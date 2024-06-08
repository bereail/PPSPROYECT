namespace MiniMarket_API.Application.ViewModels
{
    public class CategoryViewProducts
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public ICollection<ProductView>? Products { get; set; }
    }
}
