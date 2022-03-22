using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PostRepository: RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(RepositoryContext repositoryContext): base(repositoryContext)
        {

        }

        public void CreatePostForUserAsync(string userId, Post post)
        {
            post.UserId = userId;
            Create(post);
        }

        public void DeletePostAsync(Post post) =>
            Delete(post);

        public async Task<Post> GetPostAsync(string userId, Guid postId, bool trackChanges) =>
            await FindByCondition(e => e.UserId.Equals(userId) && e.Id.Equals(postId), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<IEnumerable<Post>> GetAllPostsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(c => c.Title)
            .ToListAsync();
    }
}

