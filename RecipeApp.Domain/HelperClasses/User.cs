using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using RecipeApp.Domain.Core;

namespace RecipeApp.Domain.HelperClasses
{
    public class User : CoreEntity
    {
        #region Atributes
        private string _password;
        private string _passwordConfirmation;
        private string _salt;
        #endregion

        byte[] bSalt = SaltGen();

        #region Properties
        [Required(ErrorMessage = "First name cannot be empty.")]
        [Column(TypeName = "NVARCHAR(50)")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name cannot be empty.")]
        [Column(TypeName = "NVARCHAR(50)")]
        //[RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
        //    "Números e caracteres especiais não são permitidos no nome.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "E-mail cannot be empty.")]
        public string Email { get; set; }

        [JsonIgnore]
        public string Salt //necessário para conseguir fazer a rotina de login (retrieve do salt e do  para saber private
        {
            get { return _salt; }
            set { _salt = Salt; } //Sem SET, para que propriedade não tenha permissoes de escrita. Valor atribuido no método abaixo
        }

        //SALT and for HASHING password to prevent pass from being decrypted for use in other systems
        [Required(ErrorMessage = "Password cannot be empty.")]
        public string Password // hashed password stored with salt 
        {
            get { return _password; }
            set
            {
                
                _salt = Convert.ToBase64String(bSalt);

                _password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: value,
                salt: bSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            }
        }

        [Required(ErrorMessage = "Confirmation needeed.")]// teremos que colocar o salt tbm aqui ? para conseguir confirmaçao?
        [NotMapped]// only for ensuring that user has not mistyped it (put validation constraint with no possible copy and paste: user has to type it) 
        public string PasswordConfirmation {
            get { return _passwordConfirmation; }
            set
            {
                _passwordConfirmation = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: value,
                salt: bSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            }
        }

        //[JsonIgnore]
        //public byte[] UserPhotoArray { get; set; }

        //[JsonIgnore]
        //public string UserPhotoB64 { get; set; }

        //[JsonIgnore]
        //public IFormFile UserPhotoPath { get; set; }
        public string UserPhotoPath { get; set; }

        [Required(ErrorMessage = "Birth date cannot be empty.")]
        //[Column(TypeName = "smalldatetime")]
        public DateTime? BirthDate { get; set; }

        public Genders? Gender { get; set; }

        //[JsonIgnore]
        //public Profiles Profile { get; set; }
        #endregion

        #region Methods
        public static byte[] SaltGen()
        {
            var salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            return salt;
        }
        #endregion

        public enum Profiles
        {
            Administrator,
            Member
        }

        public enum Genders
        {
            masculine,
            feminine,
            neuter,
            unspecified
        }
    }

}
