using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Entities.RepositoryInterfaces;
using RecipeApp.Services.ServicesInterfaces;

namespace RecipeApp.Services
{
    public class IngredientCompoServices : IIngredientCompoServices
    {
        #region Fields
        private readonly IIngredientCompoRepository _ingredientCompoRepository;
        #endregion

        #region Constructors
        public IngredientCompoServices(IIngredientCompoRepository ingredientCompoRepository)
        {
            _ingredientCompoRepository = ingredientCompoRepository;
        }
        #endregion

        #region Methods
        public Task<int> CountIngCompoByRecipe(int recipeId)
        {
            return _ingredientCompoRepository.CountWhere(ic => ic.RecipeId == recipeId);
        }

        public Task CreateIngCompo(IngredientComposition ingredientCompo)
        {
            return _ingredientCompoRepository.Create(ingredientCompo);
        }

        public Task DeleteIngCompo(IngredientComposition ingredientCompo)
        {
            return _ingredientCompoRepository.Delete(ingredientCompo);
        }

        public Task<IEnumerable<IngredientComposition>> GetAllIngCompos()
        {
            return _ingredientCompoRepository.GetAll();
        }

        public Task<IngredientComposition> GetIngCompoById(int id)
        {
            return _ingredientCompoRepository.GetById(id);
        }

        public Task<IEnumerable<IngredientComposition>> GetIngCompoByIngredientId(int ingredientId)
        {
            return _ingredientCompoRepository.GetIngredientsCompoByIngredientId(ingredientId);
        }

        public Task<IEnumerable<IngredientComposition>> GetIngCompoByRecipe(int recipeId)
        {
            return _ingredientCompoRepository.GetIngredientsByRecipe(recipeId);
        }

        public Task SoftDeleteIngCompo(IngredientComposition ingredientCompo)
        {
            return _ingredientCompoRepository.SoftDelete(ingredientCompo);
        }

        public Task UpdateIngCompo(IngredientComposition ingredientCompo)
        {
            return _ingredientCompoRepository.Update(ingredientCompo);
        }
        #endregion
    }
}
