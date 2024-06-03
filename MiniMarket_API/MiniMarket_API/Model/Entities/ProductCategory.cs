using System.ComponentModel.DataAnnotations.Schema;

namespace MiniMarket_API.Model.Entities
{
    public class ProductCategory
    {
        public Guid Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string CategoryName { get; set; }
        public bool IsActive { get; set; } = true;
        [Column(TypeName = "datetime")]
        public DateTime? DeactivationTime { get; set; }
        public ICollection<Product> Products { get; set; }    //Collection navigation to contain dependents
    }
}
