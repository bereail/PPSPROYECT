using System.ComponentModel.DataAnnotations;

namespace MiniMarket_Server_dev.Application.DTOs.Requests
{
    public class CreateDetailDto
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public int ProductQuantity { get; set; }
    }
}
