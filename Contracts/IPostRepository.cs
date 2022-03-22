using Entities.Models;
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
        void CreatePostAsync(Post post);
        Task<IEnumerable<Post>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeletePostAsync(Post post);
    }
}
