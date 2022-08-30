using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Entities.RepositoryInterfaces;
using RecipeApp.Services.ServicesInterfaces;

namespace RecipeApp.Services
{
    public class FavoriteRecipeServices : IFavoriteRecipeServices
    {
        #region Fields
        private readonly IFavoriteRecipeRepository _favoriteRecipeRepository;
        #endregion

        #region Constructors
        public FavoriteRecipeServices(IFavoriteRecipeRepository favoriteRecipeRepository)
        {
            _favoriteRecipeRepository = favoriteRecipeRepository;
        }
        #endregion

        #region Methods

        public Task<int> CountAllFavorites()
        {
            return _favoriteRecipeRepository.Count();
        }

        public Task<int> CountFavoritesByMember(int memberId)
        {
            return _favoriteRecipeRepository.CountWhere(f => f.MemberId == memberId);
        }

        public Task CreateFavorite(FavoriteRecipe favorite)
        {
            return _favoriteRecipeRepository.InsertFavorite(favorite);
        }

        public Task DeleteFavorite(FavoriteRecipe favorite)
        {
            return _favoriteRecipeRepository.Delete(favorite);
        }

        public Task<IEnumerable<FavoriteRecipe>> GetAllFavorites()
        {
            return _favoriteRecipeRepository.GetAll();
        }

        public Task<IEnumerable<FavoriteRecipe>> GetFavoritesByMember(int memberId)
        {
            return _favoriteRecipeRepository.GetFavoriteByFan(memberId);
        }

        Task<FavoriteRecipe> IFavoriteRecipeServices.GetFavoriteById(int id)
        {
            return _favoriteRecipeRepository.GetById(id);
        }
        #endregion
    }
}
