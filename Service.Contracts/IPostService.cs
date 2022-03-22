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
        Task<(LinkResponse linkResponse, MetaData metaData)> GetPostsAsync(Guid userId,
            PostLinkParameters linkParams, bool trackChanges);
        Task<PostDto> GetPostAsync(Guid userId, Guid postId, bool trackChanges);
        Task<PostDto> CreatePostForUserAsync(Guid userId, PostForCreationDto postForCreation, bool trackChanges);
        Task DeletePostForUserAsync(Guid userId, Guid id, bool trackChanges);
        Task UpdatePostForUserAsync(Guid userId, Guid id, PostForUpdateDto postForUpdate, bool compTrackChanges, bool empTrackChanges);
        Task<(PostForUpdateDto postToPatch, Post postEntity)> GetPostForPatchAsync(Guid userId, Guid id, bool compTrackChanges, bool empTrackChanges);
        Task SaveChangesForPatchAsync(PostForUpdateDto postToPach, Post postEntity);
    }
}
