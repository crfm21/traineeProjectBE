using System;
using static RecipeApp.Domain.HelperClasses.User;

namespace RecipeApp.Domain.Models
{
    public class MemberRequestDTO //ALTERAÇAO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }

        //public string UserPhotoB64 { get; set; }
        public string UserPhotoPath { get; set; }

        public DateTime? BirthDate { get; set; }//only the date ()

        public Genders? Gender { get; set; }

        public string Password { get; set; }

        public DateTime? UpdateDate { get; set; } = DateTime.Now;

        public bool? isDeleted { get; set; }
    }
}
