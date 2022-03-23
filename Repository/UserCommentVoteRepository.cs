using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserCommentVoteRepository: RepositoryBase<UserCommentVote>, IUserCommentVoteRepository
    {
        public UserCommentVoteRepository(RepositoryContext repositoryContext): base(repositoryContext)
        {

        }

        public void CreateUserCommentVoteForUserAsync(string userId, Guid commentId, UserCommentVote userCommentVote)
        {
            userCommentVote.UserId = userId;
            userCommentVote.CommentId = commentId;
            Create(userCommentVote);
        }

        public void DeleteUserCommentVoteAsync(UserCommentVote userCommentVote) =>
            Delete(userCommentVote);

        public async Task<UserCommentVote> GetUserCommentVoteAsync(string userId, Guid commentId, Guid userCommentVoteId, bool trackChanges) =>
            await FindByCondition(e => e.UserId.Equals(userId) && e.CommentId.Equals(commentId) && e.Id.Equals(userCommentVoteId), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<PagedList<UserCommentVote>> GetUserCommentVotesAsync(string userId, Guid commentId,
         UserCommentVoteParameters userCommentVoteParameters, bool trackChanges)
        {
            var userCommentVotes = await FindByCondition(e => e.UserId.Equals(userId) && e.CommentId.Equals(commentId), trackChanges)
                .FilterUserCommentVotes(userCommentVoteParameters.MinDate, userCommentVoteParameters.MaxDate)
                .Search(userCommentVoteParameters.SearchTerm)
                .Skip((userCommentVoteParameters.PageNumber - 1) * userCommentVoteParameters.PageSize)
                .Take(userCommentVoteParameters.PageSize)
                .ToListAsync();

            var count = await FindByCondition(e => e.UserId.Equals(userId), trackChanges).CountAsync();

            return new PagedList<UserCommentVote>
                (userCommentVotes, count, userCommentVoteParameters.PageNumber, userCommentVoteParameters.PageSize);
        }
    }
}
