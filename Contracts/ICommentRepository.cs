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
        Task<PagedList<Comment>> GetCommentsAsync(Guid userId,
        CommentParameters commentParameters, bool trackChanges);
        Task<Comment> GetCommentAsync(Guid userId, Guid commentId, bool trackChanges);
        void CreateCommentForUserAsync(Guid userId, Comment comment);
        void DeleteCommentAsync(Comment comment);
    }
}
