using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApp.Domain.Core;

namespace RecipeApp.Domain.Entities.RepositoryInterfaces
{
    public interface IIngredientRepository : ICoreRepository<Ingredient>
    {
        Task<IEnumerable<Ingredient>> GetByRecipe(int recipeId);
        Task<Ingredient> GetByName(string name);
    }
}
