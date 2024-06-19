using MiniMarket_API.Application.DTOs.Requests;
using System.ComponentModel.DataAnnotations;

namespace Tests
{
    public class UserDtoTests
    {
        [Theory]
        [InlineData("ThisIsAValidName", true)]
        [InlineData("Th", false)]
        [InlineData("ThisIsAUserNameThatIsInvalidDueToExceeding50Characters.", false)]
        [InlineData("", false)]
        public void ValidateName(string userName, bool expectedResult)
        {
            //Arrange

            var dto = new CreateUserDto();

            //Act

            var ctx = new ValidationContext(dto) { MemberName = "Name" };

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateProperty(userName, ctx, results);

            //Assert

            Assert.Equal(expectedResult, isValid);
        }

        [Theory]
        [InlineData("user@example.com", true)]
        [InlineData("user@.com", true)]
        [InlineData("@.com", false)]
        [InlineData("", false)]
        public void ValidateEmail(string userEmail, bool expectedResult)
        {
            //Arrange

            var dto = new CreateUserDto();

            //Act

            var ctx = new ValidationContext(dto) { MemberName = "Email" };

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateProperty(userEmail, ctx, results);

            //Assert

            Assert.Equal(expectedResult, isValid);
        }

        [Theory]
        [InlineData("ThisIsAValidPassword", true)]
        [InlineData("NotThis", false)]
        [InlineData("ThisIsAUserPasswordThatIsInvalidDueToExceeding50Characters.", false)]
        [InlineData("", false)]
        public void ValidatePassword(string userPassword, bool expectedResult)
        {
            //Arrange

            var dto = new CreateUserDto();

            //Act

            var ctx = new ValidationContext(dto) { MemberName = "Password" };

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateProperty(userPassword, ctx, results);

            //Assert

            Assert.Equal(expectedResult, isValid);
        }

        [Theory]
        [InlineData("+5424789573", true)]
        [InlineData("54247448122", true)]
        [InlineData("+54132648951321452", false)]
        [InlineData("+3224154625", true)]
        [InlineData("", true)]
        public void ValidatePhoneNumber(string userNumber, bool expectedResult)
        {
            //Arrange

            var dto = new CreateUserDto();

            //Act

            var ctx = new ValidationContext(dto) { MemberName = "PhoneNumber" };

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateProperty(userNumber, ctx, results);

            //Assert

            Assert.Equal(expectedResult, isValid);
        }
    }
}
