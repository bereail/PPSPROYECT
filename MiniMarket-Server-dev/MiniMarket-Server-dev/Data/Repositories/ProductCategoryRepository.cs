using Microsoft.EntityFrameworkCore;
using MiniMarket_Server_dev.Data.Interfaces;
using MiniMarket_Server_dev.Model;
using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Data.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly MarketDbContext _context;

        public ProductCategoryRepository(MarketDbContext context)
        {
            _context = context;
        }

        public async Task<ProductCategory> CreateProductCategoryAsync(ProductCategory category)
        {
            category.Id = Guid.NewGuid ();
            await _context.Categories.AddAsync (category);
            await _context.SaveChangesAsync ();
            return category;
        }

        public async Task<ProductCategory?> UpdateProductCategoryAsync(Guid id, ProductCategory category)
        {
            var getCategoryToUpdate = await _context.Categories.FirstOrDefaultAsync (c => c.Id == id);
            if (getCategoryToUpdate == null)
            {
                return null;
            }
            getCategoryToUpdate.CategoryName = category.CategoryName;
            await _context.SaveChangesAsync();
            return getCategoryToUpdate;
        }

        public async Task<ProductCategory?> DeactivateProductCategoryAsync(Guid id)
        {
            var getCategoryToDeactivate = await _context.Categories.FirstOrDefaultAsync (c => c.Id == id);
            if (getCategoryToDeactivate == null)
            {
                return null;
            }
            getCategoryToDeactivate.IsActive = false;
            //getCategoryToDeactivate.DeactivationTime = DateTime.UtcNow       Would be placed here, but for the time being, Categories don't have it. Might be added in later for cascade delete.
            await _context.SaveChangesAsync();
            return getCategoryToDeactivate;
        }

        public async Task<ProductCategory?> EraseProductCategoryAsync(Guid id)
        {
            var getCategoryToErase = await _context.Categories.FirstOrDefaultAsync (c => c.Id == id);
            if (getCategoryToErase == null)
            {
                return null;
            }
            _context.Categories.Remove (getCategoryToErase);
            await _context.SaveChangesAsync();
            return getCategoryToErase;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllProductCategoriesAsync()
        {
            return await 
                _context.Categories
                .ToListAsync();
        }

        public Task<ProductCategory?> GetCategoryByIdAsync (Guid id)
        {
            return _context.Categories
                .FirstOrDefaultAsync (c => c.Id == id);
        }
    }
}
