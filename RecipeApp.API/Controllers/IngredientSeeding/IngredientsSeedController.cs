using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml;
using RecipeApp.Domain.Entities;
using RecipeApp.Infrastructure.Context;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecipeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsSeedController : Controller
    {
        #region Atribute
        private readonly MainContext _context;
        private readonly IWebHostEnvironment _env;
        #endregion
        
        #region Constructor
        public IngredientsSeedController(MainContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        #endregion
        // GET: api/values
        [HttpGet]
        [Route("Import")]
        public async Task<ActionResult> Import()
        {
            // prevents non-development environments from running this method
            if (!_env.IsDevelopment())
                throw new SecurityException("Not allowed");

            var path = Path.Combine(_env.ContentRootPath, "Controllers/IngredientSeeding/Food.xlsx");
            using var stream = System.IO.File.OpenRead(path);
            using var excelPackage = new ExcelPackage(stream);

            // get the worksheet
            var worksheet = excelPackage.Workbook.Worksheets[0]; // define how many rows we want to process
            var nEndRow = worksheet.Dimension.End.Row;

            // initialize the record counters
            var numberOfIngredientsAdded = 0;

            // create a lookup dictionary
            // containing all the ingredients already existing
            // into the Database (it will be empty on first run)
            var ingredientsByName = _context.Ingredients
                                                    .AsNoTracking()
                                                    .ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

            // iterates through all rows, skipping the first one
            for (int nRow = 2; nRow <= nEndRow; nRow++)
            {
                var row = worksheet.Cells[nRow, 1, nRow, worksheet.Dimension.End.Column];
                var ingredientName = row[nRow, 2].GetValue<string>();

                // skip this country if it already exists in the database
                if (ingredientsByName.ContainsKey(ingredientName))
                    continue;

                // create the Country entity and fill it with xlsx data
                var ingredient = new Ingredient
                {
                    Name = ingredientName,
                    IsDeleted = false,
                    CreationDate = DateTime.Now
                };

                // add the new country to the DB context
                await _context.Ingredients.AddAsync(ingredient);

                // store the country in our lookup to retrieve its Id later on
                ingredientsByName.Add(ingredientName, ingredient);

                // increment the counter
                numberOfIngredientsAdded++;
            }

            // save all the countries into the Database
            if (numberOfIngredientsAdded > 0)
                await _context.SaveChangesAsync();

            return new JsonResult(new
            {
                Ingredients = numberOfIngredientsAdded
            });
        }
    }
}
