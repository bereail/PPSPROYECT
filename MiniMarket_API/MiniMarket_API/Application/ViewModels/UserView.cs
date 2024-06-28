namespace MiniMarket_API.Application.ViewModels
{
    public class UserView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserType { get; set; }
        public bool IsActive { get; set; }
    }
}
