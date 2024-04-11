using System.ComponentModel.DataAnnotations;

namespace MiniMarket_Server_dev.Model.DTOs
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public string UserType { get; set; }

        [JsonIgnore]
        // Establecer IsActive a true por defecto
        public bool IsActive { get; set; } 
        
    }
}
