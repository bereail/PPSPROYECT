using AutoMapper;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Application.ViewModels;
using MiniMarket_API.Data.Interfaces;
using MiniMarket_API.Model.Entities;
using MiniMarket_API.Model.Exceptions;

namespace MiniMarket_API.Application.Services.Implementations
{
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepository productImageRepository;
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductImageService(IProductImageRepository productImageRepository, IProductRepository productRepository, IMapper mapper)
        {
            this.productImageRepository = productImageRepository;
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<ProductImageView?> HandleImageUpload(Guid productId, AddProductImageDto addProductImage)
        {
            var checkProduct = await productRepository.UnrestrictedGetByIdAsync(productId);
            if (checkProduct == null)
            {
                throw new NotFoundException("Image Upload Failed: Product couldn't be found.");
            }
            var productImage = new ProductImage
            {
                ProductId = productId,
                ImageFile = addProductImage.ImageFile,
                ImageExtension = Path.GetExtension(addProductImage.ImageFile.FileName),
                ImageSize = addProductImage.ImageFile.Length,
                ImageName = addProductImage.ImageName,
            };

            var savedImage = await productImageRepository.UploadImageAsync(productImage);

            return mapper.Map<ProductImageView>(savedImage);
        }

        public async Task<ProductImageBasicView?> HandleImageDeletion(Guid productId)
        {
            var deletedImage = await productImageRepository.DeleteImagebyProductIdAsync(productId);
            if (deletedImage == null) { return null;}

            return mapper.Map<ProductImageBasicView>(deletedImage);
        }

        public async Task<ProductImageBasicView?> GetProductImage(Guid productId)
        {
            var getImage = await productImageRepository.GetImageByProductAsync(productId);
            if (getImage == null)
            {
                return null;
            }
            return mapper.Map<ProductImageBasicView>(getImage);
        }
    }
}
