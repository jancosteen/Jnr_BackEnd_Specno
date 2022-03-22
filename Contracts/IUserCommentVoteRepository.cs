using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserCommentVoteRepository
    {
        Task<PagedList<UserCommentVote>> GetUserCommentVotesAsync(Guid userId, Guid commentId,
        UserCommentVoteParameters userCommentVoteParameters, bool trackChanges);
        Task<UserCommentVote> GetUserCommentVoteAsync(Guid userId, Guid commentId, Guid userCommentVoteId, bool trackChanges);
        void CreateUserCommentVoteForUserAsync(Guid userId, Guid commentId, UserCommentVote userCommentVote);
        void DeleteUserCommentVoteAsync(UserCommentVote userCommentVote);
    }
}
