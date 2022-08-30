using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApp.Domain.Core;

namespace RecipeApp.Domain.Entities.RepositoryInterfaces
{
    public interface IIngredientCompoRepository : ICoreRepository<IngredientComposition>//only added on recipe Side
    {
        Task<IEnumerable<IngredientComposition>> GetIngredientsByRecipe(int recipeId);
        Task<IEnumerable<IngredientComposition>> GetIngredientsCompoByIngredientId(int ingredientId);
        //Task AddIngredientId(string name);//to link on the infredient
    }
}
