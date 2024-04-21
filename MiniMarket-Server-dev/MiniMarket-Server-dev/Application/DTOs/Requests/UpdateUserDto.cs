using System.ComponentModel.DataAnnotations;

namespace MiniMarket_Server_dev.Application.DTOs.Requests
{
    public class UpdateUserDto
    {
        [Required]
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
