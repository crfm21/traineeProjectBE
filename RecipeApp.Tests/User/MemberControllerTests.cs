using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RecipeApp.API.Controllers;
using RecipeApp.Domain.Entities;
using RecipeApp.Services.ServicesInterfaces;
using Xunit;

namespace RecipeApp.UnitTests.User
{
    public class MemberControllerTests
    {
        private readonly Mock<IMemberServices> _mockService;
        private readonly MemberController _controller;

        public MemberControllerTests()
        {
            _mockService = new Mock<IMemberServices>();
            _controller = new MemberController(_mockService.Object);
        }

        [Fact]//nao faz sentido
        public async Task GetAllMembers_MembersExist_ReturnsExactNumberOfActiveMembers()
        {
            //arrange
            var listOfMembers = new List<MemberUser>() {
                new MemberUser(),
                new MemberUser(),
                new MemberUser(),
                new MemberUser()
            };

            _mockService.Setup(service => service.GetAllActiveMembers())
                .ReturnsAsync(listOfMembers);//Estamos a indicar o que ele retorna não o que lhe enviamos

            //Act
            var result = await _controller.GetAllTheMembers();

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var members = Assert.IsType<List<MemberUser>>(actionResult.Value);
            Assert.Equal(4, members.Count);
        }

        [Fact]//nao faz sentido
        public async Task GetAllMembers_MembersDoNotExist_ReturnsBaqRequest()
        {
            //arrange
            List<MemberUser> listOfMembers = null;

            _mockService.Setup(service => service.GetAllActiveMembers())
                .ReturnsAsync(listOfMembers);

            //Act
            var result = await _controller.GetAllTheMembers();

            //Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("There are no members yet.", actionResult.Value);
        }

        [Fact]
        public async Task GetAMember_IdExists_ReturnsMember()
        {
            //arrange
            MemberUser member1 = new MemberUser() { Id = 1, FirstName = "Luis", LastName = "Paz"};
            MemberUser member2 = new MemberUser() { Id = 2, FirstName = "Luisa", LastName = "Fonte"};

            _mockService.Setup(m => m.GetMemberById(1))
                .ReturnsAsync(member1);
            _mockService.Setup(m => m.GetMemberById(2))
                .ReturnsAsync(member2);

            //Act
            var result = await _controller.GetAMember(2);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var actualMember = Assert.IsType<MemberUser>(actionResult.Value);
            Assert.Equal(member2, actualMember);
        }

        [Fact]
        public async Task GetAMember_IdDoesNotExist_ReturnsBadRequest()
        {
            //arrange
            MemberUser member1 = new MemberUser() { Id = 1, FirstName = "Luis", LastName = "Paz" };
            MemberUser member2 = new MemberUser() { Id = 2, FirstName = "Luisa", LastName = "Fonte" };

            _mockService.Setup(m => m.GetMemberById(1))
                .ReturnsAsync(member1);
            _mockService.Setup(m => m.GetMemberById(2))
                .ReturnsAsync(member2);

            //Act
            var result = await _controller.GetAMember(3);

            //Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("The member with the id 3 does not exist.", actionResult.Value);
        }

        [Fact]//nao faz sentido
        public async Task GetAllAdmins_AdminsExist_ReturnsExactNumberOfAdmins()
        {
            //arrange
            var listOfMembers = new List<MemberUser>() {
                new MemberUser(){ Id = 1, Profile = Domain.HelperClasses.User.Profiles.Administrator},
                new MemberUser() { Id = 2, Profile = Domain.HelperClasses.User.Profiles.Member},
                new MemberUser() { Id = 3, Profile = Domain.HelperClasses.User.Profiles.Administrator},
                new MemberUser() { Id = 4, Profile = Domain.HelperClasses.User.Profiles.Member},
            };

            List<MemberUser> admins = new List<MemberUser>();

            foreach (var member in listOfMembers)
            {
                if(member.Profile == Domain.HelperClasses.User.Profiles.Administrator)
                admins.Add(member);
            }

            _mockService.Setup(service => service.GetAllAdmins())
                .ReturnsAsync(admins);//Estamos a indicar o que ele retorna não o que lhe enviamos

            //Act
            var result = await _controller.GetAllAdmins();

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var members = Assert.IsType<List<MemberUser>>(actionResult.Value);
            Assert.Equal(2, admins.Count);
        }

        [Fact]//nao faz sentido
        public async Task GetAllAdmins_AdminsDoNoExist_ReturnsBadRequest()
        {
            //arrange
            var listOfMembers = new List<MemberUser>() {
                new MemberUser(){ Id = 1, Profile = Domain.HelperClasses.User.Profiles.Member},
                new MemberUser() { Id = 2, Profile = Domain.HelperClasses.User.Profiles.Member},
                new MemberUser() { Id = 3, Profile = Domain.HelperClasses.User.Profiles.Member},
                new MemberUser() { Id = 4, Profile = Domain.HelperClasses.User.Profiles.Member},
            };

            List<MemberUser> admins = new List<MemberUser>();

            foreach (var member in listOfMembers)
            {
                if (member.Profile == Domain.HelperClasses.User.Profiles.Administrator)
                    admins.Add(member);
            }
            if (admins.Count == 0) admins = null;

            _mockService.Setup(service => service.GetAllAdmins())
                .ReturnsAsync(admins);//Estamos a indicar o que ele retorna não o que lhe enviamos

            //Act
            var result = await _controller.GetAllAdmins();

            //Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal("There are no admins yet.", actionResult.Value);
        }

        [Fact]
        public async Task CreatingNewMember_ValidMember_ReturnsBadRequest()
        {
            //arrange
            var newMember = new MemberUser
            {
                Id = 1,
                FirstName = "Sam",
                LastName = "Robin",
                NickName = "SamR",
                Email = "SweetPotatoe@sr.com",
                Profile = Domain.HelperClasses.User.Profiles.Member,
                Password = "Love-Me4ever",
                PasswordConfirmation = "Love-Me4ever"
            };

            //Act
            var result = await _controller.CreatingNewMember(newMember);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var jSon = Assert.IsType<JsonResult>(actionResult.Value);

            Assert.Equal("A new member was created with the email SweetPotatoe@sr.com!", jSon.Value);
        }

        [Fact]
        public async Task CreatingNewMember_InvalidMember_ReturnsBadRequest()
        {
            //arrange
            var newMember = new MemberUser
            {
                LastName = "Robin",
                NickName = "SamR",
                Email = "SweetPotatoe@sr.com",
                Profile = Domain.HelperClasses.User.Profiles.Member,
                Password = "Love-Me4ever",
                PasswordConfirmation = "Love-Me4ever"
            };

            _controller.ModelState.AddModelError("FirstName", "First name cannot be empty.");
            //Act
            var result = await _controller.CreatingNewMember(newMember);

            //Assert
            var noNameResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Some required fields were not completed.", noNameResult.Value);
            Assert.Equal(newMember.Password, newMember.PasswordConfirmation);

        }
        [Fact]
        public async Task CreatingNewMember_InvalidPasswords_ReturnsBadRequest()
        {
            //arrange
            var newMember = new MemberUser
            {
                FirstName = "Sam",
                LastName = "Robin",
                NickName = "SamR",
                Email = "SweetPotatoe@sr.com",
                Profile = Domain.HelperClasses.User.Profiles.Member,
                Password = "Love-Me4ever",
                PasswordConfirmation = "Love-Meever"
            };

            //Act
            var result = await _controller.CreatingNewMember(newMember);

            //Assert
            var passErrorResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("The passwords are not identical.", passErrorResult.Value);
        }

        [Fact]
        public async Task CreatingNewMember_InvalidMember_CreateMemberDoNotExecute()
        {
            //arrange
            var newMember = new MemberUser
            {
                LastName = "Robin",
                NickName = "SamR",
                Email = "SweetPotatoe@sr.com",
                Profile = Domain.HelperClasses.User.Profiles.Member,
                Password = "Love-Me4ever",
                PasswordConfirmation = "Love-Me4ever"
            };
            //nao interessa nada disto ele vai considerar apenas o que vem a seguir, mesmo que tenha o firstname. Nao está assim a avaliar nada !
            //se nao tiver a linha de baixo ele cham na mmesma o metodo

            _controller.ModelState.AddModelError("FirstName", "First name cannot be empty.");

            //Act
            var result = await _controller.CreatingNewMember(newMember);

            //Assert
            _mockService.Verify(m => m.CreateMember(It.IsAny<MemberUser>()), Times.Never);

        }
    }
}
