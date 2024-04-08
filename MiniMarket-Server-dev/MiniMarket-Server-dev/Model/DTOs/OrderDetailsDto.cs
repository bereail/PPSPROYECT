using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Model.DTOs
{
    public class OrderDetailsDto
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }     
        public Guid OrderId { get; set; }      
        public Product Product { get; set; } = null!;      
        public SaleOrder SaleOrder { get; set; } = null!;        
        public int ProductQuantity { get; set; }
    }
}
