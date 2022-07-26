using System;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IPointRepository
    {
        void CreatePoint(Point point);

        int GetNumberByPostId(Guid id, bool trackChanges);
        Task<Point> GetPointByPostIdAndUserIdAsync(string userId, Guid postId, bool trackChanges);
        
        void DeletePoint(Point point);
    }
}