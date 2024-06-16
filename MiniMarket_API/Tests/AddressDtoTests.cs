using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Controllers;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace Tests
{
    public class AddressDtoTests
    {

        [Theory]
        [InlineData("ThisIsAValidString", true)]
        [InlineData("Th", false)]
        [InlineData("ThisIsAProvinceStringThatIsInvalidDueToExceeding55Characters.", false)]
        [InlineData("", false)]
        public void ValidateProvinceString(string provinceString, bool expectedResult)
        {
            //Arrange

            var dto = new AddDeliveryAddressDto();

            //Act

            var ctx = new ValidationContext(dto) { MemberName = "Province" };

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateProperty(provinceString, ctx, results);

            //Assert

            Assert.Equal(expectedResult, isValid);

        }

        [Theory]
        [InlineData("Buenos Aires", true)]
        [InlineData("Ciudad Autónoma De Buenos Aires", true)]
        [InlineData("Santiago De Chile", false)]
        [InlineData("Entre Rios", false)]
        [InlineData("", false)]
        public void ValidateProvinceName(string provinceName, bool expectedResult)
        {
            //Arrange

            var mockService = new Mock<IDeliveryAddressService>();

            var controller = new UserAddressController(mockService.Object);

            var dto = new AddDeliveryAddressDto
            {
                Province = provinceName,
            };

            //Act

            controller.AddressValidation(dto);

            //Assert

            Assert.Equal(controller.ModelState.IsValid, expectedResult);

        }

        [Theory]
        [InlineData("ThisIsAValidCity", true)]
        [InlineData("Th", false)]
        [InlineData("ThisIsACityNameThatIsInvalidDueToExceeding45Characters.", false)]
        [InlineData("", false)]
        [InlineData("Ciudad del 8", true)]
        public void ValidateCity(string cityName, bool expectedResult)
        {
            //Arrange

            var dto = new AddDeliveryAddressDto();

            //Act

            var ctx = new ValidationContext(dto) { MemberName = "City" };

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateProperty(cityName, ctx, results);

            //Assert

            Assert.Equal(expectedResult, isValid);

        }

        [Theory]
        [InlineData("ThisIsAValidStreet", true)]
        [InlineData("Th", false)]
        [InlineData("ThisIsAStreetNameAddressThatIsInvalidDueToExceeding60Characters.", false)]
        [InlineData("", false)]
        [InlineData("San Antonio 700", true)]
        public void ValidateStreet(string streetAddrs, bool expectedResult)
        {
            //Arrange

            var dto = new AddDeliveryAddressDto();

            //Act

            var ctx = new ValidationContext(dto) { MemberName = "Street" };

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateProperty(streetAddrs, ctx, results);

            //Assert

            Assert.Equal(expectedResult, isValid);

        }

        [Theory]
        [InlineData(7, true)]
        [InlineData(24, false)]
        [InlineData(-3, false)]
        [InlineData(6.5, false)] //Failed
        public void ValidateFloor(int floor, bool expectedResult)
        {
            //Arrange

            var dto = new AddDeliveryAddressDto();

            //Act

            var ctx = new ValidationContext(dto) { MemberName = "Floor" };

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateProperty(floor, ctx, results);

            //Assert

            Assert.Equal(expectedResult, isValid);

        }

        [Theory]
        [InlineData("B", true)]
        [InlineData("Apart. B", true)]
        [InlineData("Apartamento B", false)]
        [InlineData("A, B", true)]
        [InlineData("", false)] 
        public void ValidateApartment(string apartment, bool expectedResult)
        {
            //Arrange

            var dto = new AddDeliveryAddressDto();

            //Act

            var ctx = new ValidationContext(dto) { MemberName = "Apartment" };

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateProperty(apartment, ctx, results);

            //Assert

            Assert.Equal(expectedResult, isValid);

        }
    }
}
