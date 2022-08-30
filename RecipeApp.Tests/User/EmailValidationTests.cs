using System;
using RecipeApp.Domain.Validations.User;
using Xunit;

namespace RecipeApp.UnitTests.User
{
    public class EmailValidationTests
    {
        private readonly EmailValidator _validator;

        public EmailValidationTests()
        {
            _validator = new EmailValidator();
        }

        [Theory]
        [InlineData("c@love.com")]
        [InlineData("c@LOVE.com")]
        [InlineData("c+-9@love.com")]
        [InlineData("c.o@love.com")]
        public void IsValid_ValidEmailFormat_ReturnsTrue(string email)
        {
            Assert.True(_validator.IsValidRgx(email));
        }

        [Theory]
        [InlineData("@love.com")]
        [InlineData("c@love.")]
        [InlineData("lovecom")]
        [InlineData("@love.")]
        [InlineData("@.")]
        [InlineData("love.com")]
        [InlineData("c.o@love.com@")]
        public void IsValid_InvalidEmailFormat_ReturnsFalse(string email)
        {
            Assert.False(_validator.IsValidRgx(email));
        }
    }
}
