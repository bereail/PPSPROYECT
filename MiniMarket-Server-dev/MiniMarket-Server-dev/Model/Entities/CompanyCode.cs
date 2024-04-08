using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniMarket_Server_dev.Model.Entities
{
    public class CompanyCode
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(16)]
        public string EmployeeCode { get; set; }
        public bool InUse { get; set; } = false;
        public bool IsActive { get; set; } = true;

        public Seller? Seller { get; set; } //Nullable reference navigation to the dependent.

        
    }
}
