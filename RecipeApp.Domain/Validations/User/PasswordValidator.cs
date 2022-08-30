using System;
using System.Text.RegularExpressions;

namespace RecipeApp.Domain.Validations.User
{
    public class PasswordValidator
    {
        //private readonly string rgx = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-_.]).{8,}$";
        //private readonly string rgx = @"^(?=.*[A-Za-z])(?=.*[0-9])(?=.*[@$!%*#?&._-])[A-Za-z\d@$!%*#?&._-]{8,}$";
        private readonly string rgx = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$@!%&*?_])[A-Za-z\d#$@!%&*?_]{8,}$";

        public bool IsValid(string pass)
        {
            return Regex.IsMatch(pass, rgx);
        }
    }
}
