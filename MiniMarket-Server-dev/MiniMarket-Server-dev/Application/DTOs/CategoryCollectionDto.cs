using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Application.DTOs
{
    public class CategoryCollectionDto
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<ProductDto>? Products { get; set; }
    }
}
