﻿using Entities.Models;
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
    //[Authorize]
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

        [HttpPut("upvoteComment/{userId}/{postId}/commentId/{id}")]
        public async Task<IActionResult> UpvoteComment(string userId, Guid postId, Guid id)
        {
            var commentDto = await _service.CommentService.GetCommentAsync(userId, postId, id, trackChanges: false);

            await _service.CommentService.UpvoteComment(userId, postId, id, commentDto, userTrackChanges: false, postTrackChanges: false, commentTrackChanges: true);

            return NoContent();
        }

        [HttpPut("downvoteComment/{userId}/{postId}/commentId/{id}")]
        public async Task<IActionResult> DownvoteComment(string userId, Guid postId, Guid id)
        {
            var commentDto = await _service.CommentService.GetCommentAsync(userId, postId, id, trackChanges: false);

            await _service.CommentService.DownvoteComment(userId, postId, id, commentDto, userTrackChanges: false, postTrackChanges: false, commentTrackChanges: true);

            return NoContent();
        }
    }
}