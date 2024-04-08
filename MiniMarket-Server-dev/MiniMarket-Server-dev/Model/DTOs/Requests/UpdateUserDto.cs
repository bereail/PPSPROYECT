namespace MiniMarket_Server_dev.Model.DTOs.Requests
{
    public class UpdateUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
    }
}
