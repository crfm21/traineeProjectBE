using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Entities.RepositoryInterfaces;
using RecipeApp.Infrastructure.Context;

namespace RecipeApp.Infrastructure.EntityRepositories
{
    public class FavoriteRecipeRepository : IFavoriteRecipeRepository
    {
        #region Fields
        private readonly MainContext _context;

        private readonly DbSet<FavoriteRecipe> _favoriteRecipes;
        #endregion

        #region Constructors
        public FavoriteRecipeRepository(MainContext context)
        {
            _context = context;
            _favoriteRecipes = _context.Set<FavoriteRecipe>();
        }
        #endregion

        #region Methods
        public Task<int> Count()
        {
            return _favoriteRecipes
                .CountAsync();
        }

        public Task<int> CountWhere(Expression<Func<FavoriteRecipe, bool>> predicate)
        {
            return _favoriteRecipes
                .CountAsync(predicate);
        }

        public async Task InsertFavorite(FavoriteRecipe favoriteRecipe)
        {
            try
            {
                if (favoriteRecipe == null) throw new ArgumentNullException("Create method repository - favoriteRecipe");

                var r = await _context.Recipes.FindAsync(favoriteRecipe.RecipeId);
                if (r == null) throw new Exception("The recipe does not exists.");
                //caso exista já o registo, no front temos que impedir de clicar no botao add

                await _favoriteRecipes.AddAsync(favoriteRecipe);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Delete(FavoriteRecipe favoriteRecipe)
        {
            if (favoriteRecipe == null) throw new ArgumentNullException("Delete method repository - favoriteRecipe");

            _favoriteRecipes.Remove(favoriteRecipe);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FavoriteRecipe>> GetAll()
        {
            return await _favoriteRecipes.ToListAsync(); ;
        }

        public async Task<FavoriteRecipe> GetById(int id)
        {
            return await _favoriteRecipes.FindAsync(id);
        }

        public Task<IEnumerable<FavoriteRecipe>> GetFavoriteByFan(int memberId)
        {
            return GetWhere(f => f.MemberId == memberId);
        }

        public async Task<IEnumerable<FavoriteRecipe>> GetWhere(Expression<Func<FavoriteRecipe, bool>> predicate)
        {
            return await _favoriteRecipes.Where(predicate).ToListAsync();
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }
        #endregion

    }
}
