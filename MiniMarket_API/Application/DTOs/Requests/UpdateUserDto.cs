using System.ComponentModel.DataAnnotations;

namespace MiniMarket_API.Application.DTOs.Requests
{
    public class UpdateUserDto
    {
        [Required]
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
