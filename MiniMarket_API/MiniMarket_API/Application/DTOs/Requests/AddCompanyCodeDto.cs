using System.ComponentModel.DataAnnotations;

namespace MiniMarket_API.Application.DTOs.Requests
{
    public class AddCompanyCodeDto
    {
        [Required(ErrorMessage = "Hexadecimal Code is Required.")]
        [StringLength(25, MinimumLength = 16, ErrorMessage = "The provided code must be at least 16 characters, or shorter than 25 characters.")]
        public string EmployeeCode { get; set; }
    }
}
