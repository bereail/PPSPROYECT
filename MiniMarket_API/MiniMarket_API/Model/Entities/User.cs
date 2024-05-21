using System.ComponentModel.DataAnnotations;

namespace MiniMarket_API.Model.Entities
{
    public abstract class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public string UserType { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? DeactivationTime { get; set; }

        public ICollection<SaleOrder>? SaleOrders { get; set; }
    }
}
