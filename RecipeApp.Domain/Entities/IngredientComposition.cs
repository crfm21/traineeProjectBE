using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using RecipeApp.Domain.Core;

namespace RecipeApp.Domain.Entities
{
    [Table("IngredientCompositions")]//Estes dados só existem se a receita for criada

    public class IngredientComposition : CoreEntity
    {
        //[Required(ErrorMessage = "Ingredient name cannot be empty.")]
        public string Name { get; set; } // = NAME OF THE INGREDIENT OBJECT WITH ID

        public double Quantity { get; set; }
        public MesurementUnits Unit { get; set; }

        ////FK
        [Required]
        [JsonIgnore]
        public int RecipeId { get; set; }

        public int IngredientId { get; set; }//can be from the list or not

        #region Navigation properties
        [JsonIgnore]
        public Recipe Recipe { get; set; }//(N TO 1)

        [JsonIgnore]
        public Ingredient Ingredient { get; set; }//(N TO 1)
        #endregion
    }

    public enum MesurementUnits
    {
        unit,
        mg,
        g,
        kg,
        mL,
        cL,
        dL,
        L,
        pinch,
        qb
    }
}
