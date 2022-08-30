using System;
using System.Collections.Generic;
using RecipeApp.Domain.Entities;

namespace RecipeApp.Domain.Models
{
    public class RecipeRequestDTO
    {
        public string Title { get; set; }

        public Categories? Category { get; set; }

        public ExperienceLevel? Difficulty { get; set; }

        public string Duration { get; set; }

        public int Servings { get; set; }

        public ICollection<IngredientComposition> IngredientCompoList { get; set; }//(N to N-COMPOS)//on update we have to update only some fields

        public string Description { get; set; }

        //public string RecipePhotoB64 { get; set; }

        public DateTime? UpdateDate { get; set; } = DateTime.Now;

    }
}
