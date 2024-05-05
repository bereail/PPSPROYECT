using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;

namespace MiniMarket_API.Controllers
{
    [Route("api/categories/{categoryId}/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromRoute] Guid categoryId, [FromBody] AddProductDto addProduct)
        {
            addProduct.CategoryId = categoryId;
            var createdProduct = await productService.CreateProduct(addProduct);
            if (createdProduct == null)
            {
                return BadRequest("Category doesn't exist");
            }
            return Ok(createdProduct);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync([FromQuery] bool? isActive, [FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 50)
        {
            //Here, from the claims of the token, we could make it so that, in case of the user being a Customer, isActive is set to true
            //isActive = true;
            var getProducts = await productService.GetAllProducts(isActive, filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            if (getProducts == null)
            {
                return NotFound("No products found");
            }
            return Ok(getProducts);
        }
    }
}
