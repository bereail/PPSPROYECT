using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.ViewModels;

namespace MiniMarket_API.Application.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<CompanyCodeView?> CreateCompanyCode(AddCompanyCodeDto companyCodeDto);
        Task<CompanyCodeView?> DeactivateCompanyCode(Guid id);
        Task<CompanyCodeView?> EraseCompanyCode(Guid id);
        Task<IEnumerable<CompanyCodeView>?> GetAllCompanyCodes();
        Task<CompanyCodeView?> GetCodeById(Guid id);
        Task<UserView?> CreateSeller(CreateSellerDto createSellerDto);
        Task<UserView?> CreateSuperAdmin(CreateUserDto createSuperAdmin);
        //Task<IEnumerable<SellerDto>?> GetAllSellers();
    }
}
