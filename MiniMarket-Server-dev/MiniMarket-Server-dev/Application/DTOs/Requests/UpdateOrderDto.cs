using System.ComponentModel.DataAnnotations;

namespace MiniMarket_Server_dev.Application.DTOs.Requests
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
