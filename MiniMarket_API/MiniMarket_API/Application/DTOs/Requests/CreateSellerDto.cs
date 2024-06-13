using System.ComponentModel.DataAnnotations;

namespace MiniMarket_API.Application.DTOs.Requests
{
    public class CreateSellerDto
    {
        [Required(ErrorMessage = "User Name is Required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "User Name must be at least 3 characters, or shorter than 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "User Email is Required.")]
        [EmailAddress(ErrorMessage = "User Email must be in a valid Email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "User Password is Required.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "User Password must be at least 8 characters, or shorter than 50 characters.")]
        public string Password { get; set; }

        [StringLength(50, ErrorMessage = "User Phone Number mustn't be longer than 50 characters.")]
        [RegularExpression(@"^\+?[1-9][0-9]{7,14}$", ErrorMessage = "Invalid Phone Number format.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Company Code is Required")]
        [StringLength(25, MinimumLength = 16, ErrorMessage = "The provided code must be at least 16 characters, or shorter than 25 characters.")]
        public string HexadecimalCode { get; set; }
    }
}
