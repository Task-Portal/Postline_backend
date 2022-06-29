using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.ForCreation;
using Shared.DataTransferObjects.ForShow;
using Shared.DataTransferObjects.ForUpdate;

namespace Service
{
    internal sealed class PostService : IPostService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private User _user;

        public PostService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper,UserManager<User> userManager)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsAsync(bool trackChanges)
        {
            // var posts = await _repository.Post.GetAllPostsAsync(trackChanges);
            var posts = await _repository.Post.GetAllPostsWithDetailsAsync(trackChanges);

            foreach (var post in posts)
            {
                post.User = await _userManager.FindByIdAsync(post.UserId.ToString());
            }
            
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);

            return postsDto;
        }

        public async Task<PostDto> GetPostAsync(Guid id, bool trackChanges)
        {
            var post = await GetPostAndCheckIfItExists(id, trackChanges);

            var postDto = _mapper.Map<PostDto>(post);
            return postDto;
        }

        public async Task<PostDto> CreatePostAsync(PostForCreationDto post)
        {
        	var postEntity = _mapper.Map<Post>(post);
            postEntity.PostDate = DateTime.Now;
        	_repository.Post.CreatePost(postEntity);
        	await _repository.SaveAsync();
        
        	var postToReturn = _mapper.Map<PostDto>(postEntity);
        
        	return postToReturn;
        }
        
        public async Task<IEnumerable<PostDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
        	if (ids is null)
        		throw new IdParametersBadRequestException();
        
        	var postEntities = await _repository.Post.GetByIdsAsync(ids, trackChanges);
        	if (ids.Count() != postEntities.Count())
        		throw new CollectionByIdsBadRequestException();
        
        	var postsToReturn = _mapper.Map<IEnumerable<PostDto>>(postEntities);
        
        	return postsToReturn;
        }

        public async Task<IEnumerable<PostDto>> GetPostsByUserIdAsync(Guid userid, bool trackChanges)
        {
            _user = await _userManager.FindByIdAsync(userid.ToString());
            if (_user is null)
                throw new IdParametersBadRequestException();

            var post = await _repository.Post.GetPostsByUserIdWithDetailsAsync(userid, trackChanges);
            var postsToReturn = _mapper.Map<IEnumerable<PostDto>>(post);
        
            return postsToReturn;
        } 
        
       

        // public async Task<(IEnumerable<PostDto> posts, string ids)> CreatePostCollectionAsync
        // 	(IEnumerable<PostForCreationDto> postCollection)
        // {
        // 	if (postCollection is null)
        // 		throw new PostCollectionBadRequest();
        //
        // 	var postEntities = _mapper.Map<IEnumerable<Post>>(postCollection);
        // 	foreach (var post in postEntities)
        // 	{
        // 		_repository.Post.CreatePost(post);
        // 	}
        //
        // 	await _repository.SaveAsync();
        //
        // 	var postCollectionToReturn = _mapper.Map<IEnumerable<PostDto>>(postEntities);
        // 	var ids = string.Join(",", postCollectionToReturn.Select(c => c.Id));
        //
        // 	return (posts: postCollectionToReturn, ids: ids);
        // }
        //
        public async Task DeletePostAsync(Guid postId, bool trackChanges)
        {
        	var post = await GetPostAndCheckIfItExists(postId, trackChanges);
        
        	_repository.Post.DeletePost(post);
        	await _repository.SaveAsync();
        }
        
        public async Task UpdatePostAsync(Guid postId,
        	PostForUpdateDto postForUpdate, bool trackChanges)
        {
        	var post = await GetPostAndCheckIfItExists(postId, trackChanges);
            post.User = await _userManager.FindByIdAsync(post.UserId.ToString());
        	_mapper.Map(postForUpdate, post);
        	await _repository.SaveAsync();
        }
        
        private async Task<Post> GetPostAndCheckIfItExists(Guid id, bool trackChanges)
        {
            var post = await _repository.Post.GetPostWithDetailsAsync(id, trackChanges);
            if (post is null)
                throw new PostNotFoundException(id);

            post.User = await GetUserAndCheckIfItExists(post.UserId);
            return post;
        } 
        
        private async Task<User> GetUserAndCheckIfItExists(Guid userid)
        {
            _user = await _userManager.FindByIdAsync(userid.ToString());
            
            if (_user is null)
                throw new UserNotFoundException(userid);
        
            return _user;
        }
        
        
    }
}