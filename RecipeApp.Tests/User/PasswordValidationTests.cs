using System;
using RecipeApp.Domain.Validations.User;
using Xunit;

namespace RecipeApp.UnitTests.User
{
    public class PasswordValidationTests
    {
        private readonly PasswordValidator _validator;

        public PasswordValidationTests()
        {
            _validator = new PasswordValidator();
        }

        [Theory]
        [InlineData("adminCM_2022")]//simbolo
        [InlineData("B9_ccccc")]//simbolo
        [InlineData("2adCM!!2")]//simbolo
        [InlineData("adminCM*2022")]
        public void IsValid_ValidPassword_ReturnsTrue(string pass)
        {
            Assert.True(_validator.IsValid(pass));
        }

        [Theory]
        [InlineData("adminCM2022")]//simbolo
        [InlineData("adminCM-2022")]//simbolo nao permitido
        [InlineData("admincm*2022")]//caps
        [InlineData("admincm?2022")]//caps
        [InlineData("adminCM.")]//numeros
        [InlineData("ad-CM2")]//8 carateres
        public void IsValid_InvalidPassword_ReturnsFalse(string pass)
        {
            Assert.False(_validator.IsValid(pass));
        }
    }
}
