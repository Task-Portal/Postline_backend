using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postline.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects.ForCreation;
using Shared.DataTransferObjects.ForUpdate;

namespace Postline.Presentation.Controllers
{
    [Route("api/categories")]
    [Authorize(Roles = "Manager")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CategoryController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _service.CategoryService.GetAllCategoriesAsync(false);

            return Ok(categories);
        }


        [HttpGet("{id:guid}", Name = "CategoryById")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var category = await _service.CategoryService.GetCategoryAsync(id, false);
            return Ok(category);
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
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryForCreationDto category)
        {
            var createdCategory = await _service.CategoryService.CreateCategoryAsync(category);

            return CreatedAtRoute("CategoryById", new { id = createdCategory.Id }, createdCategory);
        }

        // [HttpPost("collection")]
        // public async Task<IActionResult> CreatePostCollection
        //     ([FromBody] IEnumerable<PostForCreationDto> companyCollection)
        // {
        //     var result = await _service.PostService.CreatePostCollectionAsync(companyCollection);
        //
        //     return CreatedAtRoute("PostCollection", new { result.ids }, result.companies);
        // }
        //

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _service.CategoryService.DeleteCategoryAsync(id, false);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryForUpdateDto category)
        {
            await _service.CategoryService.UpdateCategoryAsync(id, category, true);

            return NoContent();
        }
    }
}