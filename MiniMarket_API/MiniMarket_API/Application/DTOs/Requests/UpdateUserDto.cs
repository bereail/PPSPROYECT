using System.ComponentModel.DataAnnotations;

namespace MiniMarket_API.Application.DTOs.Requests
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "User Name is Required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "User Name must be at least 3 characters, or shorter than 50 characters")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "User Phone Number mustn't be longer than 50 characters.")]
        [RegularExpression(@"^\+?[1-9][0-9]{7,14}$", ErrorMessage = "Invalid Phone Number format.")]
        public string PhoneNumber { get; set; }
    }
}
