using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllCommentsAsync(bool trackChanges);
        Task<Comment> GetCommentAsync(Guid commentId, bool trackChanges);
        void CreateCommentAsync(Comment Comment);
        Task<IEnumerable<Comment>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteCommentAsync(Comment Comment);
    }
}
