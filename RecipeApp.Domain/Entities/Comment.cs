using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using RecipeApp.Domain.Core;

namespace RecipeApp.Domain.Entities
{
    [Table("Comments")]

    public class Comment : CoreEntity
    {
        [Required(ErrorMessage = "Comment cannot be empty.")]
        [Column(TypeName = "NVARCHAR(250)")]
        public string CommentText { get; set; }

        //FK
        [Required]
        public int MemberId { get; set; }

        [Required]
        [ForeignKey(nameof(Recipe))]
        public int RecipeId { get; set; }

        [JsonIgnore]
        public bool NotApproved { get; set; }// insultuous comment, for possible deletion

        #region Navigation properties
        //[JsonIgnore]
        //public MemberUser MemberUser { get; set; }

        [JsonIgnore]
        public Recipe Recipe { get; set; }
        #endregion
    }

}
