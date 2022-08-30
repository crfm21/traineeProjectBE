using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Models;
using RecipeApp.Services.ServicesInterfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecipeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : Controller
    {
        #region Fields
        public readonly IMemberServices _memberServices;
        #endregion

        #region Constructor
        public MemberController(IMemberServices memberServices)
        {
            _memberServices = memberServices;
        }
        #endregion

        #region Methods
        // GET: api/values
        [HttpGet]
        [Route("all/members")]
        public async Task<IActionResult> GetAllTheMembers()
        {
            var m = await _memberServices.GetAllActiveMembers();

            if (m == null) return BadRequest("There are no members yet.");

            return Ok(m);
        }

        [HttpGet]
        [Route("count/all/members")]
        public async Task<IActionResult> CountAllTheMembers()
        {
            var m = await _memberServices.GetAllActiveMembers();

            if (m == null) return BadRequest(Json("There are no members yet."));

            return Ok(m.Count());
        }

        [HttpGet]
        [Route("all/admins")]
        public async Task<IActionResult> GetAllAdmins()
        {
            var m = await _memberServices.GetAllAdmins();

            if (m == null) return BadRequest("There are no admins yet.");

            return Ok(m);
        }

        [HttpGet]//alteração
        [Route("byId/{id}")]
        public async Task<IActionResult> GetAMember(int id)
        {
            var m = await _memberServices.GetMemberById(id);

            if (m == null) return BadRequest($"The member with the id {id} does not exist.");
            return Ok(m);
        }

        [HttpGet]//alteração
        [Route("login/{email}/{inputPassword}")]
        public async Task<IActionResult> Login(string email, string inputPassword)
        {
            var m = await _memberServices.GetMemberByEmail(email);

            if (m == null){
                return BadRequest($"You are not registered. Please sign up.");
            }
            else
            {
                if (m.IsBanned == true) return BadRequest("You were banned. You can't log in.");

                var inputPassHashed = PasswordHashing(inputPassword, m.Salt);
                bool loginSucceeded = m.Password == inputPassHashed ;

                if (loginSucceeded) return Ok(m);
                else
                    return BadRequest($"The password is incorrect. Try again!");
            };

        }

        [HttpGet]
        [Route("Age/byId/{id}")]//alteraçao
        public async Task<IActionResult> GetMemberAge(int id)
        {
            var m = await _memberServices.GetMemberById(id);

            if (m == null) return BadRequest(Json("The requested id was not found."));

            int age, birthday = m.BirthDate.Value.Day, birthMonth = m.BirthDate.Value.Month, birthYear = m.BirthDate.Value.Year;

            if (DateTime.Now.Month <= birthMonth)
                if (DateTime.Now.Month == birthMonth && DateTime.Now.Day > birthday)
                    age = (DateTime.Now.Year - birthYear);
                else
                    age = (DateTime.Now.Year - birthYear) - 1;
            else
                age = (DateTime.Now.Year - birthYear);

            return Ok(age);
        }

        [HttpGet]
        [Route("comment/{commId}")]//alteraçao
        public async Task<IActionResult> GetMemberByComm(int commId)
        {
            var m = await _memberServices.GetMemberByComment(commId);

            return Ok(m);
        }

        [HttpGet]
        [Route("banned")]
        public async Task<IActionResult> GetBannedMembers()
        {
            var bM = await _memberServices.GetBannedMembers();

            if (bM == null) return BadRequest(Json("No banned members encountered."));

            return Ok(bM);
        }

        [HttpGet]
        [Route("count/banned")]
        public async Task<IActionResult> CountBannedMembers()
        {
            var bM = await _memberServices.GetBannedMembers();

            if (bM == null) return BadRequest(Json("No banned members encountered."));

            return Ok(bM.Count());
        }

        // POST api/values
        [HttpPost]//rever com o model.state
        [Route("New")]
        public async Task<IActionResult> CreatingNewMember([Bind("FirstName, LastName, Email, Password, NickName")] MemberUser member)
        {
            if (!ModelState.IsValid)
                return BadRequest("Some required fields were not completed.");
            
            if (member.Password != member.PasswordConfirmation)
                return BadRequest("The passwords are not identical.");

            await _memberServices.CreateMember(member);
            return Ok(Json($"A new member was created with the email {member.Email}!"));
        }

        //[HttpPost]//REVER
        //[Route("NewWithPhoto")]
        //public async Task<IActionResult> CreatingNewMemberPhoto(MemberUser member, [FromForm] IFormFile objFile)
        //{
        //    if (member.Password != member.PasswordConfirmation) return BadRequest("The passwords are not identical.");

        //    await _memberServices.CreateMember(member);
        //    return Ok($"A new member was created with the email {member.Email}!");
        //}

        // PUT api/values/5
        [HttpPut]//ALTERAÇAO
        [Route("update/byId/{memberId}")]//ALTER
        public async Task<IActionResult> UpdateMemberData(int memberId, MemberRequestDTO member)// the entity DTO is to bypass the required fields configured in the entity
        {
            var currentMember = await _memberServices.GetMemberById(memberId);

            if (currentMember == null) return BadRequest(Json("The requested id does not match any member."));
            //if user does not change all the data entries, the following code is to avoid nulls in the fields
            currentMember.BirthDate = (member.BirthDate == null) ? currentMember.BirthDate : member.BirthDate;
            currentMember.LastName = member.LastName ?? currentMember.LastName;
            currentMember.FirstName = member.FirstName ?? currentMember.FirstName;
            currentMember.NickName = member.NickName ?? currentMember.NickName;
            currentMember.Gender = member.Gender ?? currentMember.Gender;
            currentMember.Email = member.Email ?? currentMember.Email;
            currentMember.IsDeleted = member.isDeleted ?? currentMember.IsDeleted;

            currentMember.UserPhotoPath = member.UserPhotoPath ?? currentMember.UserPhotoPath;

            currentMember.UpdateDate = member.UpdateDate;

            if (!string.IsNullOrEmpty(member.Password))
                currentMember.Password = member.Password;

            await _memberServices.UpdateMember(currentMember);

            return Ok(Json("Your data were successfully updated."));
        }

        [HttpPut]
        [Route("banning/byId/{memberId}")]//ALTER
        public async Task<IActionResult> BanningMember(int memberId)
        {
            var m = await _memberServices.GetMemberById(memberId);

            if (m == null) return BadRequest("The member does not exists");

            await _memberServices.Ban(m);

            return Ok(Json($"{m.NickName} was banned from site."));
        }

        //// DELETE api/values/5
        [HttpDelete]//ALTER
        [Route("deleteAccount/byId/{memberId}")]
        public async Task<IActionResult> DeleteMember(int memberId)
        {
            var member = await _memberServices.GetMemberById(memberId);

            if (member == null) return BadRequest("The member does not exists.");

            await _memberServices.SoftDeleteMember(member);

            return Ok(Json($"{member.FirstName}, your account was deleted."));
        }

        [HttpDelete]//alteraçao
        [Route("deleteData/byId/{memberId}")]//ALTER
        public async Task<IActionResult> DeleteDataMember(int memberId)// as receitas associadas tbm sao apagadas? SIM!!TESTE FEITO
        {
            var member = await _memberServices.GetMemberById(memberId);

            if (member == null) return BadRequest("The member does not exists.");

            await _memberServices.DeleteMember(member);

            return Ok(Json($"{member.FirstName}, all your data were deleted."));
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == storedHash;
        }


        public static byte[] SaltGen() //salt generating 
        {
            var salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            return salt;
        }

        public static string PasswordHashing(string password, string salt)
        {
            //retrieved salt and encode it:
            //var bSalt = Encoding.ASCII.GetBytes(salt);
            byte[] bSalt = Convert.FromBase64String(salt);

            var hashedPass = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: bSalt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

            return hashedPass;
        }

        #endregion
    }
}
