using System;
using System.Collections.Generic;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Validations.Core;

namespace RecipeApp.Domain.Validations.Recipes
{
    public class RecipeValidator
    {
        private readonly IdValidator _idValidator;
        public RecipeValidator()
        {
            _idValidator = new IdValidator();
        }

        public bool NoNulls(Recipe recipe)
        {
            if (recipe.Difficulty < 0
                || recipe.Servings < 0
                || recipe.Category < 0
                || recipe.Title == null
                || recipe?.Id == null
                || recipe.Duration == null
                || recipe.CreatorMemberId <= 0
                || recipe.Description == null
                || recipe.IngredientCompoList == null)
                throw new ArgumentNullException("Required fields.");
            return true;
        }
        //public bool IsValid(Recipe recipe)
        //{
        //    if (recipe?.Difficulty == null || DifficultyIsValid((int)recipe?.Difficulty))
        //        throw new ArgumentNullException("difficulty", "Difficulty level cannot be empty.");
        //    if (recipe?.Servings == null || recipe?.Servings > 0)
        //        throw new ArgumentNullException("servings", "Serving number cannot be empty.");
        //    if (recipe?.Category == null || CategoryIsValid((int)recipe?.Category))
        //        throw new ArgumentNullException("category", "Category level cannot be empty.");
        //    if (recipe?.Title == null)
        //        throw new ArgumentNullException("title", "Title cannot be empty.");
        //    if (recipe?.Id == null)
        //        throw new ArgumentNullException("id", "Id cannot be empty.");
        //    if (recipe?.Duration == null)
        //        throw new ArgumentNullException("duration", "Duration cannot be empty.");
        //    if (recipe?.CreatorMemberId == null)
        //        throw new ArgumentNullException("creatorId", "Creator Id cannot be empty.");
        //    if (recipe?.Description == null)
        //        throw new ArgumentNullException("description", "Description of recipe cannot be empty.");
        //    if (recipe?.IngredientCompoList == null)
        //        throw new ArgumentNullException("ingredientCompoList", "Description of recipe cannot be empty.");
        //    return true;
        //}

        public bool DifficultyIsValid(int? experienceLevel)
        {
            var experienceLevelValues = new int[] { (int)ExperienceLevel.Debutant, (int)ExperienceLevel.Intermediate, (int)ExperienceLevel.Experienced };
            return Array.Exists(experienceLevelValues, eL => eL.Equals(experienceLevel));
        }

        public bool IngredientCompoListIsValid(int ingrListNumber)
        {
            if (ingrListNumber <= 0) return false;
            return true;
        }

        public bool CategoryIsValid(int? category)
        {
            var categoriesValues = new int[] {
                (int)Categories.Amusebouche,
                (int)Categories.Dessert,
                (int)Categories.Fish,
                (int)Categories.Meat,
                (int)Categories.Pastry,
                (int)Categories.Starter,
                (int)Categories.Vegan,
                (int)Categories.Vegetarian,
                (int)Categories.Viennoiserie,
            };
            return Array.Exists(categoriesValues, eL => eL.Equals(category));
        }

        public bool ServingsIsValid(int? s)
        {
            if (s <= 0) throw new ArgumentOutOfRangeException("Servings must be greater than 0", "servings");
            return true;
        }

        public bool DurationIsValid(string duration)
        {
            var separation = duration.Split(':');

            if (separation == null) throw new ArgumentNullException("The duration cannot be null.");
            if (separation.Length > 2) throw new ArgumentException("The duration is not in the valid format.");

            int number, count = 0;
            int[] newArray = new int[2];

            foreach (var item in separation)
            {
                //var verif = Int32.TryParse(item, out number);

                if (item.Length != 2) return false;
                if (item.Length == 2 && int.TryParse(item, out number) == false) return false;
                if (item.Length == 2 && int.TryParse(item, out number) == true)
                {
                    newArray[count] = number;
                    count++;
                };
            }

            if (newArray[0] + newArray[1] <= 0) return false;

            return true;
        }
    }
}
