using System;
namespace RecipeApp.Domain.Models
{
    public class CommentRequestDTO
    {
        public string CommentText { get; set; }
        public DateTime? UpdateDate { get; set; } = DateTime.Now;
    }
}
