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
        //Can be used for the search bar, but it will require role validation for the isActive param.
        //Anything else: FOR SELLER/ADMIN ONLY = It's meant to work with a general inventory management interface, and it disregards category structuring.
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

        [HttpGet("offers")]
        //Homepage offers endpoint. Returns a collection of the 7 most discounted products currently available.
        public async Task<IActionResult> GetProductOffersForHomeAsync()
        {
            bool? isActive = true;
            string? filterOn = null;
            string? filterQuery = null;
            string? sortBy = "discount";
            bool? isAscending = true;
            int pageNumber = 1;
            int pageSize = 7;

            var getOffers = await productService.GetAllProducts(isActive, filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            if (getOffers == null)
            {
                return NotFound("No Offers available");
            }
            return Ok(getOffers);
        }
    }
}
