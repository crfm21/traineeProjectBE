using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApp.Domain.HelperClasses;

namespace RecipeApp.Domain.Entities.RepositoryInterfaces
{
    public interface IMemberRepository : IUserRepository<MemberUser>
    {
        Task Ban(MemberUser member);
        Task<MemberUser> GetUserByNickname(string nickName);
        Task<IEnumerable<MemberUser>> GetTheBanned();
        //Task AddFavorite(int fanId, int recipeId);
        Task<MemberUser> GetMemberByComm(int commId);
        Task<IEnumerable<MemberUser>> GetAllAdmins();

    }
}
