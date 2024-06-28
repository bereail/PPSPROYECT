using AutoMapper;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Application.ViewModels;
using MiniMarket_API.Data.Interfaces;
using MiniMarket_API.Model.Entities;
using MiniMarket_API.Model.Exceptions;

namespace MiniMarket_API.Application.Services.Implementations
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyCodeRepository _companyCodeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly ICustomAuthenticationService _customAuthenticationService;
        private readonly IMapper mapper;

        public CompanyService(ICompanyCodeRepository companyCodeRepository, IUserRepository userRepository, IUserService userService, IMapper mapper, ICustomAuthenticationService customAuthenticationService)
        {
            _companyCodeRepository = companyCodeRepository;
            _userRepository = userRepository;
            _userService = userService;
            this.mapper = mapper;
            _customAuthenticationService = customAuthenticationService;
        }


        public async Task<CompanyCodeView?> CreateCompanyCode(AddCompanyCodeDto companyCodeDto)
        {
            var existingCode = await _companyCodeRepository.GetCodeIdByHexAsync(companyCodeDto.EmployeeCode);
            if (existingCode == Guid.Empty)
            {
                var newCode = mapper.Map<CompanyCode>(companyCodeDto);

                await _companyCodeRepository.CreateCompanyCodeAsync(newCode);

                return mapper.Map<CompanyCodeView>(newCode);
            }
            return null;

        }

        public async Task<CompanyCodeView?> DeactivateCompanyCode(Guid id)
        {
            var codeToDeactivate = await _companyCodeRepository.DeactivateCompanyCodeAsync(id);
            if (codeToDeactivate == null)
            {
                return null;
            }
            var sellerToDeactivate = codeToDeactivate.Seller;

            if (sellerToDeactivate == null)
            {
                return mapper.Map<CompanyCodeView?>(codeToDeactivate);
            }

            await _userService.DeactivateUser(sellerToDeactivate.Id);
            
            var mappedCode = mapper.Map<CompanyCodeView?>(codeToDeactivate);

            return mappedCode;
        }

        public async Task EraseCompanyCode(Guid id)
        {
            await _companyCodeRepository.EraseCompanyCodeAsync(id);
        }

        public async Task<IEnumerable<CompanyCodeView>?> GetAllCompanyCodes()
        {
            var getCodes = await _companyCodeRepository.GetAllCodesAsync();
            if (!getCodes.Any())
            {
                return null;
            }
            return mapper.Map<IEnumerable<CompanyCodeView>>(getCodes);
        }

        public async Task<CompanyCodeView?> GetCodeById(Guid id)
        {
            var getCode = await _companyCodeRepository.GetCodeByIdAsync(id);
            if (getCode == null)
            {
                return null;
            }
            return mapper?.Map<CompanyCodeView?>(getCode);
        }

        public async Task<UserView?> CreateSeller(CreateSellerDto createSellerDto)
        {
            Guid? availableCode = await _companyCodeRepository.GetCodeIdByHexAsync(createSellerDto.HexadecimalCode);              //Checks if the employee code received is both real and available.
            if (availableCode == Guid.Empty)
            {
                throw new ValidationException("Seller Creation Failed: Not a valid Company Code!");
            }
            var existingMail = await _userRepository.GetUserIdByEmailAsync(createSellerDto.Email);      //Checks if the mail already exists in the database.
            if (existingMail == Guid.Empty)
            {
                var sellerToCreate = mapper.Map<Seller>(createSellerDto);

                sellerToCreate.CompanyCodeId = availableCode.Value;
                sellerToCreate.PasswordHash = _customAuthenticationService.PasswordHasher(createSellerDto.Password);

                await _userRepository.CreateUserAsync(sellerToCreate);

                return mapper.Map<UserView>(sellerToCreate);
            }
            return null;
        }

        public async Task<UserView?> CreateSuperAdmin(CreateUserDto createSuperAdmin)
        {
            var adminToCreate = mapper.Map<SuperAdmin>(createSuperAdmin);

            string passwordToHash = createSuperAdmin.Password;

            var newAdmin = await _userService.CreateUser(adminToCreate, passwordToHash);

            return mapper.Map<UserView?>(newAdmin);
        }

        public async Task<UserView?> RestoreSeller(Guid id)
        {
            var getSeller = await _userRepository.GetSellerByIdAsync(id);

            if (getSeller == null || getSeller.IsActive || getSeller.CompanyCodeId == Guid.Empty)
            {
                return null;
            }

            var codeCheck = await _companyCodeRepository.GetCodeByIdAsync(getSeller.CompanyCodeId);

            if (codeCheck == null || !codeCheck.IsActive)
            {
                return null;
            }

            var restoredSeller = await _userRepository.RestoreUserAsync(getSeller.Id);

            return mapper.Map<UserView?>(restoredSeller);
        }
    }
}
