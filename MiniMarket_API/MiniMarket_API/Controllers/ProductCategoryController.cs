using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Model.Entities;
using System.Security.Claims;

namespace MiniMarket_API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductService _productService;

        public ProductCategoryController(IProductCategoryService productCategoryService, IProductService productService)
        {
            _productCategoryService = productCategoryService;
            _productService = productService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProductCategoryAsync([FromBody] AddCategoryDto addCategory)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == typeof(Seller).Name || userRole == typeof(SuperAdmin).Name)
            {
                var createdCategory = await _productCategoryService.CreateProductCategory(addCategory);

                if (createdCategory == null)
                {
                    return Conflict("Category Creation Failed: Category already exists.");
                }

                return Ok(createdCategory);
            }

            return Forbid();
        }

        [HttpPut("{categoryId}")]
        [Authorize]
        public async Task<IActionResult> UpdateProductCategoryAsync([FromRoute] Guid categoryId, [FromBody] AddCategoryDto addCategory)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == typeof(Seller).Name || userRole == typeof(SuperAdmin).Name)
            {
                var updatedCategory = await _productCategoryService.UpdateProductCategory(categoryId, addCategory);
                return Ok(updatedCategory);
            }

            return Forbid();
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeactivateProductCategoryAsync([FromRoute] Guid categoryId)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == typeof(Seller).Name || userRole == typeof(SuperAdmin).Name)
            {
                var categoryToDeactivate = await _productCategoryService.DeactivateProductCategory(categoryId);
                if (categoryToDeactivate == null)
                {
                    return NotFound("Category Deactivation Failed: Category Wasn't Found");
                }
                return Ok(categoryToDeactivate);
            }

            return Forbid();
        }

        [HttpDelete("{categoryId}/erase")]
        public async Task<IActionResult> EraseProductCategoryAsync([FromRoute] Guid categoryId)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == typeof(SuperAdmin).Name)
            {
                await _productCategoryService.EraseProductCategory(categoryId);
               
                return NoContent();
            }

            return Forbid();
        }

        [HttpPatch("{categoryId}")]
        [Authorize]
        public async Task<IActionResult> RestoreProductCategoryAsync([FromRoute] Guid categoryId)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == typeof(Seller).Name || userRole == typeof(SuperAdmin).Name)
            {
                var categoryToRestore = await _productCategoryService.RestoreProductCategory(categoryId);
                if (categoryToRestore == null)
                {
                    return NotFound("Category Restoration Failed: Category Wasn't Found");
                }
                return Ok(categoryToRestore);
            }

            return Forbid();
        }

        [HttpPatch("{categoryId}/products")]
        [Authorize]
        public async Task<IActionResult> CascadeRestoreProductCategoryAsync([FromRoute] Guid categoryId)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == typeof(Seller).Name || userRole == typeof(SuperAdmin).Name)
            {
                var categoryToCascadeRestore = await _productCategoryService.CascadeRestoreProductCategory(categoryId);
                if (categoryToCascadeRestore == null)
                {
                    return NotFound("Category Restoration Failed: Category Wasn't Found");
                }

                return Ok(categoryToCascadeRestore);
            }

            return Forbid();
        }

        [HttpPost("{categoryId}/products")]
        [Authorize]
        public async Task<IActionResult> CreateProductAsync([FromRoute] Guid categoryId, [FromBody] AddProductDto addProduct)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == typeof(Seller).Name || userRole == typeof(SuperAdmin).Name)
            {
                var createdProduct = await _productService.CreateProduct(categoryId, addProduct);
                if (createdProduct == null)
                {
                    return Conflict("Product Creation Failed: Product Already Exists");
                }
                return Ok(createdProduct);
            }

            return Forbid();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync([FromQuery] bool? isActive, [FromQuery] string? sortBy = null, [FromQuery] bool isAscending = true)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == null || userRole == typeof(Customer).Name)
            {
                isActive = true;
            }

            var getCategories = await _productCategoryService.GetAllCategories(isActive, sortBy, isAscending);

            if (getCategories == null)
            {
                return NotFound("No Categories Found");
            }

            return Ok(getCategories);
        }

        [HttpGet("{categoryId}/products")]
        //FOR GENERAL USE
        public async Task<IActionResult> GetProductsByCategoryAsync([FromRoute] Guid categoryId,
            [FromQuery] bool? isActive, [FromQuery] bool? inStock,
            [FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 15)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == null || userRole == typeof(Customer).Name)
            {
                filterOn = "Name";
                isActive = true;
                inStock = true;
            }

            var categoryProducts = await _productCategoryService.GetCategoryCollection(categoryId, isActive, inStock, filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            if (categoryProducts == null)
            {
                return BadRequest("Category Doesn't Exist");
            }
            if (categoryProducts.Products == null || !categoryProducts.Products.Any())
            {
                return NotFound("Category is Empty");
            }

            return Ok(categoryProducts);
        }
    }
}
