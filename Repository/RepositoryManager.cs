using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {

        private readonly RepositoryContext _repositoryContext;

        private readonly Lazy<IPostRepository> _postRepository;
        private readonly Lazy<ICommentRepository> _commentRepository;
        private readonly Lazy<IUserPostVoteRepository> _userPostVoteRepository;
        private readonly Lazy<IUserCommentVoteRepository> _userCommentVoteRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;

            _postRepository = new Lazy<IPostRepository>(() => new PostRepository(repositoryContext));
            _commentRepository = new Lazy<ICommentRepository>(() => new CommentRepository(repositoryContext));
            _userPostVoteRepository = new Lazy<IUserPostVoteRepository>(() => new UserPostVoteRepository(repositoryContext));
            _userCommentVoteRepository = new Lazy<IUserCommentVoteRepository>(() => new UserCommentVoteRepository(repositoryContext));
        }

        public IPostRepository Post => _postRepository.Value;
        public ICommentRepository Comment => _commentRepository.Value;

        public IUserCommentVoteRepository UserCommentVote => _userCommentVoteRepository.Value;

        public IUserPostVoteRepository UserPostVote => _userPostVoteRepository.Value;

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
