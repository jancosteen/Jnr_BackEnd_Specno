using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class PostService : IPostService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        //private readonly IPostLinks _postLinks;
        private readonly UserManager<User> _userManager;

        public PostService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper,
             UserManager<User> userManager)// IPostLinks postLinks,
        {
            _repository = repository;
            _logger = logger; ;
            _mapper = mapper;
            //_postLinks = postLinks;
            _userManager = userManager;
        }
        public async Task<PostDto> CreatePostForUserAsync(string userId, PostForCreationDto postForCreation, bool trackChanges)
        {
            await CheckIfUserExists(userId, trackChanges);

            var postEntity = _mapper.Map<Post>(postForCreation);

            var creationDate = DateTime.UtcNow;
            postEntity.CreationDate = creationDate;

            _repository.Post.CreatePostForUserAsync(userId, postEntity);
            await _repository.SaveAsync();

            var postToReturn = _mapper.Map<PostDto>(postEntity);

            return postToReturn;
        }

        public async Task DeletePostForUserAsync(string userId, Guid id, bool trackChanges)
        {
            await CheckIfUserExists(userId, trackChanges);

            var postForUser = await GetpostForUserAndCheckIfItExists(userId, id, trackChanges);

            _repository.Post.DeletePostAsync(postForUser);
            await _repository.SaveAsync();
        }

        public async Task<PostDto> GetPostAsync(string userId, Guid postId, bool trackChanges)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                throw new UserNotFoundException(userId);

            var postFromDb = await _repository.Post.GetPostAsync(userId, postId, trackChanges);
            if (postFromDb is null)
                throw new PostNotFoundException(postId);

            var postDto = _mapper.Map<PostDto>(postFromDb);

            return postDto;
        }

        public async Task<(PostForUpdateDto postToPatch, Post postEntity)> GetPostForPatchAsync(string userId, Guid id, bool compTrackChanges, bool empTrackChanges)
        {
            await CheckIfUserExists(userId, compTrackChanges);

            var postFromDb = await GetpostForUserAndCheckIfItExists(userId, id, empTrackChanges);

            var postToPach = _mapper.Map<PostForUpdateDto>(postFromDb);

            return (postToPach, postFromDb);
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsAsync(bool trackChanges)
        {
            var postEntities = await _repository.Post.GetAllPostsAsync(trackChanges);

            var postDto = _mapper.Map<IEnumerable<PostDto>>(postEntities);

            return postDto;
        }

        public async Task SaveChangesForPatchAsync(PostForUpdateDto postToPach, Post postEntity)
        {
            _mapper.Map(postToPach, postEntity);
            await _repository.SaveAsync();
        }

        public async Task UpdatePostForUserAsync(string userId, Guid id, PostForUpdateDto postForUpdate, bool compTrackChanges, bool empTrackChanges)
        {
            await CheckIfUserExists(userId, compTrackChanges);

            var postFromDb = await GetpostForUserAndCheckIfItExists(userId, id, empTrackChanges);

            postForUpdate.CreationDate = postFromDb.CreationDate;            

            _mapper.Map(postForUpdate, postFromDb);
            postFromDb.UpdateDate = DateTime.UtcNow;

            await _repository.SaveAsync();
        }

        private async Task CheckIfUserExists(string userId, bool trackChanges)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                throw new UserNotFoundException(userId);
        }

        private async Task<Post> GetpostForUserAndCheckIfItExists(string userId, Guid postId, bool trackChanges)
        {
            var postForUser = await _repository.Post.GetPostAsync(userId, postId, trackChanges);
            if (postForUser is null)
                throw new PostNotFoundException(postId);

            return postForUser;
        }
    }
}
