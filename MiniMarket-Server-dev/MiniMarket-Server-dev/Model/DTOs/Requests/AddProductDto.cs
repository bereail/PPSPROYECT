using System.ComponentModel.DataAnnotations;

namespace MiniMarket_Server_dev.Model.DTOs.Requests
{
    public class AddProductDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
    }
}
