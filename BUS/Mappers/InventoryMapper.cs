using WinFormsFashionShop.Data.Entities;
using WinFormsFashionShop.DTO;

namespace WinFormsFashionShop.Business.Mappers
{
    /// <summary>
    /// Mapper for converting between Inventory Entity and InventoryDTO
    /// </summary>
    public static class InventoryMapper
    {
        public static InventoryDTO ToDTO(Inventory entity, string? productName = null, string? productCode = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
            return new InventoryDTO
            {
                Id = entity.Id,
                ProductId = entity.ProductId,
                ProductName = productName,
                ProductCode = productCode,
                QuantityInStock = entity.QuantityInStock,
                LastUpdated = entity.LastUpdated
            };
        }
    }
}

