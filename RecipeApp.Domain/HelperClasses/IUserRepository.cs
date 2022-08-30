using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApp.Domain.Core;

namespace RecipeApp.Domain.HelperClasses
{
    public interface IUserRepository<T> : ICoreRepository<T> where T : User
    {
        Task<IEnumerable<T>> GetUserByGender(User.Genders gender);
        Task<IEnumerable<T>> GetUserByAge(int age);
        Task<T> GetUserByEmail(string email);
    }
}
