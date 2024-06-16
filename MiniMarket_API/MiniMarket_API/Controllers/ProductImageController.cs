using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Model.Entities;
using System.Security.Claims;

namespace MiniMarket_API.Controllers
{
    [Route("api/products/{productId}/images")]
    [ApiController]
    [Authorize]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageService productImageService;

        public ProductImageController(IProductImageService productImageService)
        {
            this.productImageService = productImageService;
        }

        public ProductImageController()
        {
        }

        [HttpPost]
        public async Task<IActionResult> UploadProductImageAsynC([FromRoute] Guid productId, [FromForm] AddProductImageDto addProductImage)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == typeof(Seller).Name || userRole == typeof(SuperAdmin).Name)
            {
                FileValidation(addProductImage);

                if (ModelState.IsValid)
                {

                    var uploadedImage = await productImageService.HandleImageUpload(productId, addProductImage);

                    return Ok(uploadedImage);
                }

                return BadRequest(ModelState);
            }

            return Forbid();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductImageAsync([FromRoute] Guid productId)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == typeof(Seller).Name || userRole == typeof(SuperAdmin).Name)
            {
                await productImageService.HandleImageDeletion(productId);
                
                return Ok();
            }

            return Forbid();
        }

        [NonAction]
        public void FileValidation(AddProductImageDto addProductImage)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(addProductImage.ImageFile.FileName)))
            {
                ModelState.AddModelError("file", "Image Upload Failed: Unsupported File Extension.");
            }
            
            if (addProductImage.ImageFile.Length > 5242880 )
            {
                ModelState.AddModelError("file", "Image Upload Failed: File Size cannot exceed 5MB.");
            }
        }
    }
}
