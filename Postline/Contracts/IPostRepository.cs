using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface IPostRepository
    {
        // Task<IEnumerable<Post>> GetAllPostsAsync(bool trackChanges);
        Task<Post> GetPostAsync(Guid postId, bool trackChanges);
        void CreatePost(Post post);
        Task<IEnumerable<Post>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        Task<IEnumerable<Post>> GetPostsByUserNameWithDetailsAsync(string name, bool trackChanges);
        Task<IEnumerable<Post>> GetPostsByUserIdWithDetailsAsync(Guid userId, bool trackChanges);

        void DeletePost(Post post);
        
        Task<PagedList<Post>> GetAllPostsWithDetailsAsync(PostParameters postParameters,bool trackChanges);
        Task<Post> GetPostWithDetailsAsync(Guid postId,bool trackChanges);
        
    }
}