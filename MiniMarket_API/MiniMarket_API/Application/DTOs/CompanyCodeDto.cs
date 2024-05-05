namespace MiniMarket_API.Application.DTOs
{
    public class CompanyCodeDto
    {
        public Guid Id { get; set; }
        public string EmployeeCode { get; set; }
        public UserDto? Seller { get; set; }
    }
}
