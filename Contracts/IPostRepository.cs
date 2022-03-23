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
        Task<IEnumerable<Post>> GetAllPostsAsync(bool trackChanges);
        Task<Post> GetPostAsync(Guid postId, bool trackChanges);
        void CreatePostForUserAsync(string userId, Post post);
        void DeletePostAsync(Post post);
    }
}
