using System;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class PointRepository:RepositoryBase<Point>, IPointRepository
    {
        public PointRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        
        public void CreatePoint(Point point) => Create(point);
       
        
        public  int GetNumberByPostId(Guid id,bool trackChanges)
        {
           var numberInc=  FindByCondition(p => p.Post.Id == id && p.IsIncrement == true,trackChanges).Count();
           var numberDecr=  FindByCondition(p => p.Post.Id == id && p.IsIncrement == false,trackChanges).Count();
           return numberInc - numberDecr;
        }
        
       
        public async  Task<Point> GetPointByPostIdAndUserIdAsync(string userId, Guid postId, bool trackChanges)
        {
            var result = await FindByCondition(p => p.Post.Id == postId && p.User.Id == userId,
                trackChanges).FirstOrDefaultAsync();
            return result;
        }
        
        public void DeletePoint(Point point) => Delete(point);
    }
}