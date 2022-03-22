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
        /*Task<(LinkResponse linkResponse, MetaData metaData)> GetPostsAsync(Guid userId,
            PostLinkParameters linkParams, bool trackChanges);*/
        Task<PostDto> GetPostAsync(string userId, Guid postId, bool trackChanges);
        Task<PostDto> CreatePostForUserAsync(string userId, PostForCreationDto postForCreation, bool trackChanges);
        Task DeletePostForUserAsync(string userId, Guid id, bool trackChanges);
        Task UpdatePostForUserAsync(string userId, Guid id, PostForUpdateDto postForUpdate, bool compTrackChanges, bool empTrackChanges);
        Task<(PostForUpdateDto postToPatch, Post postEntity)> GetPostForPatchAsync(string userId, Guid id, bool compTrackChanges, bool empTrackChanges);
        Task SaveChangesForPatchAsync(PostForUpdateDto postToPach, Post postEntity);
    }
}
