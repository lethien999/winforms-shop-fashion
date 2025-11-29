using WinFormsFashionShop.Data.Entities;
using WinFormsFashionShop.DTO;

namespace WinFormsFashionShop.Business.Mappers
{
    /// <summary>
    /// Mapper for converting between Product Entity and ProductDTO
    /// </summary>
    public static class ProductMapper
    {
        public static ProductDTO ToDTO(Product entity, string? categoryName = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
            return new ProductDTO
            {
                Id = entity.Id,
                ProductCode = entity.ProductCode,
                Name = entity.Name,
                CategoryId = entity.CategoryId,
                CategoryName = categoryName,
                UnitPrice = entity.UnitPrice,
                Unit = entity.Unit,
                IsActive = entity.IsActive
            };
        }

        public static Product ToEntity(CreateProductDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            
            return new Product
            {
                ProductCode = dto.ProductCode,
                Name = dto.Name,
                CategoryId = dto.CategoryId,
                UnitPrice = dto.UnitPrice,
                Unit = dto.Unit,
                IsActive = true,
                CreatedAt = DateTime.Now
            };
        }

        public static Product ToEntity(UpdateProductDTO dto, Product existingEntity)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));
            
            existingEntity.ProductCode = dto.ProductCode;
            existingEntity.Name = dto.Name;
            existingEntity.CategoryId = dto.CategoryId;
            existingEntity.UnitPrice = dto.UnitPrice;
            existingEntity.Unit = dto.Unit;
            existingEntity.IsActive = dto.IsActive;
            existingEntity.UpdatedAt = DateTime.Now;
            
            return existingEntity;
        }
    }
}

