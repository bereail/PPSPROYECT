using System.ComponentModel.DataAnnotations;

namespace MiniMarket_API.Application.DTOs.Requests
{
    public class CreateOrderDto
    {
        [Required(ErrorMessage = "Order Delivery Address is Required.")]
        public string DeliveryAddress { get; set; }

        [Required, MinLength(1, ErrorMessage = "Order requires at least 1 Product request.")]
        public ICollection<CreateDetailDto> NewDetails { get; set; }
    }
}
