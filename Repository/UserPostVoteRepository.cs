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
    public class UserPostVoteRepository : RepositoryBase<UserPostVote>, IUserPostVoteRepository
    {
        public UserPostVoteRepository(RepositoryContext repositoryContext): base(repositoryContext)
        {

        }
        public void CreateUserPostVoteForUserAsync(string userId, Guid postId, UserPostVote userPostVote)
        {
            userPostVote.UserId = userId;
            userPostVote.PostId = postId;
            Create(userPostVote);
        }

        public void DeleteUserPostVoteAsync(UserPostVote userPostVote) =>
            Delete(userPostVote);

        public async Task<UserPostVote> GetUserPostVoteAsync(string userId, Guid postId, Guid userPostVoteId, bool trackChanges) =>
            await FindByCondition(e => e.UserId.Equals(userId) && e.PostId.Equals(postId) && e.Id.Equals(userPostVoteId), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<PagedList<UserPostVote>> GetUserPostVotesAsync(string userId, Guid postId,
         UserPostVoteParameters userPostVoteParameters, bool trackChanges)
        {
            var userPostVotes = await FindByCondition(e => e.UserId.Equals(userId) && e.PostId.Equals(postId), trackChanges)
                .FilterUserPostVotes(userPostVoteParameters.MinDate, userPostVoteParameters.MaxDate)
                .Search(userPostVoteParameters.SearchTerm)
                .Skip((userPostVoteParameters.PageNumber - 1) * userPostVoteParameters.PageSize)
                .Take(userPostVoteParameters.PageSize)
                .ToListAsync();

            var count = await FindByCondition(e => e.UserId.Equals(userId), trackChanges).CountAsync();

            return new PagedList<UserPostVote>
                (userPostVotes, count, userPostVoteParameters.PageNumber, userPostVoteParameters.PageSize);
        }
    }
}
