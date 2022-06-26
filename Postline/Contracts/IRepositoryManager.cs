using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IPostRepository Post { get; }
        ICommentRepository Comment { get; }
        ICategoryRepository Category { get; }
        Task SaveAsync();
    }
}