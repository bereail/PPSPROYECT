using System.ComponentModel.DataAnnotations;

namespace MiniMarket_API.Application.DTOs.Requests
{
    public class CreateDetailDto
    {
        [Required(ErrorMessage = "Requested Product ID is Required.")]
        public Guid ProductId { get; set; }
        [Required(ErrorMessage = "Requested Product Quantity is Required.")]
        [Range(1, 30, MinimumIsExclusive = false, MaximumIsExclusive = false, ErrorMessage = "The requested Quantity mustn't be below 1 unit, or more than 30 units.")]
        public int ProductQuantity { get; set; }
    }
}
