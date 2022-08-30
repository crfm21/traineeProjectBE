using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RecipeApp.Domain.Core;

namespace RecipeApp.Domain.Entities.RepositoryInterfaces
{
    public interface IFavoriteRecipeRepository
    {
        //CRUD
        Task InsertFavorite(FavoriteRecipe favoriteRecipe);
        //Task Update(FavoriteRecipe favoriteRecipe);
        Task Delete(FavoriteRecipe favoriteRecipe);
        Task SaveChanges();

        Task<IEnumerable<FavoriteRecipe>> GetAll();
        Task<FavoriteRecipe> GetById(int id);
        Task<IEnumerable<FavoriteRecipe>> GetWhere(Expression<Func<FavoriteRecipe, bool>> predicate);

        Task<int> Count();
        Task<int> CountWhere(Expression<Func<FavoriteRecipe, bool>> predicate);

        Task<IEnumerable<FavoriteRecipe>> GetFavoriteByFan(int memberId); //only visible on the member side
    }
}
