using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository.Repositories
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }


        public async Task<IEnumerable<Post>> GetAllPostsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(c => c.PostDate)
                .ToListAsync();

        public async Task<Post> GetPostAsync(Guid postId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(postId), trackChanges)
                .SingleOrDefaultAsync();

        public void CreatePost(Post post) => Create(post);

        public async Task<IEnumerable<Post>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges)
                .ToListAsync();

        public async Task<IEnumerable<Post>> GetPostsByUserIdAsync(Guid userId, bool trackChanges) =>
            await FindByCondition(x => x.User.Id.Equals(userId), trackChanges)
                .ToListAsync();


        public async Task<IEnumerable<Post>> GetPostsByUserIdWithDetailsAsync(Guid userId, bool trackChanges) =>
            await FindByCondition(x => x.User.Id.Equals(userId), trackChanges).Include(u => u.User)
                .Include(c => c.Category)
                .OrderBy(c => c.PostDate)
                .ToListAsync();

        public async Task<IEnumerable<Post>> GetPostsByUserNameWithDetailsAsync(string name, bool trackChanges) =>
            await FindByCondition(x => x.User.UserName.Equals(name), trackChanges).Include(u => u.User)
                .Include(c => c.Category)
                .OrderBy(c => c.PostDate)
                .ToListAsync();


        public void DeletePost(Post post) => Delete(post);


        public async Task<PagedList<Post>> GetAllPostsWithDetailsAsync(PostParameters postParameters, bool trackChanges)
        {
            
            var posts = await FindAll(trackChanges)
                .FilterPostsByCategory(postParameters.CategoryName)
                .FilterPostsByDate(postParameters.PostFrom, postParameters.PostTo)
                .Search(postParameters.SearchTerm)
                .Include(u => u.User)
                .Include(c => c.Category)
                .OrderBy(c => c.PostDate)
                .ToListAsync();
           
            return  PagedList<Post>.ToPagedList(posts,  postParameters.PageNumber, postParameters.PageSize);    
            
        }
        public async Task<Post> GetPostWithDetailsAsync(Guid postId, bool trackChanges)
        {
            return await FindByCondition(c => c.Id.Equals(postId), trackChanges)
                .Include(u => u.User)
                .Include(c => c.Category)
                .SingleOrDefaultAsync();
        }
    }
}