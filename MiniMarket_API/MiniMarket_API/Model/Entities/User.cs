using System.ComponentModel.DataAnnotations.Schema;

namespace MiniMarket_API.Model.Entities
{
    public abstract class User
    {
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(75)")]
        public string Email { get; set; }

        //[Column(TypeName = "binary(32)")]
        //public byte[] Password { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        public string Password { get; set; }

        [Column(TypeName = "nvarchar(75)")]
        public string PhoneNumber { get; set; }

        [Column("Role", TypeName = "nvarchar(25)")]
        public string UserType { get; set; }
        public bool IsActive { get; set; } = true;

        [Column(TypeName = "datetime")]
        public DateTime? DeactivationTime { get; set; }


        public DeliveryAddress? DeliveryAddress { get; set; }
        public ICollection<SaleOrder>? SaleOrders { get; set; }
    }
}
