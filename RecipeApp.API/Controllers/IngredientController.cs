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
    public class IngredientController : Controller
    {
        #region Fields
        public readonly IIngredientServices _ingredientServices;
        #endregion

        #region Constructor
        public IngredientController(IIngredientServices ingredientServices)
        {
            _ingredientServices = ingredientServices;
        }
        #endregion

        #region Methods
        // GET: api/values
        [HttpGet]
        [Route("countAll")]
        public async Task<IActionResult> CountIngredient()
        {
            var total = await _ingredientServices.CountAllIngredients();

            if (total == 0) return BadRequest("No ingredients found.");

            return Ok($"There are {total} ingredients in the internal list.");
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetIngredientsName()
        {
            var ingredients = await _ingredientServices.GetAllIngredients();
            if (ingredients == null) return BadRequest("No existing ingredient in DB");

            return Ok(ingredients);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetIngredientsById(int id)
        {
            var ingredient = await _ingredientServices.GetIngredientById(id);
            if (ingredient == null) return BadRequest("The requested id does not exists.");

            return Ok(ingredient);
        }

        // POST api/values
        [HttpPost]
        [Route("New")]
        public async Task<IActionResult> AddingIngredients(Ingredient ingredient)//varios?
        {
            var ingr = await _ingredientServices.GetIngredientByName(ingredient.Name);
            if (ingr != null) return BadRequest("The ingredient already exists.");

            await _ingredientServices.CreateIngredient(ingredient);

            return Ok(Json($"The ingredient {ingredient.Name} was added to the intrernal list."));
        }

        [HttpPut]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditIngredient(int id, Ingredient ingredient)//varios?
        {
            if (ingredient.Id == 0) return BadRequest("The id is set to 0. Verify code.");

            var currentIngredient = await _ingredientServices.GetIngredientById(id);
            if (currentIngredient == null) return BadRequest("The selected id does not exists.");

            var ingredientVerif = await _ingredientServices.GetIngredientByName(ingredient.Name);
            if (ingredientVerif != null) return BadRequest("The ingredient already exists.");

            currentIngredient.Name = ingredient.Name ?? currentIngredient.Name;

            await _ingredientServices.UpdateIngredient(currentIngredient);

            return Ok(Json($"The ingredient {currentIngredient.Name} was updated to {ingredient.Name} in the intrernal list."));
        }

        //[HttpDelete]
        //[Route("sDelete")]
        //public async Task<IActionResult> SoftDeleteIngredient(int ingredientId)
        //{
        //    var ingredient = await _ingredientServices.GetIngredientById(ingredientId);
        //    if (ingredient == null) return BadRequest("No existing ingredient in DB.");


        //    await _ingredientServices.SoftDeleteIngredient(ingredient);

        //    return Ok($"The ingredient {ingredient.Name} was deleted from the list.");
        //}
        #endregion
    }
}
