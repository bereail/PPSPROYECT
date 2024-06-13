namespace MiniMarket_API.Application.ViewModels
{
    public class CompanyCodeView
    {
        public Guid Id { get; set; }
        public string EmployeeCode { get; set; }
        public UserView? Seller { get; set; }
    }
}
