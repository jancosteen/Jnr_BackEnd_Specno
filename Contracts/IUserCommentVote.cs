using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserCommentVote
    {
        Task<IEnumerable<UserCommentVote>> GetAllUserCommentVotesAsync(bool trackChanges);
        Task<UserCommentVote> GetUserCommentVoteAsync(Guid userId, Guid commentId, bool trackChanges);
        void CreateUserCommentVoteAsync(UserCommentVote userCommentVote);
        Task<IEnumerable<UserCommentVote>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteUserCommentVoteAsync(UserCommentVote userCommentVote);
    }
}
