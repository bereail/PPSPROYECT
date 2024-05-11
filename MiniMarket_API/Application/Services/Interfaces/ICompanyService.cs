using MiniMarket_API.Application.DTOs;
using MiniMarket_API.Application.DTOs.Requests;

namespace MiniMarket_API.Application.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<CompanyCodeDto?> CreateCompanyCode(AddCompanyCodeDto companyCodeDto);
        Task<CompanyCodeDto?> DeactivateCompanyCode(Guid id);
        Task<CompanyCodeDto?> EraseCompanyCode(Guid id);
        Task<IEnumerable<CompanyCodeDto>?> GetAllCompanyCodes();
        Task<CompanyCodeDto?> GetCodeById(Guid id);
        Task<SellerDto?> CreateSeller(CreateSellerDto createSellerDto);
        Task<UserDto?> CreateSuperAdmin(CreateUserDto createSuperAdmin);
        //Task<IEnumerable<SellerDto>?> GetAllSellers();
    }
}
