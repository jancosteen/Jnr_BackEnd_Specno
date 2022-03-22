using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Reddit_Api.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Api.Presentation.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController: ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly UserManager<User> _userManager;

        public CommentController(IServiceManager service, UserManager<User> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpGet(Name = "GetAllComments")]
        [HttpHead]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _service.CommentService.GetAllCommentsAsync(trackChanges: false);
            //var users = await _userManager.FindByIdAsync("db9ef0fe-eefa-4548-ad0d-0bb3205d7df8");

            return Ok(comments);
        }

        [HttpGet("userId/{userId}/postId/{postId}/commentId/{id}", Name = "GetComment")]
        public async Task<IActionResult> GetComment(string userId, Guid postId, Guid id)
        {
            var comment = await _service.CommentService.GetCommentAsync(userId,postId, id, trackChanges: false);
            return Ok(comment);
        }

        [HttpPost("userId/{userId}/postId/{postId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCommentForUser(string userId, Guid postId, [FromBody] CommentForCreationDto comment)
        {
            var commentToReturn = await _service.CommentService.CreateCommentAsync(userId, postId, comment, trackChanges: false);

            return CreatedAtRoute("GetComment", new { userId,postId, id = commentToReturn.Id }, commentToReturn);
        }

        [HttpDelete("userId/{userId}/postId/{postId}/commentId/{id}")]
        public async Task<IActionResult> DeleteCommentForUser(string userId,Guid postId, Guid id)
        {
            await _service.CommentService.DeleteCommentAsync(userId, postId, id, trackChanges: false);

            return NoContent();
        }

        [HttpPut("userId/{userId}/postId/{postId}/commentId/{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCommentForUser(string userId, Guid postId, Guid id, [FromBody] CommentForUpdateDto commentForUpdate)
        {

            await _service.CommentService.UpdateCommentAsync(userId, postId, id, commentForUpdate, compTrackChanges: false, empTrackChanges: true);

            return NoContent();
        }
    }
}
