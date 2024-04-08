using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Model.DTOs
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        bool IsActive { get; set; } 
        public ICollection<Product> Products { get; set; } 
    }
}
