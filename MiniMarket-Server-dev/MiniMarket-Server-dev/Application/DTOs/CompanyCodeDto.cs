using MiniMarket_Server_dev.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace MiniMarket_Server_dev.Application.DTOs
{
    public class CompanyCodeDto
    {
        public Guid Id { get; set; }
        public string EmployeeCode { get; set; }
        public UserDto? Seller { get; set; }
    }
}
