namespace MiniMarket_Server_dev.Model.Entities
{
    public class Seller : User
    {
        public Guid CompanyCodeId { get; set; }    //Required foreign key
        public CompanyCode CompanyCode { get; set; } = null!;   //Required reference navigation to principal
    }
}
