using System.ComponentModel.DataAnnotations;

namespace MiniMarket_API.Application.DTOs.Requests
{
    public class AddCategoryDto
    {
        [Required(ErrorMessage = "Category Name is Required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Category Name must be at least 3 characters, or shorter than 50 characters." )]
        public string CategoryName { get; set; }
    }
}
