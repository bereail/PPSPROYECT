using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;

namespace MiniMarket_API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        //FOR SELLER/ADMIN ONLY
        public async Task<IActionResult> GetAllProductsAsync([FromQuery] bool? isActive, [FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 15)
        {
            var getProducts = await productService.GetAllProducts(isActive, filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            if (getProducts == null)
            {
                return NotFound("No products found");
            }
            return Ok(getProducts);
        }
    }
}
