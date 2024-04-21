using AutoMapper;
using MiniMarket_Server_dev.Application.DTOs;
using MiniMarket_Server_dev.Application.DTOs.Requests;
using MiniMarket_Server_dev.Application.Services.Interfaces;
using MiniMarket_Server_dev.Data.Interfaces;
using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Application.Services.Implementations
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyCodeRepository _companyCodeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IMapper mapper;

        public CompanyService(ICompanyCodeRepository companyCodeRepository, IUserRepository userRepository, IUserService userService, IMapper mapper)
        {
            _companyCodeRepository = companyCodeRepository;
            _userRepository = userRepository;
            _userService = userService;
            this.mapper = mapper;
        }


        public async Task<CompanyCodeDto?> CreateCompanyCode (AddCompanyCodeDto companyCodeDto)
        {
            var existingCode = await _companyCodeRepository.GetCodeIdByHexAsync(companyCodeDto.EmployeeCode);
            if (existingCode == Guid.Empty)
            {
                var newCode = mapper.Map<CompanyCode>(companyCodeDto);

                await _companyCodeRepository.CreateCompanyCodeAsync(newCode);

                return mapper.Map<CompanyCodeDto>(newCode);
            }
            return null;

        }

        public async Task<CompanyCodeDto?> DeactivateCompanyCode (Guid id)
        {
            var codeToDeactivate = await _companyCodeRepository.DeactivateCompanyCodeAsync(id);
            if (codeToDeactivate == null)
            {
                return null;
            }
            var sellerToDeactivate = codeToDeactivate.Seller;

            if (sellerToDeactivate == null)
            {
                return mapper.Map<CompanyCodeDto?>(codeToDeactivate);
            }
            var deactivatedSeller = await _userService.DeactivateUser(sellerToDeactivate.Id);
            codeToDeactivate.Seller = mapper.Map<Seller?>(deactivatedSeller);
            return mapper.Map<CompanyCodeDto?>(codeToDeactivate);
        }

        public async Task<CompanyCodeDto?> EraseCompanyCode (Guid id)
        {
            var codeToErase = await _companyCodeRepository.EraseCompanyCodeAsync(id);
            return mapper.Map<CompanyCodeDto?>(codeToErase);
        }

        public async Task<IEnumerable<CompanyCodeDto>?> GetAllCompanyCodes()
        {
            var getCodes = await _companyCodeRepository.GetAllCodesAsync();
            if (!getCodes.Any())
            {
                return null;
            }
            return mapper.Map<IEnumerable<CompanyCodeDto>>(getCodes);
        }

        public async Task<CompanyCodeDto?> GetCodeById(Guid id)
        {
            var getCode = await _companyCodeRepository.GetCodeByIdAsync(id);
            if (getCode == null)
            {
                return null;
            }
            return mapper?.Map<CompanyCodeDto?>(getCode);
        }

        public async Task<SellerDto?> CreateSeller (CreateSellerDto createSellerDto)       
        {
            Guid? availableCode = await _companyCodeRepository.GetCodeIdByHexAsync(createSellerDto.HexadecimalCode);              //Checks if the employee code received is both real and available.
            if (availableCode == Guid.Empty)
            {
                return null;                                                                            
            }
            var existingMail = await _userRepository.GetUserIdByEmailAsync(createSellerDto.Email);      //Checks if the mail already exists in the database.
            if (existingMail == Guid.Empty)
            {
                var sellerToCreate = mapper.Map<Seller>(createSellerDto);
                sellerToCreate.CompanyCodeId = availableCode.Value;

                await _userRepository.CreateUserAsync(sellerToCreate);

                return mapper.Map<SellerDto>(sellerToCreate);
            }
            return null;
        }

        public async Task<UserDto?> CreateSuperAdmin (CreateUserDto createSuperAdmin)
        {
            var adminToCreate = mapper.Map<SuperAdmin>(createSuperAdmin);

            var newAdmin = await _userService.CreateUser(adminToCreate);

            return mapper.Map<UserDto?>(newAdmin);
        }

        //public async Task<IEnumerable<SellerDto>?> GetAllSellers()
        //{
        //    var sellers = await 
        //    if (!sellers.Any())
        //    {
        //        return null;
        //    }
        //    return mapper.Map<IEnumerable<SellerDto>?>(sellers);
        //}
    }
}
