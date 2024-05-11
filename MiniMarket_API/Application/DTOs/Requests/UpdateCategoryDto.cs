using System.ComponentModel.DataAnnotations;

namespace MiniMarket_API.Application.DTOs.Requests
{
    public class UpdateCategoryDto
    {
        [Required]
        public string CategoryName { get; set; }
    }
}
