using Microsoft.EntityFrameworkCore;
using MiniMarket_Server_dev.Data.Interfaces;
using MiniMarket_Server_dev.Model;
using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MarketDbContext _context;

        public ProductRepository(MarketDbContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProductAsync (Product product)
        {
            product.Id = Guid.NewGuid();
            await _context.Products.AddAsync (product);
            await _context.SaveChangesAsync ();
            return product;
        }

        public async Task<Product?> UpdateProductAsync (Guid id, Product product)
        {
            var getProduct = await _context.Products.FirstOrDefaultAsync (x => x.Id == id);
            if (getProduct == null)
            {
                return null;
            }
            getProduct.Name = product.Name;
            getProduct.Description = product.Description;
            getProduct.Price = product.Price;
            getProduct.Stock = product.Stock;

            await _context.SaveChangesAsync();
            return getProduct;
        }

        public async Task<Product?> DeactivateProductAsync (Guid id)
        {
            var getProductToDeactivate = await _context.Products.FirstOrDefaultAsync (x =>x.Id == id);
            if (getProductToDeactivate == null)
            {
                return null;
            }

            getProductToDeactivate.IsActive = false;
            getProductToDeactivate.DeactivationTime = DateTime.Now;
            await _context.SaveChangesAsync();
            return getProductToDeactivate;
        }

        public async Task<Product?> EraseProductAsync (Guid id)
        {
            var getProductToErase = await _context.Products.FirstOrDefaultAsync (x => x.Id == id && !x.IsActive);
            if (getProductToErase == null)
            {
                return null;
            }
            _context.Products.Remove (getProductToErase);
            await _context.SaveChangesAsync();
            return getProductToErase;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await
                _context.Products
                .ToListAsync();
        }

        public Task<Product?> GetProductByIdAsync (Guid id) 
        {
            return _context.Products
                .FirstOrDefaultAsync (x => x.Id == id);
        }
    }

}
