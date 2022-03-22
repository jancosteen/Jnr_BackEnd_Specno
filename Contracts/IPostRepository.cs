using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPostRepository
    {
        Task<PagedList<Post>> GetPostsAsync(string userId,
        PostParameters postParameters, bool trackChanges);
        Task<Post> GetPostAsync(string userId, Guid postId, bool trackChanges);
        void CreatePostForUserAsync(string userId, Post post);
        void DeletePostAsync(Post post);
    }
}
