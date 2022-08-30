using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Domain.Core;
using RecipeApp.Infrastructure.Context;

namespace RecipeApp.Infrastructure.CoreRepository
{
    public class CoreRepository<T> : ICoreRepository<T> where T : CoreEntity
    {
        #region Fields
        private readonly MainContext _context;

        private readonly DbSet<T> _entities;
        #endregion

        #region Constructors
        public CoreRepository(MainContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }
        #endregion

        #region Methods
        public Task<int> Count()
        {
            return _entities
                .CountAsync();
        }

        public Task<int> CountWhere(Expression<Func<T, bool>> predicate)
        {
            return _entities
                .CountAsync(predicate);
        }

        public async Task Create(T entity)
        {
            if (entity == null) throw new ArgumentNullException("Create method repository - entity");

            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("Delete method repository - entity");

            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<T>> GetAll()
        {
            return GetWhere(e => e.IsDeleted == false);
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public Task<IEnumerable<T>> GetDeleted()
        {
            return GetWhere(e => e.IsDeleted == true);
        }

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public async Task SoftDelete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("SoftDelete method repository - entity");

            entity.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("Update method repository - entity");

            _entities.Update(entity);
            await _context.SaveChangesAsync();
        }
        #endregion

    }
}
