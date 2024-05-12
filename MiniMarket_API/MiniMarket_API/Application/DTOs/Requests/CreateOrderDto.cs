using System.ComponentModel.DataAnnotations;

namespace MiniMarket_API.Application.DTOs.Requests
{
    public class CreateOrderDto
    {
        [Required]
        public string DeliveryAddress { get; set; }

        [Required]
        public ICollection<CreateDetailDto> NewDetails { get; set; }
    }
}
