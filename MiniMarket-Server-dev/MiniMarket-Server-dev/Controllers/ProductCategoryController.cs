using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_Server_dev.Application.DTOs.Requests;
using MiniMarket_Server_dev.Application.Services.Interfaces;

namespace MiniMarket_Server_dev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpPost("Create Category")]
        public async Task<IActionResult> CreateProductCategoryAsync([FromBody] AddCategoryDto addCategory)
        {
            var createdCategory = await _productCategoryService.CreateProductCategory(addCategory);
            return Ok(createdCategory);
        }
    }
}
