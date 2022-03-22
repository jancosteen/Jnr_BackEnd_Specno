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

        public async Task<PagedList<Post>> GetPostsAsync(string userId,
         PostParameters postParameters, bool trackChanges)
        {
            var posts = await FindByCondition(e => e.UserId.Equals(userId), trackChanges)
                .FilterPosts(postParameters.MinDate, postParameters.MaxDate)
                .Search(postParameters.SearchTerm)
                .Skip((postParameters.PageNumber - 1) * postParameters.PageSize)
                .Take(postParameters.PageSize)
                .ToListAsync();

            var count = await FindByCondition(e => e.UserId.Equals(userId), trackChanges).CountAsync();

            return new PagedList<Post>
                (posts, count, postParameters.PageNumber, postParameters.PageSize);
        }
    }
}
