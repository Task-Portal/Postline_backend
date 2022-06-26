using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.DataTransferObjects.ForCreation;
using Shared.DataTransferObjects.ForShow;
using Shared.DataTransferObjects.ForUpdate;

namespace Service.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(bool trackChanges);
        Task<CategoryDto> GetCategoryAsync(Guid categoryId, bool trackChanges);

        Task<CategoryDto> CreateCategoryAsync(CategoryForCreationDto category);

        Task DeleteCategoryAsync(Guid categoryId, bool trackChanges);

        Task UpdateCategoryAsync(Guid categoryId, CategoryForUpdateDto categoryForUpdate, bool trackChanges);
    }
}