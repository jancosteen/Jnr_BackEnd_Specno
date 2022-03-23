using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CommentService: ICommentService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        //private readonly ICommentLinks _commentLinks;
        private readonly UserManager<User> _userManager;

        public CommentService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper,
             UserManager<User> userManager)// ICommentLinks commentLinks,
        {
            _repository = repository;
            _logger = logger; ;
            _mapper = mapper;
            //_commentLinks = commentLinks;
            _userManager = userManager;
        }
        public async Task<CommentDto> CreateCommentAsync(string userId, Guid postId, CommentForCreationDto commentForCreation, bool trackChanges)
        {
            await CheckIfUserExists(userId, trackChanges);

            var commentEntity = _mapper.Map<Comment>(commentForCreation);

            var creationDate = DateTime.UtcNow;
            commentEntity.CreationDate = creationDate;

            _repository.Comment.CreateCommentAsync(userId, postId, commentEntity);
            await _repository.SaveAsync();

            var commentToReturn = _mapper.Map<CommentDto>(commentEntity);

            return commentToReturn;
        }

        public async Task DeleteCommentAsync(Guid id, bool trackChanges)
        {

            var commentForUser = await GetcommentForUserAndCheckIfItExists(id, trackChanges);

            _repository.Comment.DeleteCommentAsync(commentForUser);
            await _repository.SaveAsync();
        }

        public async Task<CommentDto> GetCommentAsync(Guid commentId, bool trackChanges)
        {

            var commentFromDb = await _repository.Comment.GetCommentAsync(commentId, trackChanges);
            if (commentFromDb is null)
                throw new CommentNotFoundException(commentId);

            var commentDto = _mapper.Map<CommentDto>(commentFromDb);

            return commentDto;
        }

        public async Task<IEnumerable<CommentDto>> GetAllCommentsAsync(bool trackChanges)
        {
            var commentEntities = await _repository.Comment.GetAllCommentsAsync(trackChanges);

            var commentDto = _mapper.Map<IEnumerable<CommentDto>>(commentEntities);

            return commentDto;
        }


        public async Task UpdateCommentAsync(Guid id, CommentForUpdateDto commentForUpdate, bool compTrackChanges, bool empTrackChanges)
        {

            var commentFromDb = await GetcommentForUserAndCheckIfItExists(id, empTrackChanges);

            commentForUpdate.CreationDate = commentFromDb.CreationDate;

            _mapper.Map(commentForUpdate, commentFromDb);
            commentFromDb.UpdateDate = DateTime.UtcNow;

            await _repository.SaveAsync();
        }

        private async Task CheckIfUserExists(string userId, bool trackChanges)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                throw new UserNotFoundException(userId);
        }

        private async Task<Comment> GetcommentForUserAndCheckIfItExists(Guid commentId, bool trackChanges)
        {
            var commentForUser = await _repository.Comment.GetCommentAsync(commentId, trackChanges);
            if (commentForUser is null)
                throw new CommentNotFoundException(commentId);

            return commentForUser;
        }

        public async Task UpvoteComment(string userId, Guid id, CommentDto commentForUpdate, bool userTrackChanges, bool postTrackChanges, bool commentTrackChanges)
        {
            await CheckIfUserExists(userId, userTrackChanges);

            var commentFromDb = await GetcommentForUserAndCheckIfItExists(id, commentTrackChanges);

            _mapper.Map(commentForUpdate, commentFromDb);
            commentFromDb.UpvoteCount++;

            await _repository.SaveAsync();

            var creationDate = DateTime.UtcNow;

            var createUserCommentVote = new UserCommentVote();
            createUserCommentVote.CreationDate = creationDate;
            createUserCommentVote.UpdateDate = null;
            createUserCommentVote.VoteType = "up";



            _repository.UserCommentVote.CreateUserCommentVoteForUserAsync(userId, id, createUserCommentVote);
            await _repository.SaveAsync();
        }

        public async Task DownvoteComment(string userId, Guid id, CommentDto commentForUpdate, bool userTrackChanges, bool postTrackChanges, bool commentTrackChanges)
        {
            await CheckIfUserExists(userId, userTrackChanges);

            var commentFromDb = await GetcommentForUserAndCheckIfItExists(id, commentTrackChanges);

            _mapper.Map(commentForUpdate, commentFromDb);
            commentFromDb.DownvoteCount++;

            await _repository.SaveAsync();

            var creationDate = DateTime.UtcNow;

            var createUserCommentVote = new UserCommentVote();
            createUserCommentVote.CreationDate = creationDate;
            createUserCommentVote.UpdateDate = null;
            createUserCommentVote.VoteType = "down";



            _repository.UserCommentVote.CreateUserCommentVoteForUserAsync(userId, id, createUserCommentVote);
            await _repository.SaveAsync();
        }
    }
}
