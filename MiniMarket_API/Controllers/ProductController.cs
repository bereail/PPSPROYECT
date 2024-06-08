using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Model.Entities;
using System.Security.Claims;

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

        [HttpPut("{productId}")]
        [Authorize]
        public async Task<IActionResult> UpdateProductAsync([FromRoute] Guid productId, [FromBody] UpdateProductDto updateProduct)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == typeof(Seller).Name || userRole == typeof(SuperAdmin).Name)
            {
                var productToUpdate = await productService.UpdateProduct(productId, updateProduct);
                if (productToUpdate == null) 
                {
                    return NotFound("Product Update Failed: Product Wasn't Found");
                }
                return Ok(productToUpdate);
            }

            return Forbid();
        }

        [HttpDelete("{productId}")]
        [Authorize]
        public async Task<IActionResult> DeactivateProductAsync([FromRoute] Guid productId)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == typeof(Seller).Name || userRole == typeof(SuperAdmin).Name)
            {
                var productToDeactivate = await productService.DeactivateProduct(productId);
                if (productToDeactivate == null)
                {
                    return NotFound("Product Deactivation Failed: Product Wasn't Found");
                }
                return Ok(productToDeactivate);
            }

            return Forbid();
        }

        [HttpPatch("{productId}")]
        [Authorize]
        public async Task<IActionResult> RestoreProductAsync([FromRoute] Guid productId)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == typeof(Seller).Name || userRole == typeof(SuperAdmin).Name)
            {
                var productToRestore = await productService.RestoreProduct(productId);
                if (productToRestore == null)
                {
                    return BadRequest("Product Deactivation Failed: Product Wasn't Found or is in a Deactivated Category");
                }
                return Ok(productToRestore);
            }

            return Forbid();
        }

        [HttpGet]
        //Can be used for the search bar, but it will require role validation for the isActive param.
        //Anything else: FOR SELLER/ADMIN ONLY = It's meant to work with a general inventory management interface, and it disregards category structuring.
        public async Task<IActionResult> GetAllProductsAsync([FromQuery] bool? isActive, [FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 15)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == null || userRole == typeof(Customer).Name)
            {
                filterOn = "Name";
                isActive = true;
            }

            var getProducts = await productService.GetAllProducts(isActive, filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            if (getProducts == null)
            {
                return NotFound("No Products Found");
            }

            return Ok(getProducts);
        }

        [HttpGet("offers")]
        //Homepage offers endpoint. Returns a collection of the 9 most discounted products currently available.
        public async Task<IActionResult> GetProductOffersForHomeAsync()
        {
            bool? isActive = true;
            string? filterOn = null;
            string? filterQuery = null;
            string? sortBy = "discount";
            bool? isAscending = true;
            int pageNumber = 1;
            int pageSize = 9;

            var getOffers = await productService.GetAllProducts(isActive, filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            if (getOffers == null)
            {
                return NotFound("No Offers Available");
            }
            return Ok(getOffers);
        }
    }
}
