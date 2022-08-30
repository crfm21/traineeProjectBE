using System;
using System.Collections;
using System.Collections.Generic;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Validations.Core;
using RecipeApp.Domain.Validations.Ingredients;
using Xunit;

namespace RecipeApp.UnitTests.Ingredients
{
    public class IngredientsValidation
    {
        private readonly IngredientValidator _validator;
        private readonly IdValidator _idValidator;

        public IngredientsValidation()
        {
            _validator = new IngredientValidator();
            _idValidator = new IdValidator();
        }

        [Theory]
        [MemberData(nameof(PassingTestData))]
        public void NoNulls_ValidIngredientModel_ReturnsTrue(Ingredient ingredient)
        {
            //Arrange
            //Act
            //Assert
            Assert.True(_validator.NoNulls(ingredient));
        }

        [Theory]
        [MemberData(nameof(FailingTestData))]
        public void NoNulls_InvalidIngredientModel_ReturnsFalse(Ingredient ingredient)
        {
            //Arrange
            //Act
            //Assert
            Assert.False(_validator.NoNulls(ingredient));
        }

        [Theory]
        [MemberData(nameof(PassingIngrCompoTestData))]
        public void IngCompoNoNulls_ValidIngredientCompo_ReturnsTrue(IngredientComposition ingCompo)
        {
            //Arrange
            //Act
            //Assert
            Assert.True(_validator.IngCompoNoNulls(ingCompo));
        }

        [Theory]
        [MemberData(nameof(FailingIngrCompoTestData))]
        public void IngCompoNoNulls_InvalidIngredientCompo_ThrowsError(IngredientComposition ingCompo)
        {
            //Arrange
            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => _validator.IngCompoNoNulls(ingCompo));
        }

        [Theory]
        [MemberData(nameof(FailingIngrCompoUnitTestData))]
        public void UnitIsValid_InvalidUnitAndIngredientCompo_ReturnsFalse(IngredientComposition ingCompo)
        {
            //Arrange
            //Act
            //Assert
            Assert.True(_validator.IngCompoNoNulls(ingCompo));
            Assert.True(_validator.IngCompoNameIsValid(ingCompo.Name));
            Assert.True(_idValidator.IsValid(ingCompo.IngredientId));
            Assert.True(_validator.IngCompoQuantityIsValid(ingCompo.Quantity));

            Assert.False(_validator.UnitIsValid((int)ingCompo.Unit));
        }

        [Theory]
        [MemberData(nameof(FailingIngrCompoNameTestData))]
        public void InvalidName_InvalidNameAndIngredientCompo_ThrowsError(IngredientComposition ingCompo)
        {
            //Arrange
            //Act
            //Assert
            Assert.True(_validator.IngCompoNoNulls(ingCompo));
            Assert.True(_validator.IngCompoQuantityIsValid(ingCompo.Quantity));
            Assert.True(_idValidator.IsValid(ingCompo.IngredientId));
            Assert.True(_validator.UnitIsValid((int)ingCompo.Unit));

            Assert.Throws<ArgumentOutOfRangeException>(() => _validator.IngCompoNameIsValid(ingCompo.Name));
        }

        [Theory]
        [MemberData(nameof(FailingIngrCompoQuantityTestData))]
        public void InvalidQuantity_InvalidIngredientCompo_ThrowsError(IngredientComposition ingCompo)
        {
            //Arrange
            //Act
            //Assert
            Assert.True(_validator.UnitIsValid((int)ingCompo.Unit));
            Assert.True(_validator.IngCompoNameIsValid(ingCompo.Name));
            Assert.True(_idValidator.IsValid(ingCompo.IngredientId));

            Assert.Throws<ArgumentOutOfRangeException>(() => _validator.IngCompoQuantityIsValid(ingCompo.Quantity));
        }

        [Theory]
        [MemberData(nameof(FailingIngrCompoIngredientIdTestData))]
        public void InvalidIngredientId_InvalidIngredientCompo_ThrowsError(IngredientComposition ingCompo)
        {
            //Arrange
            //Act
            //Assert
            Assert.True(_validator.UnitIsValid((int)ingCompo.Unit));
            Assert.True(_validator.IngCompoNameIsValid(ingCompo.Name));

            Assert.Throws<ArgumentException>(() => _idValidator.IsValid(ingCompo.IngredientId));
        }

        [Theory]
        [InlineData(11)]
        public void UnitIsValid_InvalidUnit_ReturnsFalse(int unit)
        {
            Assert.False(_validator.UnitIsValid(unit));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        public void UnitIsValid_ValidUnit_ReturnsTrue(int unit)
        {
            Assert.True(_validator.UnitIsValid(unit));
        }

        //  Arrange:

        public static IEnumerable<object[]> PassingTestData()
        {
            yield return new object[]
            {
                new Ingredient
                {
                    Id = 1,
                    Name = "Lobster"
                }
            };
            yield return new object[]
            {
                new Ingredient
                {
                    Id = 2,
                    Name = "Cocoa"
                }
            };
            //yield return new object[]//teste para falahr
            //{
            //    new Ingredient
            //    {
            //        Id = 0,
            //        Name = "Lobster"
            //    }
            //};
        }

        public static IEnumerable<object[]> FailingTestData()
        {
            yield return new object[]
            {
                new Ingredient
                {
                    Id = 0,
                    Name = "Lobster"
                }
            };
            yield return new object[]
            {
                new Ingredient
                {
                    Id = 1,
                    Name = ""
                }
            };
            yield return new object[]
            {
                new Ingredient
                {
                    Name = "Chocolate"
                }
            };
            yield return new object[]
            {
                new Ingredient
                {
                    Id = 2
                }
            };
            yield return new object[]
            {
                new Ingredient {}
            };
            //teste para falahr
            //yield return new object[]
            //{
            //    new Ingredient
            //    {
            //        Id = 2,
            //        Name = "Cocoa"
            //    }
            //};
        }

        public static IEnumerable<object[]> PassingIngrCompoTestData()
        {
            yield return new object[]
            {
                new IngredientComposition
                {
                    Id = 1,
                    Name = "Lobster",
                    Quantity = 1,
                    Unit = MesurementUnits.unit,
                    IngredientId = 3
                }
            };
        }

        public static IEnumerable<object[]> FailingIngrCompoTestData()
        {
            //yield return new object[]
            //{
            //    new IngredientComposition
            //    {
            //    }
            //};
            //yield return new object[]
            //{
            //    new IngredientComposition
            //    {                    
            //        Name = "Lobster",
            //        Quantity = 1,
            //        Unit = MesurementUnits.unit,
            //        IngredientId = 3
            //    }
            //};
            yield return new object[]
            {
                new IngredientComposition
                {
                    Id = 1,
                    Quantity = 1,
                    IngredientId = 3,

                }
            };
            yield return new object[]
            {
                new IngredientComposition
                {
                    Id = 1,
                    Name = "Lobster",
                    IngredientId = 3,
                }
            };
            yield return new object[]
{
                new IngredientComposition
                {
                    Id = 1,
                    Name = "Lobster",
                    Quantity = 1,
                }
};
            yield return new object[]
            {  
                new IngredientComposition
                {
                    Name = "Lobster",
                    Quantity = 1,
                    IngredientId = 3,
                }
            };
            //yield return new object[]
            //{
            //    new IngredientComposition
            //    {
            //        Id = 1,
            //        Name = "Lobster",
            //        Quantity = 1,
            //        Unit = MesurementUnits.unit,
            //    }
            //};
        }

        public static IEnumerable<object[]> FailingIngrCompoNameTestData()
        {
            yield return new object[]
            {
                new IngredientComposition
                {
                    Id = 1,
                    Name = "",
                    Quantity = 1,
                    Unit = MesurementUnits.unit,
                    IngredientId = 3
                }
            };
        }

        public static IEnumerable<object[]> FailingIngrCompoQuantityTestData()
        {
            yield return new object[]
            {
                new IngredientComposition
                {
                    Id = 1,
                    Name = "Lobster",
                    Quantity = 0,
                    Unit = MesurementUnits.unit,
                    IngredientId = 3
                }
            };
        }

        public static IEnumerable<object[]> FailingIngrCompoUnitTestData()
        {
            yield return new object[]
            {
                new IngredientComposition
                {
                    Id = 1,
                    Name = "Lobster",
                    Quantity = 1,
                    Unit = (MesurementUnits)11,
                    IngredientId = 3
                }
            };
        }

        public static IEnumerable<object[]> FailingIngrCompoIngredientIdTestData()
        {
            yield return new object[]
            {
                new IngredientComposition
                {
                    Id = 1,
                    Name = "Lobster",
                    Quantity = 1,
                    Unit = MesurementUnits.unit,
                    IngredientId = 0
                }
            };
        }

        //public class IngredientClassData : IEnumerable<object[]>
        //{
        //    public IEnumerator<object[]> GetEnumerator()
        //    {
        //        throw new NotImplementedException();
        //    }

        //    IEnumerator IEnumerable.GetEnumerator()
        //    {
        //        throw new NotImplementedException();
        //    }
        //}


    }
}
