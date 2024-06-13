﻿using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Data.Interfaces
{
    public interface ICompanyCodeRepository
    {
        Task<CompanyCode> CreateCompanyCodeAsync(CompanyCode code);
        Task<CompanyCode?> DeactivateCompanyCodeAsync(Guid id);
        Task<CompanyCode?> EraseCompanyCodeAsync(Guid id);
        Task<IEnumerable<CompanyCode>> GetAllCodesAsync();
        Task<CompanyCode?> GetCodeByIdAsync(Guid id);
        Task<Guid?> GetCodeIdByHexAsync(string hexCode);
        //Task<CompanyCode?> GetCodeByHex(string hexCode);

    }
}
