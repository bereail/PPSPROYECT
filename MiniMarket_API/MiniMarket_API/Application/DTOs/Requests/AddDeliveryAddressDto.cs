using System.ComponentModel.DataAnnotations;

namespace MiniMarket_API.Application.DTOs.Requests
{
    public class AddDeliveryAddressDto
    {
        [Required(ErrorMessage = "Province is Required.")]
        [StringLength(55, MinimumLength = 3, ErrorMessage = "Province must be at least 3 characters, or shorter than 55 characters.")]
        public string Province { get; set; }

        [Required(ErrorMessage = "City is Required.")]
        [StringLength(45, MinimumLength = 3, ErrorMessage = "City must be at least 3 characters, or shorter than 45 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "City is Required.")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Street must be at least 3 characters, or shorter than 60 characters.")]
        public string Street { get; set; }

        [Range(0, 20, MinimumIsExclusive = true, MaximumIsExclusive = false, ErrorMessage = "Floor mustn't be below 1, or above 20.")]
        public int? Floor { get; set; }

        [StringLength(10, MinimumLength = 1, ErrorMessage = "Apartment must be at least 1 character, or shorter than 10 characters.")]
        public string? Apartment { get; set; }
    }
}
