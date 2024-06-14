using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniMarket_API.Model.Entities
{
    public class OrderDetails
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }     
        public Guid OrderId { get; set; }       
        public Product Product { get; set; }     
        public SaleOrder SaleOrder { get; set; }

        [Column("Item", TypeName = "nvarchar(50)")]
        public string ProductName { get; set; }

        [Precision(18, 2)]
        public decimal DetailPrice { get; set; }
        public int ProductQuantity { get; set; }

    }
}
