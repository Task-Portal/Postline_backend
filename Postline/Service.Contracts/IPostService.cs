using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.ForCreation;
using Shared.DataTransferObjects.ForShow;
using Shared.DataTransferObjects.ForUpdate;

namespace Service.Contracts
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> GetAllPostsAsync(bool trackChanges);
        Task<PostDto> GetPostAsync(Guid postId, bool trackChanges);
        Task<PostDto> CreatePostAsync(PostForCreationDto post);
        Task<IEnumerable<PostDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges); 
        Task<IEnumerable<PostDto>> GetPostsByUserIdAsync(Guid id, bool trackChanges);
        // Task<(IEnumerable<PostDto> posts, string ids)> CreatePostCollectionAsync
        //     (IEnumerable<PostForCreationDto> postCollection);
         Task DeletePostAsync(Guid postId, bool trackChanges);
           Task UpdatePostAsync(Guid postId, PostForUpdateDto postForUpdate, bool trackChanges);
           
         
          
         
    }
}