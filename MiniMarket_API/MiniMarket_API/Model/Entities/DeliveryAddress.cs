using System.ComponentModel.DataAnnotations.Schema;

namespace MiniMarket_API.Model.Entities
{
    public class DeliveryAddress
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        [Column(TypeName = "nvarchar(45)")]
        public string Province { get; set; }

        [Column(TypeName = "nvarchar(45)")]
        public string City { get; set; }

        [Column(TypeName = "nvarchar(60)")]
        public string Street { get; set; }

        public int? Floor { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string? Apartment { get; set; }

        //Navigation Properties

        public User User { get; set; }

        public ICollection<SaleOrder>? SaleOrders { get; set; }
    }
}
