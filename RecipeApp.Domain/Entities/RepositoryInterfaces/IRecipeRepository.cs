using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApp.Domain.Core;

namespace RecipeApp.Domain.Entities.RepositoryInterfaces
{
    public interface IRecipeRepository : ICoreRepository<Recipe>
    {
        Task<IEnumerable<Recipe>> GetRecipeByDificulty(ExperienceLevel level);
        Task<IEnumerable<Recipe>> GetRecipeByCategory(Categories category);
        Task<IEnumerable<Recipe>> GetRecipeByTitle(string title);
        Task<IEnumerable<Recipe>> GetRecipeByServings(int servings);
        Task<IEnumerable<Recipe>> GetRecipeByRating(int rating);

        Task<IEnumerable<Recipe>> GetRecipeByCreatorPublished(int creatorId);
        Task<IEnumerable<Recipe>> GetRecipeByCreatorOnReview(int creatorId);

        Task<IEnumerable<Recipe>> GetRecipeToReview();
        Task<IEnumerable<Recipe>> GetFavRecipes(int memberId);

        Task AddRating(int recipeId, int rating);
        Task Validate(int recipeId);
        //Task AddIngredients(IngredientComposition ingredientComposition);

    }
}
