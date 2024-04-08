namespace MiniMarket_Server_dev.Model.Entities
{
    public class OrderDetails
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }     //Required FK
        public Guid OrderId { get; set; }       //Required FK
        public Product Product { get; set; } = null!;       //Required navigation property
        public SaleOrder SaleOrder { get; set; } = null!;        //Required navigation property
        public int ProductQuantity { get; set; }
    }
}
