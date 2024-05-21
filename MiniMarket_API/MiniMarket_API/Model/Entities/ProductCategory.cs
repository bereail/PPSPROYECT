namespace MiniMarket_API.Model.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? DeactivationTime { get; set; }
        public ICollection<Product> Products { get; set; }    //Collection navigation to contain dependents
    }
}
