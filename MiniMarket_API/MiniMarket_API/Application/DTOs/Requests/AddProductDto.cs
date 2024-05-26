using System.ComponentModel.DataAnnotations;

namespace MiniMarket_API.Application.DTOs.Requests
{
    public class AddProductDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(0, 20000, MinimumIsExclusive = true, MaximumIsExclusive = false)]
        public decimal Price { get; set; }
        [Range(0, 125, MinimumIsExclusive = true, MaximumIsExclusive = false)]
        public int Stock { get; set; }
        [Range(0, 100, MinimumIsExclusive = false, MaximumIsExclusive = false)]
        public int Discount { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
    }
}
