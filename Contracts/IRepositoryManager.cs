using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IPostRepository Post { get; }
        ICommentRepository Comment { get; }
        IUserCommentVoteRepository UserCommentVote { get; }
        IUserPostVoteRepository UserPostVote { get; }
        Task SaveAsync();
    }
}
