using System.ComponentModel.DataAnnotations;

namespace MiniMarket_Server_dev.Application.DTOs.Requests
{
    public class AddCompanyCodeDto
    {
        [Required]
        [MinLength(16)]
        public string EmployeeCode { get; set; }
    }
}
