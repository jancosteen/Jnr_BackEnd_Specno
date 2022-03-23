using Entities.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class CommentController: ControllerBase
    {
        private readonly IServiceManager _service;
       

        public CommentController(IServiceManager service)
        {
            _service = service;
           
        }

        [HttpGet(Name = "GetAllComments")]
        [HttpHead]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _service.CommentService.GetAllCommentsAsync(trackChanges: false);

            return Ok(comments);
        }

        [HttpGet("{id}", Name = "GetComment")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetComment( Guid id)
        {
            var comment = await _service.CommentService.GetCommentAsync(id, trackChanges: false);
            return Ok(comment);
        }

        [HttpPost("userId/{userId}/postId/{postId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateComment(string userId, Guid postId, [FromBody] CommentForCreationDto comment)
        {
            var commentToReturn = await _service.CommentService.CreateCommentAsync(userId, postId, comment, trackChanges: false);

            return CreatedAtRoute("GetComment", new { userId,postId, id = commentToReturn.Id }, commentToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            await _service.CommentService.DeleteCommentAsync(id, trackChanges: false);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateComment(Guid id, [FromBody] CommentForUpdateDto commentForUpdate)
        {

            await _service.CommentService.UpdateCommentAsync(id, commentForUpdate, compTrackChanges: false, empTrackChanges: true);

            return NoContent();
        }

        [HttpPut("upvoteComment/userId/{userId}/commentId/{id}")]
        public async Task<IActionResult> UpvoteComment(string userId, Guid id)
        {
            var commentDto = await _service.CommentService.GetCommentAsync(id, trackChanges: false);

            await _service.CommentService.UpvoteComment(userId,  id, commentDto, userTrackChanges: false, postTrackChanges: false, commentTrackChanges: true);

            return NoContent();
        }

        [HttpPut("downvoteComment/userId/{userId}/commentId/{id}")]
        public async Task<IActionResult> DownvoteComment(string userId, Guid id)
        {
            var commentDto = await _service.CommentService.GetCommentAsync(id, trackChanges: false);

            await _service.CommentService.DownvoteComment(userId, id, commentDto, userTrackChanges: false, postTrackChanges: false, commentTrackChanges: true);

            return NoContent();
        }
    }
}
