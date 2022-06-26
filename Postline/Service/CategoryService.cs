using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.ForCreation;
using Shared.DataTransferObjects.ForShow;
using Shared.DataTransferObjects.ForUpdate;

namespace Service
{
    internal sealed class CategoryService : ICategoryService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public CategoryService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(bool trackChanges)
        {
            var categories = await _repository.Category.GetAllCategoriesAsync(trackChanges);
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryAsync(Guid categoryId, bool trackChanges)
        {
            var category = await GetCategoryAndCheckIfItExists(categoryId, trackChanges);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryForCreationDto category)
        {
            var categoryEntity = _mapper.Map<Category>(category);

            _repository.Category.CreateCategory(categoryEntity);
            await _repository.SaveAsync();

            var categoryToReturn = _mapper.Map<CategoryDto>(categoryEntity);

            return categoryToReturn;
        }


        public async Task UpdateCategoryAsync(Guid categoryId,
            CategoryForUpdateDto categoryForUpdate, bool trackChanges)
        {
            var category = await GetCategoryAndCheckIfItExists(categoryId, trackChanges);

            _mapper.Map(categoryForUpdate, category);
            await _repository.SaveAsync();
        }


        public async Task DeleteCategoryAsync(Guid categoryId, bool trackChanges)
        {
            var category = await GetCategoryAndCheckIfItExists(categoryId, trackChanges);

            _repository.Category.DeleteCategory(category);
            await _repository.SaveAsync();
        }

        private async Task<Category> GetCategoryAndCheckIfItExists(Guid id, bool trackChanges)
        {
            var category = await _repository.Category.GetCategoryAsync(id, trackChanges);
            if (category is null)
                throw new CategoryNotFoundException(id);

            return category;
        }
    }
}