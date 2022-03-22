﻿using Entities.LinkModels;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Reddit_Api.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Reddit_Api.Presentation.Controllers
{
    [Route("api/posts")]// "api/users/{userId}/posts"
    [ApiController]
    public class PostController: ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly UserManager<User> _userManager;

        public PostController(IServiceManager service, UserManager<User> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpGet(Name ="GetAllPosts")]
        [HttpHead]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _service.PostService.GetAllPostsAsync(trackChanges: false);
            //var users = await _userManager.FindByIdAsync("db9ef0fe-eefa-4548-ad0d-0bb3205d7df8");

            return Ok(posts);
        }

        [HttpGet("{userId}/{id}", Name = "GetPostForUser")]
        public async Task<IActionResult> GetPostForUser(string userId, Guid id)
        {
            var post = await _service.PostService.GetPostAsync(userId, id, trackChanges: false);
            return Ok(post);
        }

        [HttpPost("{userId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreatePostForUser(string userId, [FromBody] PostForCreationDto post)
        {
            var postToReturn = await _service.PostService.CreatePostForUserAsync(userId, post, trackChanges: false);

            return CreatedAtRoute("GetPostForUser", new { userId, id = postToReturn.Id }, postToReturn);
        }

        [HttpDelete("{userId}/{id}")]
        public async Task<IActionResult> DeletePostForUser(string userId, Guid id)
        {
            await _service.PostService.DeletePostForUserAsync(userId, id, trackChanges: false);

            return NoContent();
        }

        [HttpPut("{userId}/{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdatePostForUser(string userId, Guid id, [FromBody] PostForUpdateDto postForUpdate)
        {

            await _service.PostService.UpdatePostForUserAsync(userId, id, postForUpdate, compTrackChanges: false, empTrackChanges: true);

            return NoContent();
        }

        [HttpPatch("{userId}/{id}")]
        public async Task<IActionResult> PartiallyUpdatePostForUser(string userId, Guid id, [FromBody] JsonPatchDocument<PostForUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("patchDoc is null");

            var result = await _service.PostService.GetPostForPatchAsync(userId, id, compTrackChanges: false, empTrackChanges: true);

            patchDoc.ApplyTo(result.postToPatch, ModelState);

            TryValidateModel(result.postToPatch);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _service.PostService.SaveChangesForPatchAsync(result.postToPatch, result.postEntity);

            return NoContent();
        }
    }
}
