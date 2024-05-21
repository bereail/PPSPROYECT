using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniMarket_API.Model.Entities
{
    public class CompanyCode
    {
        public int Id { get; set; }
        [Required]
        [MinLength(16)]
        public string EmployeeCode { get; set; }
        public bool IsActive { get; set; } = true;

        public Seller? Seller { get; set; } //Nullable reference navigation to the dependent.


    }
}
