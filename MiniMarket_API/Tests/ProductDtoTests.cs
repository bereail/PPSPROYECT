using MiniMarket_API.Application.DTOs.Requests;
using System.ComponentModel.DataAnnotations;


namespace Tests
{
    public class ProductDtoTests
    {
        [Theory]
        [InlineData("ThisIsAValidName", true)]
        [InlineData("Th", false)]
        [InlineData("ThisIsAProductNameThatIsInvalidDueToExceeding50Characters", false)]
        [InlineData("", false)]
        public void ValidateName(string productName, bool expectedResult)
        {
            //Arrange

            var dto = new AddProductDto
            {
            };

            //Act

            var ctx = new ValidationContext(dto) { MemberName = "Name" };

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateProperty(productName, ctx, results);

            //Assert

            Assert.Equal(expectedResult, isValid);

        }

        [Theory]
        [InlineData("ThisIsAValidDescription", true)]
        [InlineData("", true)]
        [InlineData("Th", true)]
        [InlineData("This Is A Product Description That is Invalid Due To Exceeding 200 Characters:" +
            " A refreshing, cold coca cola soda ready to bring you the best in soda beverages, at an affordable price." +
            " The can itself is made out of recycled materials.", false)]
        public void ValidateDescription(string productDescription, bool expectedResult)
        {
            //Arrange

            var dto = new AddProductDto
            {
            };

            //Act

            var ctx = new ValidationContext(dto) { MemberName = "Description" };

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateProperty(productDescription, ctx, results);

            //Assert

            Assert.Equal(expectedResult, isValid);

        }

        [Theory]
        [InlineData(180, true)]
        [InlineData(1978.74, true)]
        [InlineData(-3, false)]
        [InlineData(180.2574, false)]
        [InlineData(99999999999999998, false)]
        public void ValidatePrice(decimal productPrice, bool expectedResult)
        {
            //Arrange

            var dto = new AddProductDto
            {
            };

            //Act

            var ctx = new ValidationContext(dto) { MemberName = "Price" };

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateProperty(productPrice, ctx, results);

            //Assert

            Assert.Equal(expectedResult, isValid);

        }

        [Theory]
        [InlineData(13, true)]
        [InlineData(124, true)]
        [InlineData(-3, false)]
        [InlineData(115.25, false)] //Failed
        [InlineData(130, false)]
        public void ValidateStock(int productStock, bool expectedResult)
        {
            //Arrange

            var dto = new AddProductDto
            {
            };

            //Act

            var ctx = new ValidationContext(dto) { MemberName = "Stock" };

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateProperty(productStock, ctx, results);

            //Assert

            Assert.Equal(expectedResult, isValid);

        }

        [Theory]
        [InlineData(13, true)]
        [InlineData(99, true)]
        [InlineData(0, false)]
        [InlineData(-3, false)] 
        [InlineData(115.25, false)] //Failed
        [InlineData(130, false)]
        public void ValidateDiscount(int productDiscount, bool expectedResult)
        {
            //Arrange

            var dto = new AddProductDto
            {
            };

            //Act

            var ctx = new ValidationContext(dto) { MemberName = "Discount" };

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateProperty(productDiscount, ctx, results);

            //Assert

            Assert.Equal(expectedResult, isValid);

        }
    }
}
