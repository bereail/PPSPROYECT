using Microsoft.EntityFrameworkCore;
using MiniMarket_Server_dev.Data.Interfaces;
using MiniMarket_Server_dev.Model;
using MiniMarket_Server_dev.Model.Entities;
using System.Runtime.Versioning;

namespace MiniMarket_Server_dev.Data.Repositories
{
    public class CompanyCodeRepository : ICompanyCodeRepository
    {
        private readonly MarketDbContext _context;

        public CompanyCodeRepository(MarketDbContext context)
        {
            _context = context;
        }

        public async Task<CompanyCode> CreateCompanyCodeAsync(CompanyCode code)
        {
            code.Id = Guid.NewGuid();
            await _context.AddAsync(code);
            await _context.SaveChangesAsync();
            return code;
        }

        public async Task<CompanyCode?> DeactivateCompanyCodeAsync(Guid id)
        {
            var getCodeToDeactivate = await _context.EmployeeCodes
                .Include(e => e.Seller)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (getCodeToDeactivate == null) 
            {
                return null;
            }
            getCodeToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return getCodeToDeactivate;
        }

        public async Task<CompanyCode?> EraseCompanyCodeAsync (Guid id)
        {
            var getCodeToErase = await _context.EmployeeCodes.FirstOrDefaultAsync(e => e.Id == id && !e.IsActive);
            if (getCodeToErase == null)
            {
                return null;
            }
            _context.EmployeeCodes.Remove(getCodeToErase);
            await _context.SaveChangesAsync();
            return getCodeToErase;
        }

        public async Task<IEnumerable<CompanyCode>> GetAllCodesAsync()
        {
            return await 
                _context.EmployeeCodes
                .ToListAsync();
        }

        public Task<CompanyCode?> GetCodeByIdAsync (Guid id)
        {
            return _context.EmployeeCodes
                .FirstOrDefaultAsync (e => e.Id == id);
        }

        public async Task<Guid?> GetCodeIdByHexAsync(string hexCode)        //Untested, not sure if this is the right implementation, but it's the best I have currently
        {
            var codeId = await _context.EmployeeCodes
                .Where(h => h.EmployeeCode == hexCode && h.IsActive && h.Seller == null)        //Finds a code that: A) Matches the hexCode in the db. B) Is currently active. C) Has no seller currently assigned to it.
                .Select(h => h.Id)              //Selects only the Guid Id, which is what we are looking for.
                .FirstOrDefaultAsync ();
            return codeId;
        }

        //public Task<CompanyCode?> GetCodeByHex (string hexCode)             //We can use this to validate unique Hex codes. If there is a match, that means the code already exists
        //{
        //    return _context.EmployeeCodes
        //        .SingleOrDefaultAsync (e => e.EmployeeCode == hexCode);
        //}
    }
}
