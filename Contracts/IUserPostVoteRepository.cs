using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserPostVoteRepository
    {
        Task<PagedList<UserPostVote>> GetUserPostVotesAsync(string userId, Guid postId,
        UserPostVoteParameters userPostVoteParameters, bool trackChanges);
        Task<UserPostVote> GetUserPostVoteAsync(string userId, Guid postId, Guid userPostVoteId, bool trackChanges);
        void CreateUserPostVoteForUserAsync(string userId, Guid postId, UserPostVote userPostVote);
        void DeleteUserPostVoteAsync(UserPostVote userPostVote);
    }
}
