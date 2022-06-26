using System;
using System.Threading.Tasks;
using Contracts;
using Repository.Repositories;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IPostRepository> _postRepository;
        private readonly Lazy<ICommentRepository> _commentRepository;
        private readonly Lazy<ICategoryRepository> _categoryRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _postRepository = new Lazy<IPostRepository>(() => new PostRepository(repositoryContext));
            _commentRepository = new Lazy<ICommentRepository>(() => new CommentRepository(repositoryContext));
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(repositoryContext));
        }

        public IPostRepository Post => _postRepository.Value;
        public ICommentRepository Comment => _commentRepository.Value;
        public ICategoryRepository Category => _categoryRepository.Value;
        
        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}