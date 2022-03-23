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
    public class CommentRepository: RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(RepositoryContext repositoryContext): base(repositoryContext)
        {

        }

        public void CreateCommentAsync(string userId,  Guid postId, Comment comment)
        {
            comment.UserId = userId;
            comment.PostId = postId;
            Create(comment);
        }

        public void DeleteCommentAsync(Comment comment) =>
            Delete(comment);

        public async Task<Comment> GetCommentAsync(string userId, Guid postId, Guid commentId, bool trackChanges) =>
            await FindByCondition(e => e.UserId.Equals(userId) && e.PostId.Equals(postId) && e.Id.Equals(commentId), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<PagedList<Comment>> GetCommentsByUserAsync(string userId,
         CommentParameters commentParameters, bool trackChanges)
        {
            var comments = await FindByCondition(e => e.UserId.Equals(userId), trackChanges)
                .FilterComments(commentParameters.MinDate, commentParameters.MaxDate)
                .Search(commentParameters.SearchTerm)
                .Skip((commentParameters.PageNumber - 1) * commentParameters.PageSize)
                .Take(commentParameters.PageSize)
                .ToListAsync();

            var count = await FindByCondition(e => e.UserId.Equals(userId), trackChanges).CountAsync();

            return new PagedList<Comment>
                (comments, count, commentParameters.PageNumber, commentParameters.PageSize);
        }

        public async Task<PagedList<Comment>> GetCommentsOnPostAsync(Guid postId,
         CommentParameters commentParameters, bool trackChanges)
        {
            var comments = await FindByCondition(e => e.PostId.Equals(postId), trackChanges)
                .FilterComments(commentParameters.MinDate, commentParameters.MaxDate)
                .Search(commentParameters.SearchTerm)
                .Skip((commentParameters.PageNumber - 1) * commentParameters.PageSize)
                .Take(commentParameters.PageSize)
                .ToListAsync();

            var count = await FindByCondition(e => e.UserId.Equals(postId), trackChanges).CountAsync();

            return new PagedList<Comment>
                (comments, count, commentParameters.PageNumber, commentParameters.PageSize);
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(c => c.PostId)
            .ToListAsync();
    }
}
