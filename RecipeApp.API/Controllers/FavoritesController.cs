using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeApp.Domain.Entities;
using RecipeApp.Services.ServicesInterfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecipeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : Controller
    {
        #region Fields
        public readonly IFavoriteRecipeServices _favoriteRecipeServices;
        #endregion

        #region Constructor
        public FavoritesController(IFavoriteRecipeServices favoriteRecipeServices)
        {
            _favoriteRecipeServices = favoriteRecipeServices;
        }
        #endregion

        #region Methods
        // GET: api/values

        //counting fav recipes
        [HttpPost]
        [Route("AddToFavorites")]
        public async Task<IActionResult> AddFavorite(FavoriteRecipe favrecipe)
        {
            await _favoriteRecipeServices.CreateFavorite(favrecipe);
            return Ok(Json("The recipe was added to your favorites."));
            //return Ok("The recipe was added to your favorites."); // see how to get recipe title
        }

        [HttpDelete]
        [Route("delete/{recipeId}/{memberId}")]
        public async Task<IActionResult> DeleteRecipe(int recipeId, int memberId)
        {
            var fvList = await _favoriteRecipeServices.GetFavoritesByMember(memberId);
            if (fvList == null) return BadRequest(Json("No recipe found with the requested id."));

            FavoriteRecipe favoriteToDelete = new FavoriteRecipe();

            foreach (var item in fvList)
            {
                if (item.RecipeId == recipeId) favoriteToDelete = item;
            }

            //var rFavToDelete = fvList.ToList().Find(r => r.Id == recipeId);

            await _favoriteRecipeServices.DeleteFavorite(favoriteToDelete);
            return Ok(Json("You deleted the recipe from your favorites."));
        }
        #endregion
    }
}
