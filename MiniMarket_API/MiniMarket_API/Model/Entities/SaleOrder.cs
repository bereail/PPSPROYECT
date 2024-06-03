using Microsoft.EntityFrameworkCore;
using MiniMarket_API.Model.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniMarket_API.Model.Entities
{
    public class SaleOrder
    {
        public Guid Id { get; set; }
        [Precision(18, 2)]
        public decimal FinalPrice { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime OrderTime { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string DeliveryAddress { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        [Column(TypeName = "datetime")]
        public DateTime? FinishTime { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public ICollection<OrderDetails>? Details { get; set; }      //Principal collective property for OrdDtls
    }
}
