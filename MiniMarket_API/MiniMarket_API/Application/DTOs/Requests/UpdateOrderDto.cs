using System.ComponentModel.DataAnnotations;

namespace MiniMarket_API.Application.DTOs.Requests
{
    public class UpdateOrderDto
    {
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        public string DeliveryAddress { get; set; }
        [Required]
        public ICollection<CreateDetailDto> UpdateDetails { get; set; }
    }
}
