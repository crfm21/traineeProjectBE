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
    public class RecipeRepository : IRecipeRepository
    {
        #region Fields
        private readonly MainContext _context;

        private readonly DbSet<Recipe> _recipes;
        #endregion

        #region Constructors
        public RecipeRepository(MainContext context)
        {
            _context = context;
            _recipes = _context.Set<Recipe>();
        }
        #endregion

        #region Methods
        //public Task AddIngredients(IngredientComposition ingredientComposition)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task AddRating(int recipeId, int rate)//VER COMO VALIDAR UM RATE POR MEMBRO E POR RECEITA
        {
            var recipe = await GetById(recipeId);
            if (recipe == null) throw new ArgumentNullException("Rating method repository - recipe");

            recipe.NumberRatings += 1;
            recipe.TotalRating += rate;

            recipe.Rating = (recipe.TotalRating/recipe.NumberRatings);
            //rating has to be updated at the sameTime ? so perhaps better fill in rating method in here rather than in the entity!
            await _context.SaveChangesAsync();
        }
        //Add as favortie?
        public Task<int> Count()
        {
            return _recipes
                .CountAsync();
        }

        public Task<int> CountWhere(Expression<Func<Recipe, bool>> predicate)
        {
            return _recipes
                .CountAsync(predicate);
        }

        public async Task Create(Recipe recipe)
        {
            if (recipe == null) throw new ArgumentNullException("Create method repository - recipe");

            foreach (var ingCompo in recipe.IngredientCompoList)
            {
                var ingredient = await _context.Ingredients.FindAsync(ingCompo.IngredientId);
                ingCompo.Name = ingredient.Name;
            }

            await _recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Recipe recipe)
        {
            if (recipe == null) throw new ArgumentNullException("Delete method repository - recipe");

            _recipes.Remove(recipe);
            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<Recipe>> GetAll()
        {
            return GetWhere(r => r.IsDeleted == false && r.isPublished == true);
        }

        public async Task<Recipe> GetById(int id)
        {
            return await _recipes.FindAsync(id);
        }

        public Task<IEnumerable<Recipe>> GetDeleted()
        {
            return GetWhere(e => e.IsDeleted == true);
        }

        public async Task<IEnumerable<Recipe>> GetFavRecipes(int memberId)
        {
            var member = await _context.MemberUsers.FindAsync(memberId);
            if (member == null) throw new Exception("The member does not exists.");

            var favIdList = await _context.FavoriteRecipes.Where(f => f.MemberId == memberId).ToListAsync();
            List<Recipe> favRecipes = new List<Recipe>();

            //if (favIdList.Count == 0) throw new Exception("You do not have any favorites yet.");
            if (favIdList.Count == 0) return favRecipes;//para conseguir obter uma lista vazia e nao um erro

            foreach (var item in favIdList)
            {
                int recipeId = item.RecipeId;
                var r = await GetById(recipeId);
                favRecipes.Add(r);
            }
            return favRecipes;
        }

        public Task<IEnumerable<Recipe>> GetRecipeByCategory(Categories category)
        {
            return GetWhere(r => r.Category == category && r.IsDeleted == false && r.isPublished == true);//ALTERAÇAO
        }

        public async Task<IEnumerable<Recipe>> GetRecipeByCreatorOnReview(int creatorId)// for the member side
        {
            var creator = await _context.MemberUsers.FindAsync(creatorId);
            if (creator == null) throw new Exception("No member found with the requested id.");

            return await GetWhere(r => r.IsDeleted == false && r.CreatorMemberId == creatorId && r.isPublished == false);
        }

        public Task<IEnumerable<Recipe>> GetRecipeByCreatorPublished(int creatorId)// for the member side
        {
            return GetWhere(r => r.IsDeleted == false && r.CreatorMemberId == creatorId && r.isPublished == true);
        }

        public Task<IEnumerable<Recipe>> GetRecipeByDificulty(ExperienceLevel level)
        {
            return GetWhere(r => r.Difficulty == level && r.IsDeleted == false && r.isPublished == true);
        }

        public Task<IEnumerable<Recipe>> GetRecipeByRating(int rating)
        {
            return GetWhere(r => r.Rating == rating && r.IsDeleted == false && r.isPublished == true);
        }

        public Task<IEnumerable<Recipe>> GetRecipeByServings(int servings)
        {
            return GetWhere(r => r.Servings == servings && r.IsDeleted == false && r.isPublished == true);
        }

        public Task<IEnumerable<Recipe>> GetRecipeByTitle(string title)
        {
            return GetWhere(r => r.Title.Contains(title) && r.IsDeleted == false && r.isPublished == true);
        }

        public Task<IEnumerable<Recipe>> GetRecipeToReview() //for the admin side
        {
            return GetWhere(r => r.IsDeleted == false && r.isPublished == false);
        }

        public async Task<IEnumerable<Recipe>> GetWhere(Expression<Func<Recipe, bool>> predicate)
        {
            return await _recipes.Where(predicate).ToListAsync();
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public async Task SoftDelete(Recipe recipe)
        {
            if (recipe == null) throw new ArgumentNullException("SoftDelete method repository - recipe");

            recipe.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task Update(Recipe recipe)
        {
            if (recipe == null) throw new ArgumentNullException("Update method repository - recipe");

            _recipes.Update(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task Validate(int recipeId)
        {
            var recipe = await GetById(recipeId);
            if (recipe == null) throw new ArgumentNullException("Validate method repository - recipe");

            recipe.isPublished = true;
            recipe.PublishDate = DateTime.Now;

            _recipes.Update(recipe);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
