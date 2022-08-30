using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApp.Domain.Entities;

namespace RecipeApp.Services.ServicesInterfaces
{
    public interface IMemberServices
    {
        Task<int> CountAllActiveMembers();

        Task CreateMember(MemberUser member);
        Task DeleteMember(MemberUser member);
        Task SoftDeleteMember(MemberUser member);
        Task UpdateMember(MemberUser member);
        Task Ban(MemberUser member);

        Task<IEnumerable<MemberUser>> GetAllActiveMembers();
        Task<IEnumerable<MemberUser>> GetBannedMembers();
        Task<IEnumerable<MemberUser>> GetMemberByGender(MemberUser.Genders gender);
        Task<MemberUser> GetMemberById(int id);
        Task<MemberUser> GetMemberByEmail(string email);
        Task<MemberUser> GetMemberByNickName(string nickName);

        Task<MemberUser> GetMemberByComment(int comm);
        Task<IEnumerable<MemberUser>> GetAllAdmins();

    }
}
