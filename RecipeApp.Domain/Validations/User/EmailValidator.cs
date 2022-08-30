using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace RecipeApp.Domain.Validations.User
{
    public class EmailValidator
    {
        //https://stackoverflow.com/questions/5342375/regex-email-validation
        private readonly string rgx = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

        public bool IsValidRgx(string emailadress)
        {
            return Regex.IsMatch(emailadress, rgx, RegexOptions.IgnoreCase);
        }

        public bool IsValidMicrosoft(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
