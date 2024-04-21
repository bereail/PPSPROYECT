using System.ComponentModel.DataAnnotations;

namespace MiniMarket_Server_dev.Application.DTOs.Requests
{
    public class CreateSellerDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        [Required]
        [MinLength(16)]
        public string HexadecimalCode { get; set; }
    }
}
