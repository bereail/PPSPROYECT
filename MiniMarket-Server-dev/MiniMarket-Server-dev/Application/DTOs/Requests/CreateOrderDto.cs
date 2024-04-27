using MiniMarket_Server_dev.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace MiniMarket_Server_dev.Application.DTOs.Requests
{
    public class CreateOrderDto
    {
        [Required]
        public string DeliveryAddress { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public ICollection<CreateDetailDto> NewDetails { get; set; }
    }
}
