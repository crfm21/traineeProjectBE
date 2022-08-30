using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Entities.RepositoryInterfaces;
using RecipeApp.Infrastructure.Context;
using RecipeApp.Infrastructure.EntityRepositories;
using RecipeApp.Services;
using Xunit;

namespace RecipeApp.UnitTests.User

    //if I want to test the logic in repositories like validate the models
{
    public class MemberRepoTests
    {
        private readonly Mock<IMemberRepository> _mockRepo;
        private readonly Mock<MainContext> _mockContext;
        private readonly MemberRepository _memberRepo;

        public MemberRepoTests()
        {
            _mockRepo = new Mock<IMemberRepository>();
            _mockContext = new Mock<MainContext>();
            _memberRepo = new MemberRepository(_mockContext.Object);
        }

        //[Fact]
        //public async Task GetAll_MembersDoNotExists_throwsError()
        //{
        //    List<MemberUser> listOfMembers = null;
        //    _mockContext.Setup(c => c.MemberUsers);

        //}

    }
}
