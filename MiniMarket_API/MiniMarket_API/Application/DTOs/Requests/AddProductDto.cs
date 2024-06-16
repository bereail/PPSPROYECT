using System.ComponentModel.DataAnnotations;

namespace MiniMarket_API.Application.DTOs.Requests
{
    public class AddProductDto
    {
        [Required(ErrorMessage = "Product Name is Required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Product Name must be at least 3 characters, or shorter than 50 characters.")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Product Description mustn't be longer than 200 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Product Price is Required.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Product Price mustn't have more than two decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Product Price mustn't exceed 18 digits.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Product Stock is Required.")]
        [Range(0, 125, MinimumIsExclusive = true, MaximumIsExclusive = false, ErrorMessage = "Product Stock mustn't be below 1, or above 125 units.")]
        public int Stock { get; set; }

        [Range(0, 99, MinimumIsExclusive = false, MaximumIsExclusive = false, ErrorMessage = "Product Discount mustn't be below 0, or above 99.")]
        public int Discount { get; set; }
    }
}
