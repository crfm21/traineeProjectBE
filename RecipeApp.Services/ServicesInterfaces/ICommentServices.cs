using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApp.Domain.Entities;

namespace RecipeApp.Services.ServicesInterfaces
{
    public interface ICommentServices
    {
        Task<int> CountAllComments();
        Task<int> CountCommentsByRecipe(int recipeId);

        Task CreateComment(Comment comment);
        Task DeleteComment(Comment comment);
        Task SoftDeleteComment(Comment comment);
        Task UpdateComment(Comment comment);

        Task<IEnumerable<Comment>> GetAllComments();
        Task<Comment> GetCommentById(int id);
        Task<IEnumerable<Comment>> GetCommentsByRecipe(int recipeId);
        Task<IEnumerable<Comment>> GetCommentsNotApproved();
        Task<IEnumerable<Comment>> GetCommentsDeleted();

    }
}
