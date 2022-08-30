using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApp.Domain.Entities;

namespace RecipeApp.Services.ServicesInterfaces
{
    public interface IIngredientCompoServices
    {
        Task<int> CountIngCompoByRecipe(int recipeId);

        Task CreateIngCompo(IngredientComposition ingredientCompo);
        Task DeleteIngCompo(IngredientComposition ingredientCompo);
        Task SoftDeleteIngCompo(IngredientComposition ingredientCompo);
        Task UpdateIngCompo(IngredientComposition ingredientCompo);

        Task<IEnumerable<IngredientComposition>> GetAllIngCompos();
        Task<IngredientComposition> GetIngCompoById(int id);
        Task<IEnumerable<IngredientComposition>> GetIngCompoByRecipe(int recipeId);
        Task<IEnumerable<IngredientComposition>> GetIngCompoByIngredientId(int ingredientId);
    }
}
