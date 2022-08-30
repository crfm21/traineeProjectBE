using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Domain.HelperClasses;
using RecipeApp.Infrastructure.Context;

namespace RecipeApp.Infrastructure.HelperRepository
{
    public class UserRepository<T> : IUserRepository<T> where T : User
    {
        #region Fields
        private readonly MainContext _context;

        private readonly DbSet<T> _users;
        #endregion

        #region Constructors
        public UserRepository(MainContext context)
        {
            _context = context;
            _users = _context.Set<T>();
        }
        #endregion

        #region Methods
        public Task<int> Count()
        {
            return _users
                .CountAsync();
        }

        public Task<int> CountWhere(Expression<Func<T, bool>> predicate)
        {
            return _users
                .CountAsync(predicate);
        }

        public async Task Create(T user)
        {
            if (user == null) throw new ArgumentNullException("Create method repository - user");

            await _users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T user)
        {
            if (user == null) throw new ArgumentNullException("Delete method repository - user");

            _users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<T>> GetAll()
        {
            return GetWhere(u => u.IsDeleted == false);
        }

        public async Task<T> GetById(int id)
        {
            return await _users.FindAsync(id);
        }

        public Task<IEnumerable<T>> GetDeleted()
        {
            return GetWhere(u => u.IsDeleted == true);
        }

        public Task<IEnumerable<T>> GetUserByAge(int age)
        {
            var birthYear = DateTime.Now.Year - age;
            return GetWhere(u => u.BirthDate.Value.Year == birthYear);
        }

        public async Task<T> GetUserByEmail(string email)
        {
            return await _users.Where(u => u.Email == email).SingleOrDefaultAsync();
        }

        public Task<IEnumerable<T>> GetUserByGender(User.Genders gender)
        {
            return GetWhere(u => u.Gender == gender);
        }

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return await _users.Where(predicate).ToListAsync();
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public async Task SoftDelete(T user)
        {
            if (user == null) throw new ArgumentNullException("SoftDelete method repository - user");

            user.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task Update(T user)
        {
            if (user == null) throw new ArgumentNullException("Update method repository - user");

            _users.Update(user);
            await _context.SaveChangesAsync();
        }
        #endregion

    }
}
