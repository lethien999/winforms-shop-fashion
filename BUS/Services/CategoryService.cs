using System;
using System.Collections.Generic;
using System.Linq;
using WinFormsFashionShop.Business.Mappers;
using WinFormsFashionShop.Data.Repositories;
using WinFormsFashionShop.DTO;

namespace WinFormsFashionShop.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            return _categoryRepository.GetAll().Select(CategoryMapper.ToDTO);
        }

        public CategoryDTO? GetCategoryById(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            var category = _categoryRepository.GetById(id);
            return category != null ? CategoryMapper.ToDTO(category) : null;
        }

        public void CreateCategory(CreateCategoryDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(dto.CategoryName))
                throw new ArgumentException("Category name is required", nameof(dto));
            
            var category = CategoryMapper.ToEntity(dto);
            _categoryRepository.Insert(category);
        }

        public void UpdateCategory(UpdateCategoryDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (dto.Id <= 0) throw new ArgumentException("Category Id invalid", nameof(dto));
            if (string.IsNullOrWhiteSpace(dto.CategoryName))
                throw new ArgumentException("Category name is required", nameof(dto));
            
            var existingCategory = _categoryRepository.GetById(dto.Id)
                ?? throw new InvalidOperationException("Category not found");
            
            var updatedCategory = CategoryMapper.ToEntity(dto, existingCategory);
            _categoryRepository.Update(updatedCategory);
        }

        public void DeleteCategory(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            _categoryRepository.Delete(id);
        }

        public IEnumerable<CategoryDTO> GetActiveCategories()
        {
            return _categoryRepository.GetAll()
                .Where(c => c.IsActive)
                .Select(CategoryMapper.ToDTO);
        }
    }
}

