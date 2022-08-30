using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using RecipeApp.Domain.Core;

namespace RecipeApp.Domain.Entities
{
    [Table("Ingredients")]

    public class Ingredient : CoreEntity
    {
        [Required(ErrorMessage = "Ingredient name cannot be empty.")]
        public string Name { get; set; }

        #region Navigation properties
        [JsonIgnore]
        public ICollection<IngredientComposition> IngredientCompositions { get; set; } //(1 TO N_COMPOS)
        #endregion
    }
}
