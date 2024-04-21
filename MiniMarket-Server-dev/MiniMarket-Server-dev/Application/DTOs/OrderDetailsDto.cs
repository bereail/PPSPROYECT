using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Application.DTOs
{
    public class OrderDetailsDto
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }
        public ProductDto Product { get; set; }
        public int ProductQuantity { get; set; }
        public decimal DetailPrice { get; set; }
    }
}
