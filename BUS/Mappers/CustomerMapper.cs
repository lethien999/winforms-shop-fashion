using WinFormsFashionShop.Data.Entities;
using WinFormsFashionShop.DTO;

namespace WinFormsFashionShop.Business.Mappers
{
    /// <summary>
    /// Mapper for converting between Customer Entity and CustomerDTO
    /// </summary>
    public static class CustomerMapper
    {
        public static CustomerDTO ToDTO(Customer entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
            return new CustomerDTO
            {
                Id = entity.Id,
                CustomerName = entity.CustomerName,
                Phone = entity.Phone,
                Email = entity.Email,
                Address = entity.Address,
                IsActive = entity.IsActive
            };
        }

        public static Customer ToEntity(CreateCustomerDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            
            return new Customer
            {
                CustomerName = dto.CustomerName,
                Phone = dto.Phone,
                Email = dto.Email,
                Address = null, // Address field removed from UI
                IsActive = true,
                CreatedAt = DateTime.Now
            };
        }

        public static Customer ToEntity(UpdateCustomerDTO dto, Customer existingEntity)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));
            
            existingEntity.CustomerName = dto.CustomerName;
            existingEntity.Phone = dto.Phone;
            existingEntity.Email = dto.Email;
            // Address field removed from UI - keep existing value or set to null
            existingEntity.Address = null;
            existingEntity.IsActive = dto.IsActive;
            existingEntity.UpdatedAt = DateTime.Now;
            
            return existingEntity;
        }
    }
}

