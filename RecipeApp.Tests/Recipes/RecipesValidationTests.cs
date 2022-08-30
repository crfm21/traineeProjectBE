using System;
using System.Collections.Generic;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Validations.Recipes;
using Xunit;

namespace RecipeApp.UnitTests.Recipes
{
    public class RecipesValidationTests
    {
        private readonly RecipeValidator _validator;

        public RecipesValidationTests()
        {
            _validator = new RecipeValidator();
        }

        [Theory]
        [MemberData(nameof(PassingTestData))]
        public void NoNulls_ValidRecipe_ReturnsTrue(Recipe recipe)
        {
            Assert.True(_validator.NoNulls(recipe));
        }

        [Theory]
        [MemberData(nameof(FailingTestData))]//null + rules
        public void NoNulls_InValidRecipe_ThrowsError(Recipe recipe)
        {
            Assert.Throws<ArgumentNullException>(() => _validator.NoNulls(recipe));
        }

        [Theory]
        [MemberData(nameof(DifficultyTestData))]//null + rules
        public void DifficultyInvalid_InvalidRecipe(Recipe recipe)
        {
            Assert.True(_validator.NoNulls(recipe));
            Assert.False(_validator.DifficultyIsValid((int)recipe.Difficulty));
        }

        [Theory]
        [MemberData(nameof(ServingTestData))]//null + rules
        public void ServingInvalid_InvalidRecipe(Recipe recipe)
        {
            Assert.True(_validator.NoNulls(recipe));
            Assert.Throws<ArgumentOutOfRangeException>(() => _validator.ServingsIsValid(recipe.Servings));
        }

        [Theory]
        [MemberData(nameof(DurationTestData))]//null + rules
        public void DurationInvalid_InvalidRecipe(Recipe recipe)
        {
            Assert.True(_validator.NoNulls(recipe));
            Assert.False(_validator.DurationIsValid(recipe.Duration));
        }

        [Theory]
        [MemberData(nameof(CategoryTestData))]//null + rules
        public void CategoryInvalid_InvalidRecipe(Recipe recipe)
        {
            Assert.True(_validator.NoNulls(recipe));
            Assert.False(_validator.CategoryIsValid((int)recipe.Category));
        }

        //[Theory]
        //[MemberData(nameof(IngCompoListTestData))]//null + rules
        //public void IngCompoListInvalid_InvalidRecipe(Recipe recipe)
        //{
        //    Assert.False(_validator.IngredientCompoListIsValid(recipe.IngredientCompoList.Count));
        //}

        //[Theory]
        //[MemberData(nameof(IngCompoListTestData))]//null + rules
        //public void IngCompoListInvalid_InvalidRecipe(Recipe recipe)
        //{
        //    Assert.True(_validator.NoNulls(recipe));
        //    Assert.False(_validator.IngredientCompoListIsValid(recipe.IngredientCompoList.Count));
        //}

        [Theory]
        [InlineData("00:35")]
        [InlineData("11:00")]
        [InlineData("11:01")]
        [InlineData("00:10")]
        [InlineData("00:11")]
        [InlineData("10:00")]
        public void DurationIsValid_ValidDurationRecipe_ReturnsTrue(string duration)
        {
            Assert.True(_validator.DurationIsValid(duration));
        }

        [Theory]
        [InlineData("00:00")]
        [InlineData("x")]
        [InlineData("11:x0")]
        [InlineData("0x:10")]
        [InlineData("00:xx")]
        public void DurationIsValid_InvalidDurationRecipe_ReturnsFalse(string duration)
        {
            Assert.False(_validator.DurationIsValid(duration));
        }


        [Theory]
        [InlineData(1)]
        [InlineData(12)]
        public void ServingIsValid_ValidServingRecipe_ReturnsTrue(int s)
        {
            Assert.True(_validator.ServingsIsValid(s));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ServingIsValid_InvalidDurationRecipe_ThrowError(int s)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _validator.ServingsIsValid(s));
        }

        [Theory]
        [InlineData(4)]
        [InlineData(null)]
        [InlineData(3)]
        public void DifficultyIsValid_InvalidExperienceLevel_ThrowsError(int? experienceLevel)
        {
            Assert.False(_validator.DifficultyIsValid(experienceLevel));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(0)]
        public void DifficultyIsValid_ValidExperienceLevel_ThrowsError(int experienceLevel)
        {
            Assert.True(_validator.DifficultyIsValid(experienceLevel));
        }

        [Theory]
        [InlineData(9)]
        [InlineData(null)]
        public void CategoryIsValid_InvalidCategory_ReturnsFalse(int? category)
        {
            Assert.False(_validator.CategoryIsValid(category));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(0)]
        public void CategoryIsValid_ValidCategory_ReturnsTrue(int category)
        {
            Assert.True(_validator.CategoryIsValid(category));
        }
        //[Theory]
        //[MemberData(nameof(FailingTestData))]//null + rules
        //public void IsValid_InValidRecipe_ThrowsError(Recipe recipe)
        //{
        //    Assert.Throws<ArgumentNullException>(() => _validator.NoNulls(recipe));
        //}

        //[Theory]
        //[MemberData(nameof(FailingTestData))]
        //public void IsValid_NullTitleRecipe_ThrowsError(Recipe recipe)
        //{
        //    Assert.Throws<ArgumentNullException>("title", () => _validator.IsValid(recipe));
        //}

        //[Theory]
        //[MemberData(nameof(FailingTestData))]
        //public void IsValid_NullDescriptionRecipe_ThrowsError(Recipe recipe)
        //{
        //    Assert.Throws<ArgumentNullException>("description", () => _validator.IsValid(recipe));
        //}

        //[Theory]
        //[MemberData(nameof(FailingTestData))]
        //public void IsValid_NullServingsRecipe_ThrowsError(Recipe recipe)
        //{
        //    Assert.Throws<ArgumentNullException>("servings", () => _validator.IsValid(recipe));
        //}

        public static IEnumerable<object[]> PassingTestData()
        {
            yield return new object[]
            {
                new Recipe
                {
                    Title = "Apple crumble",
                    Duration = "01:30",
                    Servings = 4,
                    Category = Categories.Dessert,
                    Difficulty = ExperienceLevel.Intermediate,
                    Description = "...",
                    CreatorMemberId = 2,
                    IngredientCompoList = new List<IngredientComposition>()
                    {
                        new IngredientComposition
                        {
                            Name = "Flour",
                            Id = 12,
                            Unit = MesurementUnits.kg,
                            Quantity = 1
                        },
                        new IngredientComposition
                        {
                            Name = "Egg",
                            Id = 3,
                            Unit = MesurementUnits.unit,
                            Quantity = 4
                        },
                        new IngredientComposition
                        {
                            Name = "Sugar",
                            Id = 2,
                            Unit = MesurementUnits.g,
                            Quantity = 200
                        },
                        new IngredientComposition
                        {
                            Name = "Apple",
                            Id = 30,
                            Unit = MesurementUnits.unit,
                            Quantity = 2
                        },
                    },
                    Id = 1
                }
            };

        }

        public static IEnumerable<object[]> FailingTestData()
        {
            yield return new object[]
            {
                new Recipe
                {                    
                    Duration = "01:30",
                    Servings = 4,
                    Category = Categories.Dessert,
                    Difficulty = ExperienceLevel.Intermediate,
                    Id = 3,
                    Description = "..."
                }
            };
            yield return new object[]
            {
                new Recipe
                {
                }
            };
            yield return new object[]
            {           
                new Recipe
                {
                    Title = "Apple crumble",
                    Duration = "01:30",
                    Servings = 4,
                    Category = Categories.Dessert,
                    Difficulty = (ExperienceLevel)10,//teste fail
                    Description = "...",
                    CreatorMemberId = 2,
                    //IngredientCompoList = new List<IngredientComposition>()
                    //{
                    //    new IngredientComposition
                    //    {
                    //        Name = "Flour",
                    //        Id = 12,
                    //        Unit = MesurementUnits.kg,
                    //        Quantity = 1
                    //    }
                    //},
                    Id = 1
                }
            };
        }

        public static IEnumerable<object[]> DifficultyTestData()
        {
            yield return new object[]
            {
                new Recipe
                {
                    Title = "Apple crumble",
                    Duration = "01:30",
                    Servings = 4,
                    Category = Categories.Dessert,
                    Difficulty = (ExperienceLevel)10,//teste fail
                    Description = "...",
                    CreatorMemberId = 2,
                    IngredientCompoList = new List<IngredientComposition>()
                    {
                        new IngredientComposition
                        {
                            Name = "Flour",
                            Id = 12,
                            Unit = MesurementUnits.kg,
                            Quantity = 1
                        },
                        new IngredientComposition
                        {
                            Name = "Egg",
                            Id = 3,
                            Unit = MesurementUnits.unit,
                            Quantity = 4
                        },
                        new IngredientComposition
                        {
                            Name = "Sugar",
                            Id = 2,
                            Unit = MesurementUnits.g,
                            Quantity = 200
                        },
                        new IngredientComposition
                        {
                            Name = "Apple",
                            Id = 30,
                            Unit = MesurementUnits.unit,
                            Quantity = 2
                        },
                    },
                    Id = 1
                }
            };

        }

        public static IEnumerable<object[]> ServingTestData()
        {
            yield return new object[]
            {
                new Recipe
                {
                    Title = "Apple crumble",
                    Duration = "01:30",
                    Servings = 0,//teste fail
                    Category = Categories.Dessert,
                    Difficulty = ExperienceLevel.Intermediate,
                    Description = "...",
                    CreatorMemberId = 2,
                    IngredientCompoList = new List<IngredientComposition>()
                    {
                        new IngredientComposition
                        {
                            Name = "Flour",
                            Id = 12,
                            Unit = MesurementUnits.kg,
                            Quantity = 1
                        },
                        new IngredientComposition
                        {
                            Name = "Egg",
                            Id = 3,
                            Unit = MesurementUnits.unit,
                            Quantity = 4
                        },
                        new IngredientComposition
                        {
                            Name = "Sugar",
                            Id = 2,
                            Unit = MesurementUnits.g,
                            Quantity = 200
                        },
                        new IngredientComposition
                        {
                            Name = "Apple",
                            Id = 30,
                            Unit = MesurementUnits.unit,
                            Quantity = 2
                        },
                    },
                    Id = 1
                }
            };

        }

        public static IEnumerable<object[]> DurationTestData()
        {
            yield return new object[]
            {
                new Recipe
                {
                    Title = "Apple crumble",
                    Duration = "00:00",//teste fail
                    Servings = 2,
                    Category = Categories.Dessert,
                    Difficulty = ExperienceLevel.Intermediate,
                    Description = "...",
                    CreatorMemberId = 2,
                    IngredientCompoList = new List<IngredientComposition>()
                    {
                        new IngredientComposition
                        {
                            Name = "Flour",
                            Id = 12,
                            Unit = MesurementUnits.kg,
                            Quantity = 1
                        },
                        new IngredientComposition
                        {
                            Name = "Egg",
                            Id = 3,
                            Unit = MesurementUnits.unit,
                            Quantity = 4
                        },
                        new IngredientComposition
                        {
                            Name = "Sugar",
                            Id = 2,
                            Unit = MesurementUnits.g,
                            Quantity = 200
                        },
                        new IngredientComposition
                        {
                            Name = "Apple",
                            Id = 30,
                            Unit = MesurementUnits.unit,
                            Quantity = 2
                        },
                    },
                    Id = 1
                }
            };

        }

        public static IEnumerable<object[]> CategoryTestData()
        {
            yield return new object[]
            {
                new Recipe
                {
                    Title = "Apple crumble",
                    Duration = "00:35",//teste fail
                    Servings = 2,
                    Category = (Categories)14,
                    Difficulty = ExperienceLevel.Intermediate,
                    Description = "...",
                    CreatorMemberId = 2,
                    IngredientCompoList = new List<IngredientComposition>()
                    {
                        new IngredientComposition
                        {
                            Name = "Flour",
                            Id = 12,
                            Unit = MesurementUnits.kg,
                            Quantity = 1
                        },
                        new IngredientComposition
                        {
                            Name = "Egg",
                            Id = 3,
                            Unit = MesurementUnits.unit,
                            Quantity = 4
                        },
                        new IngredientComposition
                        {
                            Name = "Sugar",
                            Id = 2,
                            Unit = MesurementUnits.g,
                            Quantity = 200
                        },
                        new IngredientComposition
                        {
                            Name = "Apple",
                            Id = 30,
                            Unit = MesurementUnits.unit,
                            Quantity = 2
                        },
                    },
                    Id = 1
                }
            };

        }

        public static IEnumerable<object[]> IngCompoListTestData()
        {
            yield return new object[]
            {
                new Recipe
                {
                    Title = "Apple crumble",
                    Duration = "01:30",
                    Servings = 4,
                    Category = Categories.Dessert,
                    Difficulty = (ExperienceLevel)10,//teste fail
                    Description = "...",
                    CreatorMemberId = 2,
                    //IngredientCompoList = new List<IngredientComposition>()
                    //{
                    //    new IngredientComposition
                    //    {
                    //        Name = "Flour",
                    //        Id = 12,
                    //        Unit = MesurementUnits.kg,
                    //        Quantity = 1
                    //    }
                    //},
                    Id = 1
                }
            };
        }
    }
}
