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
        private readonly Lazy<ICompanyRepository> _companyRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IPostRepository> _postRepository;
        private readonly Lazy<ICommentRepository> _commentRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _companyRepository = new Lazy<ICompanyRepository>(() => new CompanyRepository(repositoryContext));
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(repositoryContext));
            _postRepository = new Lazy<IPostRepository>(() => new PostRepository(repositoryContext));
            _commentRepository = new Lazy<ICommentRepository>(() => new CommentRepository(repositoryContext));
        }
        public ICompanyRepository Company => _companyRepository.Value;

        public IEmployeeRepository Employee => _employeeRepository.Value;
        public IPostRepository Post => _postRepository.Value;
        public ICommentRepository Comment => _commentRepository.Value;

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
