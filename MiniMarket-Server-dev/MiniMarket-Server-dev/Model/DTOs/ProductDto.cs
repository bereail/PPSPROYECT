using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Model.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } 
        public int Stock { get; set; } 
        public bool IsActive { get; set; } 
        public DateTime? DeactivationTime { get; set; }

        public Guid CategoryId { get; set; }        
        public ProductCategory Category { get; set; } = null!;      

        public ICollection<OrderDetails> Details { get; set; } 
    }
}
