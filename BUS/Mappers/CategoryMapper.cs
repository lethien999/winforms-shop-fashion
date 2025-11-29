using WinFormsFashionShop.Data.Entities;
using WinFormsFashionShop.DTO;

namespace WinFormsFashionShop.Business.Mappers
{
    /// <summary>
    /// Mapper for converting between Category Entity and CategoryDTO
    /// </summary>
    public static class CategoryMapper
    {
        public static CategoryDTO ToDTO(Category entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
            return new CategoryDTO
            {
                Id = entity.Id,
                CategoryName = entity.CategoryName,
                Description = entity.Description,
                IsActive = entity.IsActive
            };
        }

        public static Category ToEntity(CreateCategoryDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            
            return new Category
            {
                CategoryName = dto.CategoryName,
                Description = dto.Description,
                IsActive = true
            };
        }

        public static Category ToEntity(UpdateCategoryDTO dto, Category existingEntity)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));
            
            existingEntity.CategoryName = dto.CategoryName;
            existingEntity.Description = dto.Description;
            existingEntity.IsActive = dto.IsActive;
            
            return existingEntity;
        }
    }
}

