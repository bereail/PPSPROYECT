using MiniMarket_Server_dev.Application.DTOs.Requests;
using MiniMarket_Server_dev.Application.DTOs;

namespace MiniMarket_Server_dev.Application.Services.Interfaces
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
