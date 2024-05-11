using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Implementations;
using MiniMarket_API.Application.Services.Interfaces;

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
        public async Task<IActionResult> CreateProductCategoryAsync([FromBody] AddCategoryDto addCategory)
        {
            var createdCategory = await _productCategoryService.CreateProductCategory(addCategory);
            return Ok(createdCategory);
        }

        [HttpPost("{categoryId}/products")]
        public async Task<IActionResult> CreateProductAsync([FromRoute] Guid categoryId, [FromBody] AddProductDto addProduct)
        {
            addProduct.CategoryId = categoryId;
            var createdProduct = await _productService.CreateProduct(addProduct);
            if (createdProduct == null)
            {
                return BadRequest("Category doesn't exist");
            }
            return Ok(createdProduct);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync([FromQuery] bool? isActive, [FromQuery] string? sortBy = null, [FromQuery] bool isAscending = true)
        {
            //userClaims role
            //if role == Customer
            //isActive == true;

            var getCategories = await _productCategoryService.GetAllCategories(isActive, sortBy, isAscending);

            if (getCategories == null)
            {
                return NotFound("No categories currently active");
            }

            return Ok(getCategories);
        }

        [HttpGet("{categoryId}/products")]
        //FOR GENERAL USE
        public async Task<IActionResult> GetProductsByCategoryAsync([FromRoute] Guid categoryId, [FromQuery] bool? isActive, [FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 15)
        {
            //userClaims role
            //if role == Customer
            //isActive == true;

            var categoryProducts = await _productCategoryService.GetCategoryCollection(categoryId, isActive, filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
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
