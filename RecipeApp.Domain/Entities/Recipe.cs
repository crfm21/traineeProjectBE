using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using RecipeApp.Domain.Core;

namespace RecipeApp.Domain.Entities
{
    [Table("Recipes")]

    public class Recipe : CoreEntity
        //xUnitTests: NULLABLE PROPERTIES resolves the default 0 assignment in a int or double variableto null.
        //is this useful or the moq tests are sufficient?
    {
        #region Properties
        [Required(ErrorMessage = "Title cannot be empty.")]
        [Column(TypeName = "NVARCHAR(50)")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Category cannot be empty.")]
        public Categories Category { get; set; }

        [Required(ErrorMessage = "Difficulty level cannot be empty.")]
        public ExperienceLevel Difficulty { get ; set; }

        public string Duration { get; set; }
        //{   get { return _duration; }//validação de dados
        //    set { _duration = string.Format(_duration, "HH:mm"); }
        //} 

        [Required(ErrorMessage = "Serving number cannot be empty.")]
        public int Servings { get; set; }

        [Required(ErrorMessage = "Description of recipe cannot be empty.")]
        public string Description { get; set; }

        [Column(TypeName = "bit")]
        [JsonIgnore]
        public bool isPublished { get; set; } //  if false = reviewing and not visible

        //[JsonIgnore]
        public DateTime? PublishDate { get; set; } //where status = published

        //[JsonIgnore]
        //public byte[] RecipePhotoArray { get; set; }
        
        //public string RecipePhotoB64 { get; set; }

        [JsonIgnore]
        public string RecipePhotoPath { get; set; }

        [Range(0, 5)]
        [Column(TypeName = "DECIMAL(3,2)")]//3 ALGARISMOS NO TOTAL em que 2 são decimais (méida)
        
        public decimal Rating { get; set; }
        [JsonIgnore]
        public int NumberRatings { get; set; }
        [JsonIgnore]
        public int TotalRating { get; set; }

        ////FK
        [Required]
        [ForeignKey(nameof(MemberUser))]
        public int CreatorMemberId { get; set; }

        #region Navigation properties 
        //[InverseProperty("CreatedRecipes")]
        [JsonIgnore]
        public MemberUser MemberUser { get; set; }//(N to 1-MEMBER)

        //[JsonIgnore]
        //public ICollection<MemberUser> FavoriteMembers { get; set; }//(N to N-MEMBERS)

        [Required] //Has to be associated within
        public ICollection<IngredientComposition> IngredientCompoList { get; set; }//(N to N-COMPOS)

        [JsonIgnore]
        public ICollection<Comment> CommentsList { get; set; } //(1 to N-COMMENTS)
        #endregion

        #endregion
    }

    public enum Categories
    {
        Amusebouche,
        Starter,
        Meat,
        Fish,
        Vegetarian,
        Vegan,
        Dessert,
        Pastry,
        Viennoiserie
    }

    public enum ExperienceLevel
    {
        Debutant,
        Intermediate,
        Experienced
    }
}
