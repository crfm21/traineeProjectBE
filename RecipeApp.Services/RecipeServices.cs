using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Entities.RepositoryInterfaces;
using RecipeApp.Services.ServicesInterfaces;

namespace RecipeApp.Services
{
    public class RecipeServices : IRecipeServices
    {
        #region Fields
        private readonly IRecipeRepository _recipeRepository;
        #endregion

        #region Constructors
        public RecipeServices(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        #endregion

        #region Methods
        public Task AddRating(int recipeId, int rate)
        {
            return _recipeRepository.AddRating(recipeId, rate);
        }

        public Task<int> CountAllRecipes()
        {
            return _recipeRepository.Count();
        }

        public Task<int> CountRecipeByCategory(Categories category)
        {
            return _recipeRepository.CountWhere(r => r.Category == category);
        }

        public Task<int> CountRecipeByCreator(int creatorId)
        {
            return _recipeRepository.CountWhere(r => r.CreatorMemberId == creatorId);
        }

        public Task<int> CountRecipeByDifficulty(ExperienceLevel level)
        {
            return _recipeRepository.CountWhere(r =>  r.Difficulty == level);
        }

        public Task<int> CountRecipeByServings(int servings)
        {
            return _recipeRepository.CountWhere(r => r.Servings == servings);
        }

        public Task CreateRecipe(Recipe recipe)
        {
            return _recipeRepository.Create(recipe);
        }

        public Task DeleteRecipe(Recipe recipe)
        {
            return _recipeRepository.Delete(recipe);
        }

        public Task<IEnumerable<Recipe>> GetAllRecipes()
        {
            return _recipeRepository.GetAll();
        }

        public Task<IEnumerable<Recipe>> GetFavRecipes(int memberId)
        {
            return _recipeRepository.GetFavRecipes(memberId);
        }

        public Task<Recipe> GetRecipeById(int id)
        {
            return _recipeRepository.GetById(id);
        }

        public Task<IEnumerable<Recipe>> GetRecipesByCategory(Categories category)
        {
            return _recipeRepository.GetRecipeByCategory(category);
        }

        public Task<IEnumerable<Recipe>> GetRecipesByCreatorToReview(int creatorId)
        {
            return _recipeRepository.GetRecipeByCreatorOnReview(creatorId);
        }

        public Task<IEnumerable<Recipe>> GetPublishedRecipeByCreator(int memberId)
        {
            return _recipeRepository.GetRecipeByCreatorPublished(memberId);
        }


        public Task<IEnumerable<Recipe>> GetRecipesByDifficulty(ExperienceLevel level)
        {
            return _recipeRepository.GetRecipeByDificulty(level);
        }

        public Task<IEnumerable<Recipe>> GetRecipesByRating(int rating)
        {
            return _recipeRepository.GetRecipeByRating(rating);
        }

        public Task<IEnumerable<Recipe>> GetRecipesByServings(int servings)
        {
            return _recipeRepository.GetRecipeByServings(servings);
        }

        public Task<IEnumerable<Recipe>> GetRecipesByTitle(string title)
        {
            return _recipeRepository.GetRecipeByTitle(title);
        }

        public Task<IEnumerable<Recipe>> GetRecipesToReview()
        {
            return _recipeRepository.GetRecipeToReview();
        }

        public Task SoftDeleteRecipe(Recipe recipe)
        {
            return _recipeRepository.SoftDelete(recipe);
        }

        public Task UpdateRecipe(Recipe recipe)
        {
            return _recipeRepository.Update(recipe);
        }

        public Task Validate(int recipeId)
        {
            return _recipeRepository.Validate(recipeId);
        }
        #endregion
    }
}
