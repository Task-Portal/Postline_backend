﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync(bool trackChanges);
        Task<Post> GetPostAsync(Guid postId, bool trackChanges);
        void CreatePost(Post post);
        Task<IEnumerable<Post>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        Task<IEnumerable<Post>> GetPostsByUserIdAsync(Guid userId, bool trackChanges);

        void DeletePost(Post post);
        
        Task<IEnumerable<Post>> GetAllPostsWithDetailsAsync(bool trackChanges);
        Task<Post> GetAllPostWithDetailsAsync(Guid postId,bool trackChanges);
    }
}