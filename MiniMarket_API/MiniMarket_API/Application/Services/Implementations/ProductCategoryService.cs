using AutoMapper;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Application.ViewModels;
using MiniMarket_API.Data.Interfaces;
using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Application.Services.Implementations
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _categoryRepository;
        private readonly IProductRepository productRepository;
        private readonly IProductImageService productImageService;
        private readonly IMapper mapper;
        private readonly IOrderDetailsService orderDetailsService;

        public ProductCategoryService(IProductCategoryRepository categoryRepository,
            IProductRepository productRepository,
            IMapper mapper, IProductImageService productImageService,
            IOrderDetailsService orderDetailsService)
        {
            _categoryRepository = categoryRepository;
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.productImageService = productImageService;
            this.orderDetailsService = orderDetailsService;
        }

        public async Task<CategoryView?> CreateProductCategory(AddCategoryDto addCategoryDto)
        {
            var checkCategoryName = await _categoryRepository.CheckIfCategoryExistsAsync(addCategoryDto.CategoryName);

            if (checkCategoryName)
            {
                return null;
            }

            var categoryToCreate = mapper.Map<ProductCategory>(addCategoryDto);
            await _categoryRepository.CreateProductCategoryAsync(categoryToCreate);

            return mapper.Map<CategoryView>(categoryToCreate);
        }

        public async Task<CategoryView?> UpdateProductCategory(Guid id, AddCategoryDto updateCategoryDto)
        {
            var categoryToUpdate = mapper.Map<ProductCategory>(updateCategoryDto);
            categoryToUpdate = await _categoryRepository.UpdateProductCategoryAsync(id, categoryToUpdate);
            if (categoryToUpdate == null)
            {
                return null;
            }
            return mapper.Map<CategoryView?>(categoryToUpdate);
        }

        public async Task<CategoryViewProducts?> DeactivateProductCategory(Guid id)
        {
            var categoryToDeactivate = await _categoryRepository.DeactivateProductCategoryAsync(id);
            if (categoryToDeactivate == null)
            {
                return null;
            }

            ICollection<Product> productsToCascadeDeactivate = categoryToDeactivate.Products;

            foreach (Product product in productsToCascadeDeactivate)
            {
                await productRepository.DeactivateProductAsync(product.Id);
                continue;
            }

            return mapper.Map<CategoryViewProducts?>(categoryToDeactivate);
        }

        public async Task<CategoryView?> RestoreProductCategory(Guid id)
        {
            var categoryToRestore = await _categoryRepository.RestoreProductCategoryAsync(id);
            if (categoryToRestore == null)
            {
                return null;
            }
            return mapper.Map<CategoryView?>(categoryToRestore);
        }

        public async Task<CategoryViewProducts?> CascadeRestoreProductCategory(Guid id)
        {
            //Upon restoring the category, we will get it's former deletion time, so we can use it as a filter
            var newFilterTime = await _categoryRepository.CascadeRestoreProductCategoryAsync(id);
            if (newFilterTime == null)
            {
                return null;
            }

            //We pass the filterTime alongside the categoryId, to bring back all productIds deactivated with the category
            var productsToRestore = await productRepository.CascadeProductIds(id, newFilterTime.Value);

            //This is to have the category for the return statement
            var restoredCategory = await _categoryRepository.GetCategoryByIdAsync(id);

            //We run a loop to restore each product previously filtered, and add it to the restored category for the return statement
            foreach (Guid productId in productsToRestore)
            {
                var restoredProduct = await productRepository.RestoreProductAsync(productId);
                if (restoredProduct == null) { continue; }
                restoredCategory.Products.Add(restoredProduct);
            }

            return mapper.Map<CategoryViewProducts>(restoredCategory);
        }

        public async Task EraseProductCategory(Guid id)
        {
            // Terribly innefficient, but it works for the time being.
            var productsToHandleDetails = await productRepository.CategoryAllProductIds(id);

            foreach(var productId in productsToHandleDetails)
            {
                await orderDetailsService.HandleProductDetailTermination(productId);
                continue;
            }

            await _categoryRepository.EraseProductCategoryAsync(id);
        }

        public async Task<IEnumerable<CategoryView>?> GetAllCategories(bool? isActive, string? sortBy, bool? isAscending)
        {
            var categories = await _categoryRepository.GetAllProductCategoriesAsync(isActive, sortBy, isAscending ?? true);
            if (!categories.Any())
            {
                return null;
            }
            return mapper.Map<IEnumerable<CategoryView>>(categories);
        }

        public async Task<CategoryViewProducts?> GetCategoryCollection(Guid categoryId, 
            bool? isActive, bool? inStock,
            string? filterOn, string? filterQuery,
            string? sortBy, bool? isAscending, int pageNumber, int pageSize)
        {
            if (pageNumber < 1) { pageNumber = 1; }
            if (pageSize < 1 || pageSize > 25) { pageSize = 15; }

            var getCategory = await _categoryRepository.GetCategoryByIdAsync(categoryId);

            if (getCategory == null || !getCategory.IsActive)
            {
                return null;
            }

            var getProducts = await productRepository.GetAllCategoryProductsAsync(categoryId, isActive, inStock,
                filterOn, filterQuery, sortBy, isAscending ?? true,
                pageNumber, pageSize);

            var productCollection = mapper.Map<ICollection<ProductView>>(getProducts);

            foreach (var product in productCollection)
            {
                var productImage = await productImageService.GetProductImage(product.Id);
                if (productImage == null) { continue; }
                product.Image = productImage;
            }

            var categoryView = mapper.Map<CategoryViewProducts?>(getCategory);

            categoryView.Products = productCollection;

            return categoryView;
        }
    }
}
