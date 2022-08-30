using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RecipeApp.Domain.Core
{
    public interface ICoreRepository<T> where T : CoreEntity
    {
        //CRUD
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task SoftDelete(T entity);
        Task SaveChanges();

        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetDeleted();
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);

        Task<int> Count();
        Task<int> CountWhere(Expression<Func<T, bool>> predicate);
    }
}
