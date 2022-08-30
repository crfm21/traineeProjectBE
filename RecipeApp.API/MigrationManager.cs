using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.HelperClasses;
using RecipeApp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApp.API
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<MainContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();
                    }
                    catch (Exception)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }
            }

            return webHost;
        }

        //https://hovermind.com/aspnet-core/seeding-database-on-application-startup.html
        //public static void Seed(MainContext context)
        //{
        //    // context.Database.EnsureCreated() does not use migrations to create the database and therefore the database that is created cannot be later updated using migrations 
        //    // use context.Database.Migrate() instead
        //    context.Database.Migrate();

        //    if (context.MemberUsers.Any())
        //    {
        //        return;
        //    }

        //    // insert dummy data
        //    context.AddRange(GetDummyMembersList());

        //    if (context.Ingredients.Any())
        //    {
        //        return;
        //    }

        //    context.AddRange(GetDummyIngredientsList());

        //    if (context.Recipes.Any())
        //    {
        //        return;
        //    }

        //    context.AddRange(GetDummyRecipesList());

        //    if (context.IngredientCompositions.Any())
        //    {
        //        return;
        //    }

        //    context.AddRange(GetDummyIngredCompoList());
        //    context.SaveChanges();
        //}

        //public static List<MemberUser> GetDummyMembersList()
        //{
        //    var members = new List<MemberUser> {
        //        new MemberUser
        //        {
        //            NickName = "LeiaS555",
        //            FirstName = "Leia",
        //            LastName = "Skywalker",
        //            Email = "leiaargana@gmail.com",
        //            Password = "buhgfcyj765",
        //            BirthDate = new DateTime(1956,05,16),
        //            Gender = User.Genders.feminine,
        //            Id = 1
        //        },
        //        new MemberUser
        //        {
        //            NickName = "HarryP",
        //            FirstName = "Harry",
        //            LastName = "Potter",
        //            Email = "hp@gmail.com",
        //            Password = "2375ln0924jf",
        //            BirthDate = new DateTime(1990, 03, 17),
        //            Gender = User.Genders.masculine,
        //            Id = 2
        //        },
        //        new MemberUser
        //        {
        //            NickName = "LukeSky",
        //            FirstName = "Luke",
        //            LastName = "Skywalker",
        //            Email = "lsky@gmail.com",
        //            Password = "(&cdiuqw31",
        //            BirthDate = new DateTime(1956, 05, 16),
        //            Gender = User.Genders.masculine,
        //            Id = 3
        //        }
        //};

        //    return members;
        //}
        //public static List<Ingredient> GetDummyIngredientsList()
        //{
        //    var ingredients = new List<Ingredient> {
        //        new Ingredient
        //        {
        //            Name = "Egg yolk",
        //            Id = 1
        //        },
        //        new Ingredient
        //        {
        //            Name = "Sugar",
        //            Id = 2
        //        },
        //        new Ingredient
        //        {
        //            Name = "Milk (Cow)",
        //            Id = 3
        //        },
        //        new Ingredient
        //        {
        //            Name = "Cream",
        //            Id = 4
        //        },
        //        new Ingredient
        //        {
        //            Name = "Vanilla",
        //            Id = 5
        //        },
        //        new Ingredient
        //        {
        //            Name = "Cloves",
        //            Id = 6
        //        },
        //        new Ingredient
        //        {
        //            Name = "Veal",
        //            Id = 7
        //        },
        //        new Ingredient
        //        {
        //            Name = "White onion",
        //            Id = 8
        //        },
        //        new Ingredient
        //        {
        //            Name = "Carrots",
        //            Id = 9
        //        },
        //        new Ingredient
        //        {
        //            Name = "Celery",
        //            Id = 10
        //        },
        //        new Ingredient
        //        {
        //            Name = "Mushrooms",
        //            Id = 11
        //        },
        //        new Ingredient
        //        {
        //            Name = "Butter",
        //            Id = 12
        //        },
        //        new Ingredient
        //        {
        //            Name = "Lemon",
        //            Id = 13
        //        },
        //        new Ingredient
        //        {
        //            Name = "Sour cream",
        //            Id = 14
        //        }
        //};

        //    return ingredients;
        //}
        //public static List<Recipe> GetDummyRecipesList()
        //{
        //    var recipes = new List<Recipe> {
        //    new Recipe
        //        {
        //            Title = "Crème brûlée",
        //            Category = Categories.Dessert,
        //            Difficulty = 0,
        //            Duration = "2:15",
        //            Servings = 4,
        //            Description = "...",
        //            CreatorMemberId = 1,
        //            Id = 1
        //        },
        //        new Recipe
        //        {
        //            Title = "Blanquette de veau",
        //            Category = Categories.Meat,
        //            Difficulty = 0,
        //            Duration = "0:45",
        //            Servings = 6,
        //            Description = "...",
        //            CreatorMemberId = 7,
        //            Id = 2
        //        }
        //};

        //    return recipes;
        //}
        //public static List<IngredientComposition> GetDummyIngredCompoList()
        //{
        //    var recipes = new List<IngredientComposition> {
        //        new IngredientComposition
        //        {
        //            Name = "Egg yolk",
        //            Quantity = 4,
        //            Unit = MesurementUnits.unit,
        //            IngredientId = 1,
        //            RecipeId = 1,
        //            Id = 1
        //        },
        //        new IngredientComposition
        //        {
        //            Name = "Sugar",
        //            Quantity = 130,
        //            Unit = MesurementUnits.g,
        //            IngredientId = 2,
        //            RecipeId = 1,
        //            Id = 2
        //        },
        //        new IngredientComposition
        //        {
        //            Name = "Milk (Cow)",
        //            Quantity = 12,
        //            Unit = MesurementUnits.cL,
        //            IngredientId = 3,
        //            RecipeId = 1,
        //            Id = 3
        //        },
        //        new IngredientComposition
        //        {
        //            Name = "Cream",
        //            Quantity = 35,
        //            Unit = MesurementUnits.cL,
        //            IngredientId = 4,
        //            RecipeId = 1,
        //            Id = 4
        //        },
        //        new IngredientComposition
        //        {
        //            Name = "Vanilla",
        //            Quantity = 2,
        //            Unit = MesurementUnits.unit,
        //            IngredientId = 5,
        //            RecipeId = 1,
        //            Id = 5
        //        },
        //        new IngredientComposition
        //        {
        //            Name = "Cloves",
        //            Quantity = 3,
        //            Unit = MesurementUnits.unit,
        //            IngredientId = 6,
        //            RecipeId = 2,
        //            Id = 6
        //        },
        //        new IngredientComposition
        //        {
        //            Name = "Veal",
        //            Quantity = 1,
        //            Unit = MesurementUnits.kg,
        //            IngredientId = 7,
        //            RecipeId = 2,
        //            Id = 7
        //        },
        //        new IngredientComposition
        //        {
        //            Name = "White onion",
        //            Quantity = 2,
        //            Unit = MesurementUnits.unit,
        //            IngredientId = 8,
        //            RecipeId = 2,
        //            Id = 8
        //        },
        //        new IngredientComposition
        //        {
        //            Name = "Carrots",
        //            Quantity = 2,
        //            Unit = MesurementUnits.unit,
        //            IngredientId = 9,
        //            RecipeId = 2,
        //            Id = 9
        //        },
        //        new IngredientComposition
        //        {
        //            Name = "Celery",
        //            Quantity = 1,
        //            Unit = MesurementUnits.unit,
        //            IngredientId = 10,
        //            RecipeId = 2,
        //            Id = 10
        //        },
        //        new IngredientComposition
        //        {
        //            Name = "Mushrooms",
        //            Quantity = 250,
        //            Unit = MesurementUnits.g,
        //            IngredientId = 11,
        //            RecipeId = 2,
        //            Id = 11
        //        },
        //        new IngredientComposition
        //        {
        //            Name = "Butter",
        //            Quantity = 85,
        //            Unit = MesurementUnits.g,
        //            IngredientId = 12,
        //            RecipeId = 2,
        //            Id = 12
        //        },
        //        new IngredientComposition
        //        {
        //            Name = "Lemon",
        //            Quantity = 0.5,
        //            Unit = MesurementUnits.unit,
        //            IngredientId = 13,
        //            RecipeId = 2,
        //            Id = 13
        //        },
        //        new IngredientComposition
        //        {
        //            Name = "Sour cream",
        //            Quantity = 2,
        //            Unit = MesurementUnits.dL,
        //            IngredientId = 14,
        //            RecipeId = 2,
        //            Id = 14
        //        }
        //};

        //    return recipes;
        //}

    }
}