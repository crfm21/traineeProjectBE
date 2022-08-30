using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApp.Domain.Entities;

namespace RecipeApp.Services.ServicesInterfaces
{
    public interface IRecipeServices
    {
        Task<int> CountAllRecipes();
        Task<int> CountRecipeByCategory(Categories category);
        Task<int> CountRecipeByDifficulty(ExperienceLevel level);
        Task<int> CountRecipeByServings(int servings);
        Task<int> CountRecipeByCreator(int creatorId);//filter or in member area

        Task CreateRecipe(Recipe recipe);
        Task DeleteRecipe(Recipe recipe);
        Task SoftDeleteRecipe(Recipe recipe);
        Task UpdateRecipe(Recipe recipe);

        Task AddRating(int recipeId, int rate);
        Task Validate(int recipeId);

        Task<IEnumerable<Recipe>> GetAllRecipes();
        Task<Recipe> GetRecipeById(int id);
        Task<IEnumerable<Recipe>> GetRecipesByCategory(Categories category);
        Task<IEnumerable<Recipe>> GetRecipesByDifficulty(ExperienceLevel level);
        Task<IEnumerable<Recipe>> GetRecipesByServings(int servings);
        Task<IEnumerable<Recipe>> GetRecipesByRating(int rating);
        Task<IEnumerable<Recipe>> GetRecipesByTitle(string title);
        Task<IEnumerable<Recipe>> GetPublishedRecipeByCreator(int memberId);

        Task<IEnumerable<Recipe>> GetRecipesToReview();//admin
        Task<IEnumerable<Recipe>> GetRecipesByCreatorToReview(int creatorId);//member

        Task<IEnumerable<Recipe>> GetFavRecipes(int memberId);

    }
}
