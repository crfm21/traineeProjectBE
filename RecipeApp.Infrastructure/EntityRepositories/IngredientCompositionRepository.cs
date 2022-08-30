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
    public class IngredientCompositionRepository : IIngredientCompoRepository
    {
        #region Fields
        private readonly MainContext _context;

        private readonly DbSet<IngredientComposition> _ingredientCompositions;
        #endregion

        #region Constructors
        public IngredientCompositionRepository(MainContext context)
        {
            _context = context;
            _ingredientCompositions = _context.Set<IngredientComposition>();
        }
        #endregion

        #region Methods
        //public Task AddIngredientId(string name)
        //{
        //    throw new NotImplementedException();

        //} PUT IN THE INGREDIENTLIST CONTROLLER

        public Task<int> Count()
        {
            return _ingredientCompositions
                .CountAsync();
        }

        public Task<int> CountWhere(Expression<Func<IngredientComposition, bool>> predicate)
        { 
            return _ingredientCompositions
                .CountAsync(predicate);
        }

        public async Task Create(IngredientComposition ingredientCompo) //insertion of recipeid?
        {
            //ingredientCompo.Name = _context.Ingredients.FindAsync(ingredientCompo.IngredientId).Result.Name; //REVER !! PERCEBER !!!

            if (ingredientCompo == null) throw new ArgumentNullException("Create method repository - ingredientCompo");

            await _ingredientCompositions.AddAsync(ingredientCompo);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(IngredientComposition ingredientCompo)
        {
            if (ingredientCompo == null) throw new ArgumentNullException("Delete method repository - ingredientCompo");

            _ingredientCompositions.Remove(ingredientCompo);
            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<IngredientComposition>> GetAll()
        {
            return GetWhere(ic => ic.IsDeleted == false);
        }

        public async Task<IngredientComposition> GetById(int id)
        {
            return await _ingredientCompositions.FindAsync(id);
        }

        public Task<IEnumerable<IngredientComposition>> GetDeleted()
        {
            return GetWhere(ic => ic.IsDeleted == true);
        }

        public Task<IEnumerable<IngredientComposition>> GetIngredientsCompoByIngredientId(int ingredientId)
        {
            return GetWhere(ic => ic.IngredientId == ingredientId);
        }

        public Task<IEnumerable<IngredientComposition>> GetIngredientsByRecipe(int recipeId)
        {
            return GetWhere(ic => ic.RecipeId == recipeId);
        }

        public async Task<IEnumerable<IngredientComposition>> GetWhere(Expression<Func<IngredientComposition, bool>> predicate)
        {
            return await _ingredientCompositions.Where(predicate).ToListAsync();
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public async Task SoftDelete(IngredientComposition ingredientCompo)
        {
            if (ingredientCompo == null) throw new ArgumentNullException("SoftDelete method repository - ingredientCompo");

            ingredientCompo.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task Update(IngredientComposition ingredientCompo)
        {
            if (ingredientCompo == null) throw new ArgumentNullException("Update method repository - ingredientCompo");

            _ingredientCompositions.Update(ingredientCompo);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
