namespace MiniMarket_API.Model.Entities
{
    public class ProductCategory
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Product> Products { get; set; }    //Collection navigation to contain dependents
    }
}
