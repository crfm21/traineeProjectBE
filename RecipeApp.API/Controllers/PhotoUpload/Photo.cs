using System;
using Microsoft.AspNetCore.Http;

namespace RecipeApp.API.Controllers.PhotoUpload
{
    public class Photo
    {
        public IFormFile files { get; set; }//nome da key no postman
    }
}
