using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class PostService : IPostService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public PostService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper,
             UserManager<User> userManager)
        {
            _repository = repository;
            _logger = logger; ;
            _mapper = mapper;
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

        public async Task DeletePostForUserAsync(Guid id, bool trackChanges)
        {

            var postForUser = await GetpostForUserAndCheckIfItExists(id, trackChanges);

            _repository.Post.DeletePostAsync(postForUser);
            await _repository.SaveAsync();
        }

        public async Task<PostDto> GetPostAsync(Guid postId, bool trackChanges)
        {


            var postFromDb = await _repository.Post.GetPostAsync(postId, trackChanges);
            if (postFromDb is null)
                throw new PostNotFoundException(postId);

            var postDto = _mapper.Map<PostDto>(postFromDb);

            return postDto;
        }


        public async Task<IEnumerable<PostDto>> GetAllPostsAsync(bool trackChanges)
        {
            var postEntities = await _repository.Post.GetAllPostsAsync(trackChanges);

            var postDto = _mapper.Map<IEnumerable<PostDto>>(postEntities);

            return postDto;
        }


        public async Task UpdatePostForUserAsync(Guid id, PostForUpdateDto postForUpdate, bool compTrackChanges, bool empTrackChanges)
        {


            var postFromDb = await GetpostForUserAndCheckIfItExists(id, empTrackChanges);

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

        private async Task<Post> GetpostForUserAndCheckIfItExists( Guid postId, bool trackChanges)
        {
            var postForUser = await _repository.Post.GetPostAsync(postId, trackChanges);
            if (postForUser is null)
                throw new PostNotFoundException(postId);

            return postForUser;
        }

        public async Task UpvotePost(string userId, Guid postId, PostDto postForUpdate, bool userTrackChanges, bool postTrackChanges)
        {

            var postFromDb = await GetpostForUserAndCheckIfItExists(postId, postTrackChanges);            

            _mapper.Map(postForUpdate, postFromDb);
            postFromDb.UpvoteCount++;

            await _repository.SaveAsync();

            var creationDate = DateTime.UtcNow;

            var createUserPostVote = new UserPostVote();
            createUserPostVote.CreationDate = creationDate;
            createUserPostVote.UpdateDate = null;
            createUserPostVote.VoteType = "up";



            _repository.UserPostVote.CreateUserPostVoteForUserAsync(userId, postId, createUserPostVote);
            await _repository.SaveAsync();

        }

        public async Task DownvotePost(string userId, Guid postId, PostDto postForUpdate, bool userTrackChanges, bool postTrackChanges)
        {
            await CheckIfUserExists(userId, userTrackChanges);

            var postFromDb = await GetpostForUserAndCheckIfItExists( postId, postTrackChanges);

            _mapper.Map(postForUpdate, postFromDb);

            postFromDb.DownvoteCount++;

            await _repository.SaveAsync();

            var creationDate = DateTime.UtcNow;

            var createUserPostVote = new UserPostVote();
            createUserPostVote.CreationDate = creationDate;
            createUserPostVote.UpdateDate = null;
            createUserPostVote.VoteType = "down";

            

            _repository.UserPostVote.CreateUserPostVoteForUserAsync(userId,postId, createUserPostVote);
            await _repository.SaveAsync();

            


        }

        public async Task<IEnumerable<PostDto>> GetPostsByUsername(string userName, bool trackChanges)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
                throw new UserNotFoundException(userName);

            var postEntities = await _repository.Post.GetAllPostsAsync(trackChanges);
                                                        

            var filteredEnttites = postEntities.Where(p => p.UserId.Equals(user.Id));

            var postDto = _mapper.Map<IEnumerable<PostDto>>(filteredEnttites);

            return postDto;

        }

        
    }
}
