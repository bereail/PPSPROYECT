namespace MiniMarket_API.Application.DTOs
{
    public class CategoryCollectionDto
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<ProductDto>? Products { get; set; }
    }
}
