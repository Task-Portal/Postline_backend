using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Postline.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.ForCreation;
using Shared.DataTransferObjects.ForUpdate;

namespace Postline.Presentation.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
       
        
        private readonly IServiceManager _service;

        public PostController(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _service.PostService.GetAllPostsAsync(trackChanges: false);
        
            return Ok(posts);
        }

        [HttpGet("{id:guid}", Name = "PostById")]
       
        public async Task<IActionResult> GetPost(Guid id)
        {
            var post = await _service.PostService.GetPostAsync(id, trackChanges: false);
            return Ok(post);
        } 
        
        [HttpGet("user/{userId:guid}", Name = "PostsByUserId")]
        // [Authorize]
        public async Task<IActionResult> GetPostsByUserId(Guid userId)
        {
            var post = await _service.PostService.GetPostsByUserIdAsync(userId, trackChanges: false);
            return Ok(post);
        }
        
        
        
        
        
        

        // [HttpGet("collection/({ids})", Name = "PostCollection")]
        // public async Task<IActionResult> GetPostCollection
        //     ([ModelBinder(BinderType = typeof(ArrayModelBinder<>))] IEnumerable<Guid> ids)
        // {
        //     var companies = await _service.PostService.GetByIdsAsync(ids, trackChanges: false);
        //
        //     return Ok(companies);
        // }
        //
        
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Authorize]
        public async Task<IActionResult> CreatePost([FromBody] PostForCreationDto post)
        {
            var createdPost = await _service.PostService.CreatePostAsync(post);
        
            return CreatedAtRoute("PostById", new { id = createdPost.Id }, createdPost);
        }
        
        // [HttpPost("collection")]
        // public async Task<IActionResult> CreatePostCollection
        //     ([FromBody] IEnumerable<PostForCreationDto> postCollection)
        // {
        //     var result = await _service.PostService.CreatePostCollectionAsync(postCollection);
        //
        //     return CreatedAtRoute("PostCollection", new { result.ids }, result.companies);
        // }
        //
         [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
           
            await _service.PostService.DeletePostAsync(id, trackChanges: false);
        
            return NoContent();
        }
        
        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Authorize]
        public async Task<IActionResult> UpdatePost(Guid id, [FromBody] PostForUpdateDto post)
        {
            await _service.PostService.UpdatePostAsync(id, post, trackChanges: true);
        
            return NoContent();
        }

       

       
       

       
    }
}