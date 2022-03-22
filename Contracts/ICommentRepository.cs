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
        Task<PagedList<Comment>> GetCommentsByUserAsync(Guid userId,
        CommentParameters commentParameters, bool trackChanges);
        Task<PagedList<Comment>> GetCommentsOnPostAsync(Guid postId,
        CommentParameters commentParameters, bool trackChanges);
        Task<Comment> GetCommentAsync(Guid userId, Guid postId, Guid commentId, bool trackChanges);
        void CreateCommentAsync(Guid userId, Guid postId, Comment comment);
        void DeleteCommentAsync(Comment comment);
    }
}
