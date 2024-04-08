namespace MiniMarket_Server_dev.Model.DTOs.Requests
{
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } 
        public int Stock { get; set; } 
        public bool IsActive { get; set; }
    }
}
