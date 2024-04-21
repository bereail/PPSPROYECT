using MiniMarket_Server_dev.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace MiniMarket_Server_dev.Application.DTOs.Requests
{
    public class UpdateCategoryDto
    {
        [Required]
        public string CategoryName { get; set; }
    }
}
