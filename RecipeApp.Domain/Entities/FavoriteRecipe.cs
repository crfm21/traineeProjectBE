using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using RecipeApp.Domain.Core;

namespace RecipeApp.Domain.Entities
{
    [Table("FavoriteRecipes")]

    public class FavoriteRecipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(MemberUser))]
        [Required]
        public int MemberId { get; set; }

        [Required]
        public int RecipeId { get; set; }

        #region Navigation properties
        [JsonIgnore]
        public MemberUser MemberUser { get; set; }
        //public Recipe Recipe { get; set; }
        #endregion
    }
}
