using System.ComponentModel.DataAnnotations;

namespace MiniMarket_API.Application.DTOs.Requests
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "User Name is Required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "User Name must be at least 3 characters, or shorter than 50 characters")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "User Phone Number mustn't be longer than 75 characters.")]
        [RegularExpression(@"^(?([0-9]{3}))?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone Number format.")]
        public string PhoneNumber { get; set; }

        [StringLength(200, ErrorMessage = "User Address mustn't be longer than 200 characters.")]
        public string Address { get; set; }
    }
}
