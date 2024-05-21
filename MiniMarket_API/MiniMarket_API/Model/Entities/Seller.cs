namespace MiniMarket_API.Model.Entities
{
    public class Seller : User
    {
        public int CompanyCodeId { get; set; }    //Required foreign key
        public CompanyCode CompanyCode { get; set; } = null!;   //Required reference navigation to principal
    }
}
