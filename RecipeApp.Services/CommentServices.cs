using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Entities.RepositoryInterfaces;
using RecipeApp.Services.ServicesInterfaces;

namespace RecipeApp.Services
{
    public class CommentServices : ICommentServices
    {
        #region Fields
        private readonly ICommentRepository _commentRepository;
        #endregion

        #region Constructors
        public CommentServices(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        #endregion

        #region Methods
        public Task<int> CountAllComments()
        {
            return _commentRepository.Count();
        }

        public Task<int> CountCommentsByRecipe(int recipeId)
        {
            return _commentRepository.CountWhere(c => c.RecipeId == recipeId);
        }

        public Task CreateComment(Comment comment)
        {
            return _commentRepository.Create(comment);
        }

        public Task DeleteComment(Comment comment)
        {
            return _commentRepository.Delete(comment);
        }

        public Task<IEnumerable<Comment>> GetAllComments()
        {
            return _commentRepository.GetAll();
        }

        public Task<Comment> GetCommentById(int id)
        {
            return _commentRepository.GetById(id);
        }

        public Task<IEnumerable<Comment>> GetCommentsByRecipe(int recipeId)
        {
            return _commentRepository.GetCommentByRecipe(recipeId);
        }

        public Task<IEnumerable<Comment>> GetCommentsDeleted()
        {
            return _commentRepository.GetDeleted();
        }

        public Task<IEnumerable<Comment>> GetCommentsNotApproved()
        {
            return _commentRepository.GetWhere(c => c.NotApproved == true);
        }

        public Task SoftDeleteComment(Comment comment)
        {
            return _commentRepository.SoftDelete(comment);
        }

        public Task UpdateComment(Comment comment)
        {
            return _commentRepository.Update(comment);
        }
        #endregion
    }
}
