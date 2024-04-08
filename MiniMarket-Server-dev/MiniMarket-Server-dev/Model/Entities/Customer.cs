namespace MiniMarket_Server_dev.Model.Entities
{
    public class Customer : User
    {
        public ICollection<SaleOrder> SaleOrders { get; set; } = new List<SaleOrder>();      //Might have to be moved to User, depending on how it works out
    }
}
