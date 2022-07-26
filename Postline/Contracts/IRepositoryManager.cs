using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IPostRepository Post { get; }
        ICommentRepository Comment { get; }
        ICategoryRepository Category { get; }
        
        IPointRepository Point { get; }
        Task SaveAsync();
    }
}