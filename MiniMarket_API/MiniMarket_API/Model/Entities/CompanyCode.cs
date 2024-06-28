using System.ComponentModel.DataAnnotations.Schema;

namespace MiniMarket_API.Model.Entities
{
    public class CompanyCode
    {
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(25)")]
        public string EmployeeCode { get; set; }
        public bool IsActive { get; set; } = true;

        public Seller? Seller { get; set; } //Nullable reference navigation to the dependent.


    }
}
