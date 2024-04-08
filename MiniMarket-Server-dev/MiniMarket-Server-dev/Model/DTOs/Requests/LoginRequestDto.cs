using System.ComponentModel.DataAnnotations;

namespace MiniMarket_Server_dev.Model.DTOs.Requests
{
    public class LoginRequestDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
