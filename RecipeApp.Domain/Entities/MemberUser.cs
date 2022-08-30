using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using RecipeApp.Domain.HelperClasses;

namespace RecipeApp.Domain.Entities
{
    [Table("Members")]

    public class MemberUser : User
    {

        [Required(ErrorMessage = "Nickname cannot be empty.")]
        [Column(TypeName = "NVARCHAR(25)")]
        public string NickName { get; set; }

        [Column(TypeName = "bit")]
        [JsonIgnore]
        public bool IsBanned { get; set; }

        //[JsonIgnore]
        public Profiles Profile { get; set; }

        #region Navigation properties
        [JsonIgnore]
        public ICollection<Recipe> CreatedRecipes { get; set; } //(1 TO N_RECIPES)

        [JsonIgnore]
        //[InverseProperty("FavoriteMembers")] //specifies which of the Member properties is related to 
        public ICollection<FavoriteRecipe> FavoriteRecipes { get; set; }//(N TO N_RECIPES)

        //[JsonIgnore]
        //public ICollection<Comment> Comments { get; set; } //(1 TO N_COMMENTS)
        #endregion
    }
}
