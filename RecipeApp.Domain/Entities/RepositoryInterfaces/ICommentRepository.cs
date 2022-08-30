using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApp.Domain.Core;

namespace RecipeApp.Domain.Entities.RepositoryInterfaces
{
    public interface ICommentRepository : ICoreRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetCommentByRecipe(int recipeId);
    }
}
