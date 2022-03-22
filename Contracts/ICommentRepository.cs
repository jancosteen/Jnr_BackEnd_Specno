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
        Task<PagedList<Comment>> GetCommentsByUserAsync(string userId,
        CommentParameters commentParameters, bool trackChanges);
        Task<PagedList<Comment>> GetCommentsOnPostAsync(Guid postId,
        CommentParameters commentParameters, bool trackChanges);
        Task<Comment> GetCommentAsync(string userId, Guid postId, Guid commentId, bool trackChanges);
        void CreateCommentAsync(string userId, Guid postId, Comment comment);
        void DeleteCommentAsync(Comment comment);
    }
}
