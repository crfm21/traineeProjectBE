using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Models;
using RecipeApp.Services.ServicesInterfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecipeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : Controller
    {
        #region Fields
        public readonly IRecipeServices _recipeServices;
        #endregion

        #region Constructor
        public RecipeController(IRecipeServices recipeServices)
        {
            _recipeServices = recipeServices;
        }
        #endregion

        #region Methods
        // GET: api/values
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllRecipes()
        {
            var recipes = await _recipeServices.GetAllRecipes();

            if (recipes == null) return BadRequest("No recipes found.");

            return Ok(recipes);
        }

        [HttpGet]
        [Route("count/all")]
        public async Task<IActionResult> countAllRecipes()
        {
            var recipes = await _recipeServices.GetAllRecipes();

            if (recipes == null) return BadRequest("No recipes found.");

            return Ok(recipes.Count());
        }

        [HttpGet]
        [Route("category/{categoryIndex}")]
        public async Task<IActionResult> GetRecipesByCategory(Categories categoryIndex)
        {
            var r = await _recipeServices.GetRecipesByCategory(categoryIndex);
            if (r == null) return BadRequest($"There's no recipe in the category {categoryIndex}.");

            return Ok(r);
        }

        [HttpGet]
        [Route("servings/{servings}")]
        public async Task<IActionResult> GetRecipesByServings(int servings)
        {
            var r = await _recipeServices.GetRecipesByServings(servings);
            if (r == null) return BadRequest($"There's no recipe with {servings} servings.");

            return Ok(r);
        }

        [HttpGet]
        [Route("difficulty/{level}")]
        public async Task<IActionResult> GetRecipesByDifficulty(ExperienceLevel level)
        {
            var r = await _recipeServices.GetRecipesByDifficulty(level);
            if (r == null) return BadRequest($"There's no recipe for {level} level.");

            return Ok(r);
        }

        [HttpGet]//ALTER
        [Route("title/{title}")]
        public async Task<IActionResult> GetRecipesByTitle(string title)
        {
            var r = await _recipeServices.GetRecipesByTitle(title);
            if (r == null) return BadRequest($"No recipe found.");

            return Ok(r);
        }

        [HttpGet]
        [Route("byId/{id}")]//ALTER
        public async Task<IActionResult> GetARecipe(int id)
        {
            var r = await _recipeServices.GetRecipeById(id);
            if (r == null) return BadRequest("The requested recipe was not found.");

            return Ok(r);
        }

        [HttpGet]//ALTER
        [Route("publishedByMember/{creatorId}")]
        public async Task<IActionResult> GetPublishedRecipeByCreator(int creatorId)
        {
            var r = await _recipeServices.GetPublishedRecipeByCreator(creatorId);
            if (r == null) return BadRequest("The requested recipe was not found.");

            return Ok(r);
        }

        [HttpGet]//ALTER
        [Route("count/publishedByMember/{creatorId}")]
        public async Task<IActionResult> CountPublishedRecipeByCreator(int creatorId)
        {
            var r = await _recipeServices.GetPublishedRecipeByCreator(creatorId);
            if (r == null) return BadRequest("The requested recipe was not found.");

            return Ok(r.Count());
        }

        [HttpGet]
        [Route("toReview")]
        public async Task<IActionResult> GetRecipesToReview()
        {
            var r = await _recipeServices.GetRecipesToReview();
            if (r == null) return BadRequest("There's no recipe to review.");

            return Ok(r);
        }

        [HttpGet]
        [Route("count/toReview")]
        public async Task<IActionResult> CountRecipesToReview()
        {
            var r = await _recipeServices.GetRecipesToReview();
            if (r == null) return BadRequest("There's no recipe to review.");

            return Ok(r.Count());
        }

        [HttpGet]//ALTER
        [Route("toReview/{creatorId}")]
        public async Task<IActionResult> GetRecipesToReviewByCreator(int creatorId)
        {
            var r = await _recipeServices.GetRecipesByCreatorToReview(creatorId);
            if (r == null) return BadRequest("There's no recipe to review.");

            return Ok(r);
        }

        [HttpGet]//ALTER
        [Route("count/toReview/{creatorId}")]
        public async Task<IActionResult> CountRecipesToReviewByCreator(int creatorId)
        {
            var r = await _recipeServices.GetRecipesByCreatorToReview(creatorId);
            if (r == null) return BadRequest("There's no recipe to review.");

            return Ok(r.Count());
        }

        [HttpGet]//ALTER
        [Route("Favorites/{memberId}")]
        public async Task<IActionResult> GetFavorites(int memberId)
        {
            var r = await _recipeServices.GetFavRecipes(memberId);
            if (r == null) return BadRequest("No favorites yet.");

            return Ok(r);
        }

        [HttpGet]//ALTER
        [Route("count/Favorites/{memberId}")]
        public async Task<IActionResult> CountFavorites(int memberId)
        {
            var r = await _recipeServices.GetFavRecipes(memberId);
            if (r == null) return BadRequest("No favorites yet.");

            return Ok(r.Count());
        }

        [HttpPost]
        [Route("New")]
        public async Task<IActionResult> CreatingNewRecipe(Recipe recipe)
        {
            await _recipeServices.CreateRecipe(recipe);

            return Ok(Json($"The recipe was sent for reviewing. You will receive a notification when it is published."));
        }

        [HttpPut]
        [Route("edit/{recipeId}")]//ALTER
        public async Task<IActionResult> EditingRecipe(int recipeId, RecipeRequestDTO recipe)
        {
            var currentRecipe = await _recipeServices.GetRecipeById(recipeId);

            if (currentRecipe == null) return BadRequest("No recipe found with the requested id.");

            currentRecipe.Category = recipe.Category ?? currentRecipe.Category;
            currentRecipe.Description = recipe.Description ?? currentRecipe.Description;
            currentRecipe.Difficulty = recipe.Difficulty ?? currentRecipe.Difficulty;
            currentRecipe.Duration = recipe.Duration ?? currentRecipe.Duration;
            //currentRecipe.RecipePhotoB64 = recipe.RecipePhotoB64 ?? currentRecipe.RecipePhotoB64;
            currentRecipe.Title = recipe.Title ?? currentRecipe.Title;
            currentRecipe.Servings = (recipe.Servings == 0) ? currentRecipe.Servings : recipe.Servings;

            currentRecipe.UpdateDate = recipe.UpdateDate;

            currentRecipe.IngredientCompoList = recipe.IngredientCompoList ?? currentRecipe.IngredientCompoList;

            await _recipeServices.UpdateRecipe(currentRecipe);

            return Ok(Json("The recipe was successfully edited."));
        }

        [HttpPut]//ALTER
        [Route("validate/{recipeId}")]
        public async Task<IActionResult> ValidateRecipe(int recipeId)
        {
            await _recipeServices.Validate(recipeId);
            return Ok(Json("The recipe was validated. It appears now in the site's published recipes."));
        }

        [HttpPut]
        [Route("Rate/{recipeId}/{rating}")]
        public async Task<IActionResult> RatingARecipe(int recipeId, int rating)
        {
            await _recipeServices.AddRating(recipeId, rating);

            return Ok(Json("Your rating was successfully added"));
        }

        //[HttpPut]
        //[Route("zeroRatings")]
        //public async Task<IActionResult> ReinitializingRating(int recipeId)
        //{
        //    var r = await _recipeServices.GetRecipeById(recipeId);

        //    r.Rating = 0;

        //    await _recipeServices.UpdateRecipe(r);

        //    return Ok("Your rating was successfully initialized.");
        //}

        [HttpPut]
        [Route("sDelete/{recipeId}")]//ALTER
        public async Task<IActionResult> SoftDeleteRecipe(int recipeId)
        {
            var r = await _recipeServices.GetRecipeById(recipeId);
            if (r == null) return BadRequest("No recipe found with the requested id.");

            await _recipeServices.SoftDeleteRecipe(r);
            return Ok(Json($"The recipe {r.Title} was deleted."));
        }

        [HttpDelete]
        [Route("delete/{recipeId}")]//ALTER
        public async Task<IActionResult> DeleteRecipe(int recipeId)
        {
            var r = await _recipeServices.GetRecipeById(recipeId);
            if (r == null) return BadRequest("No recipe found with the requested id.");

            await _recipeServices.DeleteRecipe(r);
            return Ok(Json($"The recipe {r.Title} was deleted."));
        }
        #endregion
    }
}
