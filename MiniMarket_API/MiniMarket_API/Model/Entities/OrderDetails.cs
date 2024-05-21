using System.ComponentModel.DataAnnotations.Schema;

namespace MiniMarket_API.Model.Entities
{
    public class OrderDetails
    {
        public int Id { get; set; }

        public int ProductId { get; set; }     //Required FK
        public int OrderId { get; set; }       //Required FK
        public Product Product { get; set; }     //Required navigation property
        public SaleOrder SaleOrder { get; set; }       //Required navigation property
        public decimal DetailPrice { get; set; }
        public int ProductQuantity { get; set; }
    }
}
