using Entities.Models;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetAllCommentsAsync(bool trackChanges);
        Task<CommentDto> GetCommentAsync(string userId, Guid postId, Guid commentId, bool trackChanges);
        Task<CommentDto> CreateCommentAsync(string userId, Guid postId, CommentForCreationDto commentForCreation, bool trackChanges);
        Task DeleteCommentAsync(string userId, Guid postId, Guid id, bool trackChanges);
        Task UpdateCommentAsync(string userId, Guid postId, Guid id, CommentForUpdateDto commentForUpdate, bool compTrackChanges, bool empTrackChanges);
        //Task<(CommentForUpdateDto commentToPatch, Comment commentEntity)> GetCommentForPatchAsync(string userId, Guid postId, Guid id, bool compTrackChanges, bool empTrackChanges);
        Task SaveChangesForPatchAsync(CommentForUpdateDto commentToPach, Comment commentEntity);

        Task UpvoteComment(string userId, Guid postId, Guid id, CommentDto commentForUpdate, bool userTrackChanges,bool postTrackChanges, bool commentTrackChanges);
        Task DownvoteComment(string userId, Guid postId, Guid id, CommentDto commentForUpdate, bool userTrackChanges, bool postTrackChanges, bool commentTrackChanges);
    }
}
