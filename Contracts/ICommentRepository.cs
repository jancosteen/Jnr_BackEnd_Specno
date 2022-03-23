using Entities.Models;
using Shared.RequestFeatures;
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
        Task<Comment> GetCommentAsync( Guid commentId, bool trackChanges);
        void CreateCommentAsync(string userId, Guid postId, Comment comment);
        void DeleteCommentAsync(Comment comment);
    }
}
