namespace MiniMarket_API.Application.ViewModels
{
    public class DeliveryAddressView
    {
        public Guid Id { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int? Floor { get; set; }
        public string? Apartment { get; set; }
    }
}
