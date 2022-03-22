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
        private readonly IPostLinks _postLinks;
        private readonly UserManager<User> _userManager;

        public PostService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper,
            IPostLinks postLinks, UserManager<User> userManager)
        {
            _repository = repository;
            _logger = logger; ;
            _mapper = mapper;
            _postLinks = postLinks;
            _userManager = userManager;
        }
        public async Task<PostDto> CreatePostForUserAsync(Guid userId, PostForCreationDto postForCreation, bool trackChanges)
        {
            await CheckIfUserExists(userId, trackChanges);

            var postEntity = _mapper.Map<Post>(postForCreation);

            _repository.Post.CreatePostForUserAsync(userId, postEntity);
            await _repository.SaveAsync();

            var postToReturn = _mapper.Map<PostDto>(postEntity);

            return postToReturn;
        }

        public async Task DeletePostForUserAsync(Guid userId, Guid id, bool trackChanges)
        {
            await CheckIfUserExists(userId, trackChanges);

            var postForUser = await GetpostForUserAndCheckIfItExists(userId, id, trackChanges);

            _repository.Post.DeletePostAsync(postForUser);
            await _repository.SaveAsync();
        }

        public async Task<PostDto> GetPostAsync(Guid userId, Guid postId, bool trackChanges)
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

        public async Task<(PostForUpdateDto postToPatch, Post postEntity)> GetPostForPatchAsync(Guid userId, Guid id, bool compTrackChanges, bool empTrackChanges)
        {
            await CheckIfUserExists(userId, compTrackChanges);

            var postFromDb = await GetpostForUserAndCheckIfItExists(userId, id, empTrackChanges);

            var postToPach = _mapper.Map<PostForUpdateDto>(postFromDb);

            return (postToPach, postFromDb);
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)> GetPostsAsync
            (Guid userId, PostLinkParameters linkParameters, bool trackChanges)
        {
            if (!linkParameters.PostParameters.ValidAgeRange)
                throw new MaxAgeRangeBadRequestException();

            await CheckIfUserExists(userId, trackChanges);

            var postsWithMetaData = await _repository.Post
                .GetPostsAsync(userId, linkParameters.PostParameters, trackChanges);

            var postsDto = _mapper.Map<IEnumerable<PostDto>>(postsWithMetaData);

            var links = _postLinks.TryGenerateLinks(postsDto, linkParameters.PostParameters.Fields, userId, linkParameters.Context);

            return (linkResponse: links, metaData: postsWithMetaData.MetaData);
        }

        public async Task SaveChangesForPatchAsync(PostForUpdateDto postToPach, Post postEntity)
        {
            _mapper.Map(postToPach, postEntity);
            await _repository.SaveAsync();
        }

        public async Task UpdatePostForUserAsync(Guid userId, Guid id, PostForUpdateDto postForUpdate, bool compTrackChanges, bool empTrackChanges)
        {
            await CheckIfUserExists(userId, compTrackChanges);

            var postFromDb = await GetpostForUserAndCheckIfItExists(userId, id, empTrackChanges);

            _mapper.Map(postForUpdate, postFromDb);
            await _repository.SaveAsync();
        }

        private async Task CheckIfUserExists(Guid userId, bool trackChanges)
        {
            var user = await _userManager.FindByNameAsync(userId.ToString());
            if (user is null)
                throw new UserNotFoundException(userId);
        }

        private async Task<Post> GetpostForUserAndCheckIfItExists(Guid userId, Guid postId, bool trackChanges)
        {
            var postForUser = await _repository.Post.GetPostAsync(userId, postId, trackChanges);
            if (postForUser is null)
                throw new PostNotFoundException(postId);

            return postForUser;
        }
    }
}
