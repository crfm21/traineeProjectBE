using System;
using RecipeApp.Domain.Validations.Core;
using Xunit;

namespace RecipeApp.UnitTests.Core
{
    public class IdValidationTests
    {
        private readonly IdValidator _validator;

        public IdValidationTests()
        {
            _validator = new IdValidator();
        }

        [Theory]
        [InlineData(0)]
        public void IsValid_InvalidIdNumber_ReturnsArgumentError(int id)
        {
            Assert.Throws<ArgumentException>(() => _validator.IsValid(id));
        }

        [Theory]
        [InlineData(null)]
        public void IsValid_NullIdNumber_ReturnsArgumentError(int? id)
        {
            Assert.Throws<ArgumentNullException>(() => _validator.IsValid(id));
        }

        [Theory]
        [InlineData(2)]
        public void IsValid_ValidIdNumber_ReturnsTrue(int id)
        {
            Assert.True(_validator.IsValid(id));
        }
    }

}
