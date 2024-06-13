using Microsoft.EntityFrameworkCore;
using MiniMarket_API.Data.Interfaces;
using MiniMarket_API.Model;
using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Data.Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly MarketDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContext;

        public ProductImageRepository(MarketDbContext context, IWebHostEnvironment environment, IHttpContextAccessor httpContext)
        {
            _context = context;
            _environment = environment;
            _httpContext = httpContext;
        }

        public async Task<ProductImage?> UploadImageAsync(ProductImage image)
        {
            var takenProductCheck = await _context.ProductImages.FirstOrDefaultAsync(x => x.ProductId == image.ProductId);

            if (takenProductCheck != null)
            {
                return null;
            }

            var localFilePath = Path.Combine(_environment.ContentRootPath, "Images",
                $"{image.ImageName}{image.ImageExtension}");

            using var stream = new FileStream(localFilePath, FileMode.Create);

            await image.ImageFile.CopyToAsync(stream);

            var urlFilePath = $"{_httpContext.HttpContext.Request.Scheme}://{_httpContext.HttpContext.Request.Host}{_httpContext.HttpContext.Request.PathBase}/Images/{image.ImageName}{image.ImageExtension}";

            image.ImageUrl = urlFilePath;

            image.Id = Guid.NewGuid();

            await _context.ProductImages.AddAsync(image);
            await _context.SaveChangesAsync();

            return image;
        }

        public async Task<ProductImage?> GetImageByProductAsync(Guid productId)
        {
            var getImage = await _context.ProductImages.FirstOrDefaultAsync(x => x.ProductId == productId);
            if (getImage == null)
            {
                return null;
            }
            return getImage;
        }

        public async Task<ProductImage?> DeleteImagebyProductIdAsync(Guid productId)
        {
            var getImage = await _context.ProductImages.FirstOrDefaultAsync(x => x.ProductId == productId);
            if (getImage == null)
            {
                return null;
            }
            var localFilePath = Path.Combine(_environment.ContentRootPath, "Images",
                $"{getImage.ImageName}{getImage.ImageExtension}");

            File.Delete(localFilePath);

            _context.ProductImages.Remove(getImage);
            await _context.SaveChangesAsync();
            return getImage;
        }
    }
}
