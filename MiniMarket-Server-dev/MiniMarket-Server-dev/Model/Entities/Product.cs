namespace MiniMarket_Server_dev.Model.Entities
{
    public class Product
    {
        public Guid Id { get; set; }    
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } = 0;
        public int Stock { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public DateTime? DeactivationTime { get; set; }

        public Guid CategoryId { get; set; }        //Required FK
        public ProductCategory Category { get; set; } = null!;      //Required navigation property to principal

        public ICollection<OrderDetails> Details { get; set; }      //Principal collective property for OrDtls
    }
}
