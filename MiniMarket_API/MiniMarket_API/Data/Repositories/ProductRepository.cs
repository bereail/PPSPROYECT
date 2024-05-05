using Microsoft.EntityFrameworkCore;
using MiniMarket_API.Data.Interfaces;
using MiniMarket_API.Model;
using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MarketDbContext _context;

        public ProductRepository(MarketDbContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            product.Id = Guid.NewGuid();
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateProductAsync(Guid id, Product product)
        {
            var getProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
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

        public async Task<int> HandleProductStockAsync(Guid id, int newStock)
        {
            var getProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            getProduct.Stock = newStock;
            await _context.SaveChangesAsync();
            return getProduct.Stock;
        }

        public async Task<Product?> DeactivateProductAsync(Guid id)
        {
            var getProductToDeactivate = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (getProductToDeactivate == null)
            {
                return null;
            }

            getProductToDeactivate.IsActive = false;
            getProductToDeactivate.DeactivationTime = DateTime.Now;
            await _context.SaveChangesAsync();
            return getProductToDeactivate;
        }

        public async Task<Product?> EraseProductAsync(Guid id)
        {
            var getProductToErase = await _context.Products.FirstOrDefaultAsync(x => x.Id == id && !x.IsActive);
            if (getProductToErase == null)
            {
                return null;
            }
            _context.Products.Remove(getProductToErase);
            await _context.SaveChangesAsync();
            return getProductToErase;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(bool? isActive, string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 50)
        {
            //We define products as Queryable
            var products = _context.Products.AsQueryable();

            //Filtering by isActive using Query. In case of no bool value, it should return all states.
            //Only Sellers or Admins should be able to set it to anything other than true.
            if (isActive != null)
            {
                products = isActive.Value ? products.Where(x => x.IsActive) : products.Where(x => !x.IsActive);
            }

            //Filtering by the product name using Queryable
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    //Returns all products which's name contain what we sent in the Filter Query
                    products = products.Where(p => p.Name.Contains(filterQuery));
                }
            }

            //Sorting the products using Queryable
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    //Returns all products in a new order by name. If isAscending is true, they will be by ascending order, else, descending. 
                    products = isAscending ? products.OrderBy(p => p.Name) : products.OrderByDescending(p => p.Name);
                }
                else if (sortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
                {
                    //Returns all products in a new order by price. If isAscending is true, they will be from highest to lowest, else, opposite.
                    products = isAscending ? products.OrderByDescending(p => p.Price) : products.OrderBy(p => p.Price);
                }
                else if (sortBy.Equals("Discount", StringComparison.OrdinalIgnoreCase))
                {
                    //Returns all products in a new order by Discount. If isAscending is true, they will be from highest to lowest, else, opposite.
                    products = isAscending ? products.OrderByDescending(p => p.Discount) : products.OrderBy(p => p.Discount);
                }
            }

            //Pagination of products using Queryable

            var skipResults = (pageNumber - 1) * pageSize;      //If this results in 0, it will skip it. 

            return await products.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public Task<Product?> GetProductByIdAsync(Guid id)
        {
            return _context.Products
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }

}
