using System.ComponentModel.DataAnnotations;

namespace MiniMarket_API.Application.DTOs.Requests
{
    public class AddProductImageDto
    {
        [Required(ErrorMessage = "Image File is Required.")]
        public IFormFile ImageFile { get; set; }

        [Required(ErrorMessage = "Image Name is Required.")]
        [StringLength(35, MinimumLength = 1, ErrorMessage = "Image Name must be at least 1 character, or shorter than 35 characters.")]
        public string ImageName { get; set; }
    }
}
