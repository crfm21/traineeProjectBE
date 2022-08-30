using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Models;
using RecipeApp.Services.ServicesInterfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecipeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        #region Fields
        public readonly ICommentServices _commentServices;
        #endregion

        #region Constructor
        public CommentController(ICommentServices commentServices)
        {
            _commentServices = commentServices;
        }
        #endregion

        #region Methods
        // GET: api/values
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllComments()
        {
            var c = await _commentServices.GetAllComments();

            if (c == null) return BadRequest("No comments were created yet.");

            return Ok(c);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAComment(int id)
        {
            var c = await _commentServices.GetCommentById(id);
            if (c == null) return BadRequest("No comment with the requested id.");

            return Ok(c);
        }

        [HttpGet]
        [Route("Recipe/{recipeId}")]
        public async Task<IActionResult> GetCommentsByRecipe(int recipeId)
        {
            var c = await _commentServices.GetCommentsByRecipe(recipeId);

            if (c == null) return BadRequest($"No comment were posted about the requested recipe.");

            return Ok(c);
        }

        [HttpGet]
        [Route("Creator/{creatorId}/Recipe/{recipeId}")]
        public async Task<IActionResult> GetCommentsByRecipeAndCreator(int recipeId, int creatorId)
        {
            var c = await _commentServices.GetCommentsByRecipe(recipeId);

            if (c == null) return BadRequest($"No comment were posted about the requested recipe.");

            var creatorSComment = new List<Comment>();

            foreach (var comment in c)
            {
                if (comment.MemberId == creatorId) creatorSComment.Add(comment);
            }

            return Ok(creatorSComment);
        }

        [HttpGet]
        [Route("NotApproved")]
        public async Task<IActionResult> GetNotApprovedComments()
        {
            var c = await _commentServices.GetCommentsNotApproved();

            if (c == null) return BadRequest(Json("No comment were disapproved by the community."));

            return Ok(c);
        }

        [HttpPost]
        [Route("New")]
        public async Task<IActionResult> CreatingNewComment(Comment comment)
        {
            await _commentServices.CreateComment(comment);

            return Ok(Json("Your comment were published."));
        }

        [HttpPut]
        [Route("edit/{commentId}")]
        public async Task<IActionResult> EditingComment(int commentId, CommentRequestDTO comment)
        {
            var currentComment = await _commentServices.GetCommentById(commentId);

            if (currentComment == null) return BadRequest("The requested comment does not match any published comment.");

            currentComment.CommentText = comment.CommentText ?? currentComment.CommentText;

            currentComment.UpdateDate = comment.UpdateDate;

            await _commentServices.UpdateComment(currentComment);

            return Ok("Your comment was successfully updated.");
        }

        [HttpPut]
        [Route("disapprove/{commentId}")]
        public async Task<IActionResult> DisapprovingComment(int commentId)
        {
            var comment = await _commentServices.GetCommentById(commentId);
            if (comment == null) return BadRequest("Requested comment does not exists.");

            comment.NotApproved = true;

            await _commentServices.UpdateComment(comment);

            return Ok("An alert was notified to the administrator whom will assess the comment.");
        }

        [HttpDelete]
        [Route("sDelete/{commentId}")]
        public async Task<IActionResult> SoftDeleteComment(int commentId)
        {
            var c = await _commentServices.GetCommentById(commentId);
            if (c == null) return BadRequest("Requested comment does not exists.");

            await _commentServices.SoftDeleteComment(c);

            return Ok("The comment was deleted.");
        }
        // GET api/values/5

        #endregion
    }
}
