using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserPostVote
    {
        Task<IEnumerable<UserPostVote>> GetAllUserPostVotesAsync(bool trackChanges);
        Task<UserPostVote> GetUserPostVoteAsync(Guid userId, Guid postId, bool trackChanges);
        void CreateUserPostVoteAsync(UserPostVote userPostVote);
        Task<IEnumerable<UserPostVote>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteUserPostVoteAsync(UserPostVote userPostVote);
    }
}
