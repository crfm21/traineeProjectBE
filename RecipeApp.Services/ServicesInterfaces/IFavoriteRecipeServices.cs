using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApp.Domain.Entities;

namespace RecipeApp.Services.ServicesInterfaces
{
    public interface IFavoriteRecipeServices
    {
        Task<int> CountAllFavorites();
        Task<int> CountFavoritesByMember(int memberId);

        Task CreateFavorite(FavoriteRecipe favorite);
        Task DeleteFavorite(FavoriteRecipe favorite);

        Task<IEnumerable<FavoriteRecipe>> GetAllFavorites();
        Task<FavoriteRecipe> GetFavoriteById(int id);
        Task<IEnumerable<FavoriteRecipe>> GetFavoritesByMember(int memberId);

    }
}
