using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Entities.RepositoryInterfaces;
using RecipeApp.Services.ServicesInterfaces;

namespace RecipeApp.Services
{
    public class IngredientServices : IIngredientServices
    {
        #region Fields
        private readonly IIngredientRepository _ingredientRepository;
        #endregion

        #region Constructors
        public IngredientServices(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }
        #endregion

        #region Methods
        public Task<int> CountAllIngredients()
        {
            return _ingredientRepository.Count();
        }

        public Task CreateIngredient(Ingredient ingredient)
        {
            return _ingredientRepository.Create(ingredient);
        }

        public Task DeleteIngredient(Ingredient ingredient)
        {
            return _ingredientRepository.Delete(ingredient);
        }

        public Task<IEnumerable<Ingredient>> GetAllIngredients()
        {
            return _ingredientRepository.GetAll();
        }

        public Task<IEnumerable<Ingredient>> GetDeltedIngredients()
        {
            return _ingredientRepository.GetDeleted();
        }

        public Task<Ingredient> GetIngredientById(int id)
        {
            return _ingredientRepository.GetById(id);
        }

        public Task<Ingredient> GetIngredientByName(string name)
        {
            return _ingredientRepository.GetByName(name);
        }

        public Task SoftDeleteIngredient(Ingredient ingredient)
        {
            return _ingredientRepository.SoftDelete(ingredient);
        }

        public Task UpdateIngredient(Ingredient ingredient)
        {
            return _ingredientRepository.Update(ingredient);
        }
        #endregion
    }
}
