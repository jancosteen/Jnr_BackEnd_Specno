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
        Task<CommentDto> GetCommentAsync(Guid commentId, bool trackChanges);
        Task<CommentDto> CreateCommentAsync(string userId, Guid postId, CommentForCreationDto commentForCreation, bool trackChanges);
        Task DeleteCommentAsync( Guid id, bool trackChanges);
        Task UpdateCommentAsync(Guid id, CommentForUpdateDto commentForUpdate, bool compTrackChanges, bool empTrackChanges);
        Task UpvoteComment(string userId, Guid id, CommentDto commentForUpdate, bool userTrackChanges,bool postTrackChanges, bool commentTrackChanges);
        Task DownvoteComment(string userId, Guid id, CommentDto commentForUpdate, bool userTrackChanges, bool postTrackChanges, bool commentTrackChanges);
    }
}
