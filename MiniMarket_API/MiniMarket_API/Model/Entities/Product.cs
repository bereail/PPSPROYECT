using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniMarket_API.Model.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Description { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; } = 0;
        public int Stock { get; set; } = 0;
        public int Discount { get; set; } = 0;
        public bool IsActive { get; set; } = true;

        [Column(TypeName = "datetime")]
        public DateTime? DeactivationTime { get; set; }

        public Guid CategoryId { get; set; }        //Required FK
        public ProductCategory Category { get; set; } = null!;      //Required navigation property to principal

        public ICollection<OrderDetails> Details { get; set; }      //Principal collective property for OrDtls
    }
}
