using System.ComponentModel.DataAnnotations;

namespace MiniMarket_API.Application.DTOs.Requests
{
    public class AddCompanyCodeDto
    {
        [Required]
        [MinLength(16)]
        public string EmployeeCode { get; set; }
    }
}
