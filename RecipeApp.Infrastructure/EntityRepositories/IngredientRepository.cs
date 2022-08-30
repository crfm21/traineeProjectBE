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
    public class IngredientRepository : IIngredientRepository
    {
        #region Fields
        private readonly MainContext _context;

        private readonly DbSet<Ingredient> _ingredients;
        #endregion

        #region Constructors
        public IngredientRepository(MainContext context)
        {
            _context = context;
            _ingredients = _context.Set<Ingredient>();
        }
        #endregion

        #region Methods
        public Task<int> Count()
        {
            return _ingredients
                .CountAsync();
        }

        public Task<int> CountWhere(Expression<Func<Ingredient, bool>> predicate)
        {
            return _ingredients
                .CountAsync(predicate);
        }

        public async Task Create(Ingredient ingredient)
        {
            if (ingredient == null) throw new ArgumentNullException("Create method repository - ingredient");

            await _ingredients.AddAsync(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Ingredient ingredient)
        {
            if (ingredient == null) throw new ArgumentNullException("Delete method repository - ingredient");

            _ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<Ingredient>> GetAll()
        {
            return GetWhere(i => i.IsDeleted == false);
        }

        public async Task<Ingredient> GetById(int id)
        {
            return await _ingredients.FindAsync(id);
        }

        public async Task<Ingredient> GetByName(string name)
        {
            var ingr = await _ingredients.Where(i => i.Name == name).SingleOrDefaultAsync();
            
            return ingr;
        }

        public async Task<IEnumerable<Ingredient>> GetByRecipe(int recipeId) //rever
        {
            var recipe = await _context.Recipes.FindAsync(recipeId);
            List<Ingredient> ingredientList = new List<Ingredient>();

            foreach (var item in recipe.IngredientCompoList)
            {
                if (item.RecipeId == recipeId)
                {
                    var ingredient = await GetById(item.IngredientId);
                    ingredientList.Add(ingredient);
                }
            }
                return ingredientList;
        }

        public Task<IEnumerable<Ingredient>> GetDeleted()
        {
            return GetWhere(i => i.IsDeleted == true);
        }

        public async Task<IEnumerable<Ingredient>> GetWhere(Expression<Func<Ingredient, bool>> predicate)
        {
            return await _ingredients.Where(predicate).ToListAsync();
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public async Task SoftDelete(Ingredient ingredient)
        {
            if (ingredient == null) throw new ArgumentNullException("SoftDelete method repository - ingredient");

            ingredient.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task Update(Ingredient ingredient)
        {
            if (ingredient == null) throw new ArgumentNullException("Update method repository - ingredient");

            _ingredients.Update(ingredient);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
