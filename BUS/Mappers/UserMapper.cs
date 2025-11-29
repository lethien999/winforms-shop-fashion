using WinFormsFashionShop.Data.Entities;
using WinFormsFashionShop.DTO;

namespace WinFormsFashionShop.Business.Mappers
{
    /// <summary>
    /// Mapper for converting between User Entity and UserDTO
    /// Note: PasswordHash is never included in DTO for security
    /// </summary>
    public static class UserMapper
    {
        public static UserDTO ToDTO(User entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
            return new UserDTO
            {
                Id = entity.Id,
                Username = entity.Username,
                FullName = entity.FullName,
                Role = entity.Role,
                IsActive = entity.IsActive
            };
        }

        public static User ToEntity(CreateUserDTO dto, string passwordHash)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            
            return new User
            {
                Username = dto.Username,
                PasswordHash = passwordHash, // Should be hashed before calling this
                FullName = dto.FullName,
                Role = dto.Role,
                IsActive = true,
                CreatedAt = DateTime.Now
            };
        }

        public static User ToEntity(UpdateUserDTO dto, User existingEntity, string? newPasswordHash = null)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));
            
            existingEntity.Username = dto.Username;
            if (!string.IsNullOrEmpty(newPasswordHash))
            {
                existingEntity.PasswordHash = newPasswordHash;
            }
            existingEntity.FullName = dto.FullName;
            existingEntity.Role = dto.Role;
            existingEntity.IsActive = dto.IsActive;
            existingEntity.UpdatedAt = DateTime.Now;
            
            return existingEntity;
        }
    }
}

