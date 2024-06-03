namespace MiniMarket_API.Application.ViewModels
{
    public class SellerView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string UserType { get; set; }
        public Guid CompanyCodeId { get; set; }    
    }
}
