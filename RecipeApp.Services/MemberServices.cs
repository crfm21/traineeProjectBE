using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Entities.RepositoryInterfaces;
using RecipeApp.Domain.HelperClasses;
using RecipeApp.Services.ServicesInterfaces;

namespace RecipeApp.Services
{
    public class MemberServices : IMemberServices
    {
        #region Fields
        private readonly IMemberRepository _memberRepository;
        #endregion

        #region Constructors
        public MemberServices(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public Task<int> CountAllActiveMembers()
        {
            return _memberRepository.Count();
        }
        public Task CreateMember(MemberUser member)
        {
            return _memberRepository.Create(member);
        }

        public Task DeleteMember(MemberUser member)
        {
            return _memberRepository.Delete(member);
        }

        public Task<IEnumerable<MemberUser>> GetAllActiveMembers()
        {
            return _memberRepository.GetAll();
        }

        public Task<IEnumerable<MemberUser>> GetBannedMembers()
        {
            return _memberRepository.GetTheBanned(); ;
        }

        public Task<MemberUser> GetMemberByEmail(string email)
        {
            return _memberRepository.GetUserByEmail(email);
        }

        public Task<IEnumerable<MemberUser>> GetMemberByGender(MemberUser.Genders gender)
        {
            return _memberRepository.GetUserByGender(gender);
        }

        public Task<MemberUser> GetMemberById(int id)
        {
            return _memberRepository.GetById(id);
        }

        public Task<MemberUser> GetMemberByNickName(string nickName)
        {
            return _memberRepository.GetUserByNickname(nickName);
        }

        public Task SoftDeleteMember(MemberUser member)
        {
            return _memberRepository.SoftDelete(member);
        }

        public Task UpdateMember(MemberUser member)
        {
            return _memberRepository.Update(member);
        }

        public Task<MemberUser> GetMemberByComment(int comm)
        {
            return _memberRepository.GetMemberByComm(comm);
        }

        public Task<IEnumerable<MemberUser>> GetAllAdmins()
        {
            return _memberRepository.GetAllAdmins();
        }

        public Task Ban(MemberUser member)
        {
            return _memberRepository.Ban(member);
        }

        #endregion
    }
}
