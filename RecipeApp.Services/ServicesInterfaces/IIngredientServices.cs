using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApp.Domain.Entities;

namespace RecipeApp.Services.ServicesInterfaces
{
    public interface IIngredientServices
    {
        Task<int> CountAllIngredients();

        Task CreateIngredient(Ingredient ingredient);
        Task DeleteIngredient(Ingredient ingredient);
        Task SoftDeleteIngredient(Ingredient ingredient);
        Task UpdateIngredient(Ingredient ingredient);

        Task<Ingredient> GetIngredientById(int id);
        Task<Ingredient> GetIngredientByName(string name);
        Task<IEnumerable<Ingredient>> GetAllIngredients();
        Task<IEnumerable<Ingredient>> GetDeltedIngredients();

    }
}
