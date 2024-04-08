using MiniMarket_Server_dev.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace MiniMarket_Server_dev.Model.DTOs
{
    public class CompanyCodeDto
    {
        public Guid Id { get; set; }
        public string EmployeeCode { get; set; }
        public bool InUse { get; set; } 
        public bool IsActive { get; set; } 
        public Seller? Seller { get; set; } 
    }
}
