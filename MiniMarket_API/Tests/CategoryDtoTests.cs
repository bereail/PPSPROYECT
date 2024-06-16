using MiniMarket_API.Application.DTOs.Requests;
using System.ComponentModel.DataAnnotations;

namespace Tests
{
    public class CategoryDtoTests
    {

        [Theory]
        [InlineData("ThisIsAValidName", true)]
        [InlineData("Th", false)]
        [InlineData("ThisIsACategoryNameThatIsInvalidDueToExceeding50Characters", false)]
        [InlineData("", false)]
        public void ValidateName(string categoryName, bool expectedResult)
        {
            //Arrange

            var dto = new AddCategoryDto
            {
                CategoryName = categoryName,
            };

            //Act

            var ctx = new ValidationContext(dto, serviceProvider: null, items: null);

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(dto, ctx, results, true);

            //Assert

            Assert.Equal(expectedResult, isValid);

        }
    }
}