using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IPostRepository Post { get; }
        ICommentRepository Comment { get; }
        IUserCommentVoteRepository UserCommentVote { get; }
        IUserPostVoteRepository UserPostVote { get; }
        Task SaveAsync();
    }
}
