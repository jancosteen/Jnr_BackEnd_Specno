using Entities.LinkModels;
using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> GetAllPostsAsync(bool trackChanges);
        Task<PostDto> GetPostAsync(Guid postId, bool trackChanges);
        Task<IEnumerable<PostDto>> GetPostsByUsername(string userName, bool trackChanges);
        Task<PostDto> CreatePostForUserAsync(string userId, PostForCreationDto postForCreation, bool trackChanges);
        Task DeletePostForUserAsync(Guid id, bool trackChanges);
        Task UpdatePostForUserAsync(Guid id, PostForUpdateDto postForUpdate, bool compTrackChanges, bool empTrackChanges);
        Task UpvotePost(string userId, Guid postId, PostDto postForUpdate, bool userTrackChanges, bool postTrackChanges);
        Task DownvotePost(string userId, Guid postId, PostDto postForUpdate, bool userTrackChanges, bool postTrackChanges);

    }
}
