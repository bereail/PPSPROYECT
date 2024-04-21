using System.ComponentModel.DataAnnotations;

namespace MiniMarket_Server_dev.Application.DTOs.Requests
{
    public class AddCategoryDto
    {
        [Required]
        public string CategoryName { get; set; }
    }
}
