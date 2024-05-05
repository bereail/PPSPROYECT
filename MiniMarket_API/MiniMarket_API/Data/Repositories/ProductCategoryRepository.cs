using Microsoft.EntityFrameworkCore;
using MiniMarket_API.Data.Interfaces;
using MiniMarket_API.Model;
using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Data.Repositories
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
            category.Id = Guid.NewGuid();
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<ProductCategory?> UpdateProductCategoryAsync(Guid id, ProductCategory category)
        {
            var getCategoryToUpdate = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
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
            var getCategoryToDeactivate = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
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
            var getCategoryToErase = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (getCategoryToErase == null)
            {
                return null;
            }
            _context.Categories.Remove(getCategoryToErase);
            await _context.SaveChangesAsync();
            return getCategoryToErase;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllProductCategoriesAsync(bool? isActive, string? sortBy = null, bool isAscending = true)
        {
            var categories = _context.Categories.AsQueryable();

            if (isActive != null)
            {
                categories = isActive.Value ? categories.Where(c => c.IsActive) : categories.Where(c => !c.IsActive);
            }

            //Sorting the categories using Queryable
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    //Returns all categories in a new order by name. If isAscending is true, they will be by ascending order, else, descending. 
                    categories = isAscending ? categories.OrderBy(p => p.CategoryName) : categories.OrderByDescending(p => p.CategoryName);
                }
            }

            return await categories.ToListAsync();
        }

        public Task<ProductCategory?> GetCategoryByIdAsync(Guid id)        //We can tell this to include all it's products here.
        {
            return _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
