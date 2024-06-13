namespace MiniMarket_API.Application.DTOs.Requests
{
    public class AddDeliveryAddressDto
    {
        public string Province { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int? Floor { get; set; }
        public string? Apartment { get; set; }
    }
}
