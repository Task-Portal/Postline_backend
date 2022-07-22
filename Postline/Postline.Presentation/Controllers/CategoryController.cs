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

        

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryForCreationDto category)
        {
            var createdCategory = await _service.CategoryService.CreateCategoryAsync(category);

            return CreatedAtRoute("CategoryById", new { id = createdCategory.Id }, createdCategory);
        }

       

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _service.CategoryService.DeleteCategoryAsync(id, false);

            return NoContent();
        }

        [HttpPut()]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateCategory( [FromBody] CategoryForUpdateDto category)
        {
            await _service.CategoryService.UpdateCategoryAsync( category, true);

            return NoContent();
        }
    }
}