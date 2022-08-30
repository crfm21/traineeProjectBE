using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeApp.Infrastructure.Context;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecipeApp.API.Controllers.PhotoUpload
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : Controller
    {
        public static IWebHostEnvironment _env;
        private readonly MainContext _context;

        public PhotoController(IWebHostEnvironment env, MainContext context)
        {
            _env = env;
            _context = context;
        }
        // POST api/values
        //[HttpPost]//REVER: nao corre
        //[Route("upload/userPhoto")]
        //public async Task<IActionResult> UploadUserPhoto([FromForm] Photo objFile, int id, [FromQuery] string recipePhoto, [FromQuery] string userPhoto)//REVER
        //{
        //    string path = string.Empty;

        //    if (recipePhoto != null && userPhoto != null)
        //        throw new BadHttpRequestException("Only one context supported (recipe/memeber)!");

        //    //

        //    if (recipePhoto.ToUpper() == "S")
        //    {
        //        var recipe = await _context.Recipes.FindAsync(id);

        //        if (recipe == null) return BadRequest("No recipe found.");

        //        path = _env.ContentRootPath + $"/Photos/Member_{id}/UserPhoto/";
        //    }


        //    if (userPhoto.ToUpper() == "S")
        //    {
        //        var member = await _context.MemberUsers.FindAsync(id);

        //        if (member == null) return BadRequest("No member found.");

        //        path = _env.ContentRootPath + $"/Photos/Member_{id}/UserPhoto/";
        //    }

        //    try
        //    {
        //        if (objFile.files.Length > 0)
        //        {

        //            if (!Directory.Exists(path))
        //            {
        //                Directory.CreateDirectory(path);
        //            }

        //            using (FileStream fileStream = System.IO.File.Create(path + objFile.files.FileName))
        //            {
        //                await objFile.files.CopyToAsync(fileStream);
        //                fileStream.Flush();

        //                return Ok("Photo was successfully uploaded.");
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest("");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}

        //[HttpGet]
        //[Route("download/RecipePhotos/Paths")]
        //public async Task<IActionResult> GetRecipePhotoPaths(int recipeId)
        //{
        //    var recipe = await _context.Recipes.FindAsync(recipeId);
        //    if (recipe == null) return BadRequest("No recipe found.");

        //    var photoPaths = Directory.GetFiles(recipe.RecipePhotoPath).ToList();
        //    var fileNames = new List<string>();

        //    photoPaths.ForEach(p => fileNames.Add(Path.GetFileName(p)));

        //    //Path.GetFileName(photoPaths);
        //    //return Ok(recipe.RecipePhotoPaths);
        //    return Ok(fileNames);
        //    //var image = System.IO.File.OpenRead(photo);
        //    //return File(image, "image/jpeg");
        //}

        [HttpGet]
        [Route("download/UserPhotos/{memberId}")]
        public async Task<IActionResult> GetUserPhoto(int memberId)
        {
            var member = await _context.MemberUsers.FindAsync(memberId);
            if (member == null) return BadRequest("No member found.");

            if (member.UserPhotoPath == null) return BadRequest("No photo found.");

            var photoPaths = Directory.GetFiles(member.UserPhotoPath).ToList();

            FileStream image = System.IO.File.OpenRead(photoPaths[0]);

            return File(image, "image/webp");
        }

        [HttpGet]//REVER: indica erro System.IO.IOException: Not a directory
        [Route("download/RecipePhotos/{recipeId}")]
        public async Task<IActionResult> GetRecipePhoto(int recipeId)
        {
            var recipe = await _context.Recipes.FindAsync(recipeId);
            if (recipe == null) return BadRequest("No recipe found.");

            if (recipe.RecipePhotoPath == null) return BadRequest("No photo found.");

            var photoPaths = Directory.GetFiles(recipe.RecipePhotoPath).ToList();

            FileStream image = System.IO.File.OpenRead(photoPaths[0]);

            return File(image, "image/webp");
        }

        [HttpPost]
        [Route("upload/recipePhoto/{recipeId}")]
        public async Task<string> UploadRecipePhoto([FromForm] Photo objFile, int recipeId)//REVER
        {
            var recipe = await _context.Recipes.FindAsync(recipeId);

            if (recipe == null) return BadRequest("No recipe found.").ToString();

            try
            {
                if (objFile.files.Length > 0)
                {
                    string path = _env.ContentRootPath + $"/RecipePhotos/Recipe_{recipeId}/";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + objFile.files.FileName))
                    {
                        await objFile.files.CopyToAsync(fileStream);
                        fileStream.Flush();

                        recipe.RecipePhotoPath = path/* + objFile.files.FileName*/;
                        await _context.SaveChangesAsync();

                        return "Photo was successfully uploaded.";
                    }
                }
                else
                {
                    return "Photo upload failed";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        [HttpPost]
        [Route("upload/userPhoto/{memberId}")]
        public async Task<IActionResult> UploadUserPhoto([FromForm] Photo objFile, int memberId)
        {
            var member = await _context.MemberUsers.FindAsync(memberId);

            if (member == null) return BadRequest("No members found.");

            try
            {
                if (objFile.files.Length > 0)
                {
                    string path = _env.ContentRootPath + $"/UserPhotos/Member_{memberId}/";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + objFile.files.FileName))
                    {
                        await objFile.files.CopyToAsync(fileStream);
                        fileStream.Flush();

                        member.UserPhotoPath = path/* + objFile.files.FileName*/;
                        await _context.SaveChangesAsync();

                        return Ok(Json("Photo was successfully uploaded."));
                    }
                }
                else
                {
                    return Ok(Json("Photo upload failed"));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        [Route("Deleting/UserPhoto")]
        public async Task<IActionResult> DeleteUserPhoto(int memberId)
        {
            var member = await _context.MemberUsers.FindAsync(memberId);

            if (member == null) return BadRequest("No member found.");

            System.IO.DirectoryInfo di = new DirectoryInfo(member.UserPhotoPath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            //foreach (DirectoryInfo dir in di.GetDirectories())
            //{
            //    dir.Delete(true);
            //}

            member.UserPhotoPath = null;
            await _context.SaveChangesAsync();

            return Ok(Json("The image was deleted."));
                //deleting from the directory is crutial to have any inconsistency record in the DB and Directory
                //and try catch 
        }

        [HttpDelete]
        [Route("Deleting/RecipePhoto")]
        public async Task<IActionResult> DeleteRecipePhoto(int recipeId)
        {
            var recipe = await _context.Recipes.FindAsync(recipeId);

            if (recipe == null) return BadRequest("No member found.");

            System.IO.DirectoryInfo di = new DirectoryInfo(recipe.RecipePhotoPath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            //foreach (DirectoryInfo dir in di.GetDirectories())
            //{
            //    dir.Delete(true);
            //}

            recipe.RecipePhotoPath = null;
            await _context.SaveChangesAsync();

            return Ok(Json("The image was deleted."));
            //deleting from the directory is crutial to have any inconsistency record in the DB and Directory
            //and try catch


        }
    }
}