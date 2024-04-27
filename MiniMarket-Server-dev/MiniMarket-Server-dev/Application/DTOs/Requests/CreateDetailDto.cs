using System.ComponentModel.DataAnnotations;

namespace MiniMarket_Server_dev.Application.DTOs.Requests
{
    public class CreateDetailDto
    {
        public Guid? DetailId { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        [Range(1, 30, MinimumIsExclusive = false, MaximumIsExclusive = false)]
        public int ProductQuantity { get; set; }
    }
}
