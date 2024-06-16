using Microsoft.AspNetCore.Http;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Controllers;
using Moq;
using System.Text;

namespace Tests
{
    public class ImageValidationTests
    {
        [Fact]
        public void ValidImage()
        {
            //Arrange

            var mockService = new Mock<IProductImageService>();

            var controller = new ProductImageController(mockService.Object);

            string fileName = "Coca.png";
            int size = 5000;

            var imageDto = new AddProductImageDto
            {
                ImageFile = GetMockImage(fileName, size),
                ImageName = "Coca Cola 1L"
            };

            //Act

            controller.FileValidation(imageDto);

            //Assert

            Assert.True(controller.ModelState.IsValid);

        }

        [Fact]
        public void InvalidImageExtension()
        {
            //Arrange

            var mockService = new Mock<IProductImageService>();

            var controller = new ProductImageController(mockService.Object);

            string fileName = "Coca.gif";
            int size = 5000;

            var imageDto = new AddProductImageDto
            {
                ImageFile = GetMockImage(fileName, size),
                ImageName = "Coca Cola 1L"
            };

            //Act

            controller.FileValidation(imageDto);

            //Assert

            Assert.False(controller.ModelState.IsValid);
        }

        [Fact]
        public void InvalidImageSize()
        {
            //Arrange

            var mockService = new Mock<IProductImageService>();

            var controller = new ProductImageController(mockService.Object);

            string fileName = "Coca.png";
            int size = 5242883;

            var imageDto = new AddProductImageDto
            {
                ImageFile = GetMockImage(fileName, size),
                ImageName = "Coca Cola 1L"
            };

            //Act

            controller.FileValidation(imageDto);

            //Assert

            Assert.False(controller.ModelState.IsValid);
        }

        private IFormFile GetMockImage(string fileName, int size)
        {
            byte[] bytes = Encoding.UTF8.GetBytes("This is a mock image");

            return new FormFile(
                baseStream: new MemoryStream(bytes),
                baseStreamOffset: 0,
                length: size,
                name: "Mock",
                fileName: fileName
                );
        }
    }
}
