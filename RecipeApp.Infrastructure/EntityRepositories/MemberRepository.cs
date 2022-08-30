using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Entities.RepositoryInterfaces;
using RecipeApp.Domain.HelperClasses;
using RecipeApp.Infrastructure.Context;

namespace RecipeApp.Infrastructure.EntityRepositories
{
    public class MemberRepository : IMemberRepository
    {
        #region Fields
        private readonly MainContext _context;

        private readonly DbSet<MemberUser> _memberUsers;
        #endregion

        #region Constructors
        public MemberRepository(MainContext context)
        {
            _context = context;
            _memberUsers = _context.Set<MemberUser>();
        }
        #endregion

        #region Methods
        public Task<int> Count()
        {
            return _memberUsers.CountAsync();
        }

        public Task<int> CountWhere(Expression<Func<MemberUser, bool>> predicate)
        {
            return _memberUsers.CountAsync(predicate);
        }

        public async Task Create(MemberUser member)
        {
            if (member == null) throw new ArgumentNullException("Create method repository - member");

            await _memberUsers.AddAsync(member);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(MemberUser member)
        {
            if (member == null) throw new ArgumentNullException("Delete method repository - member");

            _memberUsers.Remove(member);
            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<MemberUser>> GetAll()
        {
            return GetWhere(m => m.IsDeleted == false && m.IsBanned == false && m.Profile != 0);
        }

        public Task<IEnumerable<MemberUser>> GetAllAdmins()
        {
            return GetWhere(m => m.IsDeleted == false && m.IsBanned == false && m.Profile == User.Profiles.Administrator);
        }

        public Task<IEnumerable<MemberUser>> GetTheBanned()
        {
            return GetWhere(m => m.IsBanned == true);
        }

        public async Task<MemberUser> GetById(int id)
        {
            return await _memberUsers.FindAsync(id);
        }

        public Task<IEnumerable<MemberUser>> GetDeleted()
        {
            return GetWhere(m => m.IsDeleted == true);
        }

        public Task<IEnumerable<MemberUser>> GetUserByAge(int age)
        {
            var birthYear = DateTime.Now.Year - age;
            return GetWhere(m => m.BirthDate.Value.Year == birthYear);
        }

        public async Task<MemberUser> GetUserByEmail(string email)
        {
            return await _memberUsers.Where(u => u.Email == email).SingleOrDefaultAsync();
        }

        public Task<IEnumerable<MemberUser>> GetUserByGender(User.Genders gender)
        {
            return GetWhere(m => m.Gender == gender);
        }

        public async Task<MemberUser> GetUserByNickname(string nickName)
        {
            return await _memberUsers.Where(m => m.NickName == nickName).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberUser>> GetWhere(Expression<Func<MemberUser, bool>> predicate)
        {
            return await _memberUsers.Where(predicate).ToListAsync();
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public async Task SoftDelete(MemberUser member)//ALTERAÇAO
        {
            if (member == null) throw new ArgumentNullException("SoftDelete method repository - member");

            var recipes = _context.Recipes.ToList();//CreatedRecipe is always null!
            if (recipes != null)
            {
                foreach (var recipe in recipes)
                {
                    if (recipe.CreatorMemberId == member.Id) recipe.IsDeleted = true;
                }
            }

            member.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task Update(MemberUser member)
        {
            if (member == null) throw new ArgumentNullException("Update method repository - member");

            _memberUsers.Update(member);
            await _context.SaveChangesAsync();
        }

        public Task<MemberUser> GetMemberByComm(int commId)//alter
        {
            var comment = _context.Comments.FirstOrDefault(c => c.Id == commId);
            var m = GetById(comment.MemberId);
            return m;
        }

        public async Task Ban(MemberUser member)
        {
            if (member == null) throw new ArgumentNullException("Ban method repository - member");
            var recipes = await _context.Recipes.Where(r => r.CreatorMemberId == member.Id).ToListAsync();

            foreach (var item in recipes)
            {
                item.IsDeleted = true;
            }

            member.IsBanned = true;
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
