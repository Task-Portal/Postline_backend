﻿using System;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postline.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects.ForCreation;
using Shared.DataTransferObjects.ForUpdate;

namespace Postline.Presentation.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IServiceManager _service;
        
        public PostController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _service.PostService.GetAllPostsAsync(false);

            return Ok(posts);
        }

        [HttpGet("{id:guid}", Name = "PostById")]
      
        public async Task<IActionResult> GetPost(Guid id)
        {
            var post = await _service.PostService.GetPostAsync(id, false);
            return Ok(post);
        }

        [HttpGet("user")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetPostsByUserName()
        {
            var post = await _service.PostService.GetPostsByUserName(User.Identity.Name, false);
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
            var createdPost = await _service.PostService.CreatePostAsync(post, User.Identity.Name);

            return CreatedAtAction("CreatePost", new { id = createdPost.Id }, createdPost);
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
            await _service.PostService.DeletePostAsync(id, false);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Authorize]
        public async Task<IActionResult> UpdatePost(Guid id, [FromBody] PostForUpdateDto post)
        {
            await _service.PostService.UpdatePostAsync(id, post, true);

            return NoContent();
        }
    }
}