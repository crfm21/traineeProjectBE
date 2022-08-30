using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Entities.RepositoryInterfaces;
using RecipeApp.Infrastructure.Context;

namespace RecipeApp.Infrastructure.EntityRepositories
{
    public class CommentRepository : ICommentRepository
    {
        #region Fields
        private readonly MainContext _context;

        private readonly DbSet<Comment> _comments;
        #endregion

        #region Constructors
        public CommentRepository(MainContext context)
        {
            _context = context;
            _comments = _context.Set<Comment>();
        }
        #endregion

        #region Methods
        public Task<int> Count()
        {
            return _comments
                .CountAsync();
        }

        public Task<int> CountWhere(Expression<Func<Comment, bool>> predicate)
        {
            return _comments
                .CountAsync(predicate);
        }

        public async Task Create(Comment comment)
        {
            if (comment == null) throw new ArgumentNullException("Create method repository - comment");
            var r = await _context.Recipes.FindAsync(comment.RecipeId);

            if (r == null) throw new Exception("The recipe id does not exists.");

            await _comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Comment comment)
        {
            if (comment == null) throw new ArgumentNullException("Delete method repository - comment");

            _comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<Comment>> GetAll()
        {
            return GetWhere(c => c.IsDeleted == false);
        }

        public async Task<Comment> GetById(int id)
        {
            return await _comments.FindAsync(id);
        }

        public Task<IEnumerable<Comment>> GetCommentByRecipe(int recipeId)
        {
            return GetWhere(c => c.RecipeId == recipeId && c.IsDeleted == false);
        }

        public Task<IEnumerable<Comment>> GetDeleted()
        {
            return GetWhere(c => c.IsDeleted == true);
        }

        public async Task<IEnumerable<Comment>> GetWhere(Expression<Func<Comment, bool>> predicate)
        {
            return await _comments.Where(predicate).ToListAsync();
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public async Task SoftDelete(Comment comment)
        {
            if (comment == null) throw new ArgumentNullException("SoftDelete method repository - comment");

            comment.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task Update(Comment comment)
        {
            if (comment == null) throw new ArgumentNullException("Update method repository - comment");

            _comments.Update(comment);
            await _context.SaveChangesAsync();
        }
        #endregion

    }
}
