using System;
using System.Collections.Generic;
using System.Linq;
using WinFormsFashionShop.Data.Entities;
using WinFormsFashionShop.DTO;

namespace WinFormsFashionShop.Business.Mappers
{
    /// <summary>
    /// Mapper for converting between Order Entity and OrderDTO
    /// </summary>
    public static class OrderMapper
    {
        public static OrderDTO ToDTO(Order entity, string? customerName = null, string? userName = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
            return new OrderDTO
            {
                Id = entity.Id,
                OrderCode = entity.OrderCode,
                OrderDate = entity.OrderDate,
                CustomerId = entity.CustomerId,
                CustomerName = customerName,
                UserId = entity.UserId,
                UserName = userName,
                TotalAmount = entity.TotalAmount,
                Notes = entity.Notes,
                Status = entity.Status,
                Items = entity.Items?.Select(item => OrderItemMapper.ToDTO(item)).ToList() ?? new List<OrderItemDTO>()
            };
        }

        public static Order ToEntity(CreateOrderDTO dto, string orderCode)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            
            var order = new Order
            {
                OrderCode = orderCode,
                OrderDate = DateTime.Now,
                CustomerId = dto.CustomerId,
                UserId = dto.UserId,
                Notes = dto.Notes,
                Status = dto.Status,
                Items = new List<OrderItem>()
            };
            
            // Map items after order is created (OrderId will be set later)
            if (dto.Items != null)
            {
                foreach (var itemDto in dto.Items)
                {
                    order.Items.Add(new OrderItem
                    {
                        ProductId = itemDto.ProductId,
                        Quantity = itemDto.Quantity,
                        UnitPrice = itemDto.UnitPrice,
                        LineTotal = itemDto.Quantity * itemDto.UnitPrice
                    });
                }
            }
            
            return order;
        }

        public static Order ToEntity(UpdateOrderDTO dto, Order existingEntity)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));
            
            existingEntity.CustomerId = dto.CustomerId;
            existingEntity.Notes = dto.Notes;
            existingEntity.Status = dto.Status;
            
            return existingEntity;
        }
    }
}

