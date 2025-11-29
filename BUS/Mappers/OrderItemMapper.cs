using WinFormsFashionShop.Data.Entities;
using WinFormsFashionShop.DTO;

namespace WinFormsFashionShop.Business.Mappers
{
    /// <summary>
    /// Mapper for converting between OrderItem Entity and OrderItemDTO
    /// </summary>
    public static class OrderItemMapper
    {
        public static OrderItemDTO ToDTO(OrderItem entity, string? productName = null, string? productCode = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
            return new OrderItemDTO
            {
                Id = entity.Id,
                OrderId = entity.OrderId,
                ProductId = entity.ProductId,
                ProductName = productName,
                ProductCode = productCode,
                Quantity = entity.Quantity,
                UnitPrice = entity.UnitPrice,
                LineTotal = entity.LineTotal
            };
        }

        public static OrderItem ToEntity(CreateOrderItemDTO dto, int orderId)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            
            return new OrderItem
            {
                OrderId = orderId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice,
                LineTotal = dto.Quantity * dto.UnitPrice
            };
        }
    }
}

