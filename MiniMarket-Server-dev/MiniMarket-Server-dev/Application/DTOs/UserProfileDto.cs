using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Application.DTOs
{
    public class UserProfileDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public string UserType { get; set; }
        public ICollection<SaleOrderDto>? SaleOrders { get; set; }
    }
}
