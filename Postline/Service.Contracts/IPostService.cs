using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.ForCreation;
using Shared.DataTransferObjects.ForShow;
using Shared.DataTransferObjects.ForUpdate;
using Shared.RequestFeatures;

namespace Service.Contracts
{
    public interface IPostService
    {
        Task<(IEnumerable<PostDto> posts, MetaData metaData)> GetAllPostsAsync(PostParameters postParameters,bool trackChanges);
        Task<PostDto> GetPostAsync(Guid postId, bool trackChanges);
        Task<PostDto> CreatePostAsync(PostForCreationDto post, string name);
        Task<IEnumerable<PostDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        

        Task DeletePostAsync(Guid postId, bool trackChanges);
        Task UpdatePostAsync(Guid postId, PostForUpdateDto postForUpdate, bool trackChanges);

        Task<IEnumerable<PostDto>> GetPostsByUserName(string name, bool trackChanges);
    }
}