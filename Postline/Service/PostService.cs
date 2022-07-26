using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Exceptions.BadRequestExceptions;
using Entities.Exceptions.NotFoundExceptions;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.ForCreation;
using Shared.DataTransferObjects.ForShow;
using Shared.DataTransferObjects.ForUpdate;
using Shared.RequestFeatures;

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

        public async Task<(IEnumerable<PostDto> posts, MetaData metaData)> GetAllPostsAsync(PostParameters postParameters,bool trackChanges)
        {
            if (!postParameters.ValidDateTimeRange) 
                throw new MaxDateRangeBadRequestException(); 
            
            var postsWithMetaData =   await _repository.Post.GetAllPostsWithDetailsAsync(postParameters,trackChanges);


            foreach (var post in postsWithMetaData)
            {
                post.Rating = _repository.Point.GetNumberByPostId(post.Id,false);
            }
            
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(postsWithMetaData);
            

            return  (posts: postsDto, metaData: postsWithMetaData.MetaData);
        }

        public async Task<PostDto> GetPostAsync(Guid id, bool trackChanges)
        {
            var post = await GetPostAndCheckIfItExists(id, trackChanges);
            post.Rating =  _repository.Point.GetNumberByPostId(post.Id,false);
            var postDto = _mapper.Map<PostDto>(post);
            
            return postDto;
        }

        public async Task<PostDto> CreatePostAsync(PostForCreationDto post, string name)
        {
        	var postEntity = _mapper.Map<Post>(post);         
            postEntity.User = await _userManager.FindByNameAsync(name);
            postEntity.Category =await _repository.Category.GetCategoryAsync(post.CategoryId,true);
            postEntity.PostDate = DateTime.Now;
            postEntity.Rating =0;
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

       

        public async Task<IEnumerable<PostDto>> GetPostsByUserName(string name, bool trackChanges)
        {
            _user = await _userManager.FindByNameAsync(name);
            if (_user is null)
                throw new IdParametersBadRequestException();

            var post = await _repository.Post.GetPostsByUserNameWithDetailsAsync(name, trackChanges);
            var postsToReturn = _mapper.Map<IEnumerable<PostDto>>(post);
        
            return postsToReturn;
        } 
        
       
        
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
            post.User = await _userManager.FindByIdAsync(post.User.Id);
            post.PostDate = DateTime.Now;
        	_mapper.Map(postForUpdate, post);
        	await _repository.SaveAsync();
        }
        
        private async Task<Post> GetPostAndCheckIfItExists(Guid id, bool trackChanges)
        {
            var post = await _repository.Post.GetPostWithDetailsAsync(id, trackChanges);
            if (post is null)
                throw new PostNotFoundException(id);

            post.User = await GetUserAndCheckIfItExists(Guid.Parse(post.User.Id));
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