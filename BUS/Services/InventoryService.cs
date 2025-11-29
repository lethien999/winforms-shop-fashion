using System;
using System.Collections.Generic;
using System.Linq;
using WinFormsFashionShop.Data.Entities;
using WinFormsFashionShop.Data.Repositories;

namespace WinFormsFashionShop.Business.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public IEnumerable<Inventory> GetAllInventories()
        {
            return _inventoryRepository.GetAll();
        }

        public Inventory? GetInventoryById(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            return _inventoryRepository.GetById(id);
        }

        public Inventory? GetInventoryByProductId(int productId)
        {
            if (productId <= 0) throw new ArgumentOutOfRangeException(nameof(productId));
            return _inventoryRepository.GetByProductId(productId);
        }

        public void UpdateInventory(Inventory inventory)
        {
            if (inventory == null) throw new ArgumentNullException(nameof(inventory));
            if (inventory.Id <= 0) throw new ArgumentException("Inventory Id invalid", nameof(inventory.Id));
            if (inventory.QuantityInStock < 0) throw new ArgumentException("Quantity cannot be negative", nameof(inventory.QuantityInStock));
            
            inventory.LastUpdated = DateTime.Now;
            _inventoryRepository.Update(inventory);
        }

        /// <summary>
        /// Adjust inventory when new stock arrives.
        /// </summary>
        public void AdjustInventoryForNewStock(int productId, int quantity)
        {
            IncreaseStock(productId, quantity);
        }

        /// <summary>
        /// Decrease inventory when order is placed.
        /// </summary>
        public void DecreaseInventoryForOrder(int productId, int quantity)
        {
            DecreaseStock(productId, quantity);
        }

        /// <summary>
        /// Increase stock for a product.
        /// </summary>
        public void IncreaseStock(int productId, int quantity)
        {
            if (productId <= 0) throw new ArgumentOutOfRangeException(nameof(productId));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));

            var existing = _inventoryRepository.GetByProductId(productId);
            if (existing != null)
            {
                existing.QuantityInStock += quantity;
                existing.LastUpdated = DateTime.Now;
                _inventoryRepository.Update(existing);
                return;
            }

            var newInventory = new Inventory 
            { 
                ProductId = productId, 
                QuantityInStock = quantity,
                LastUpdated = DateTime.Now
            };
            _inventoryRepository.Insert(newInventory);
        }

        /// <summary>
        /// Decrease stock for a product.
        /// </summary>
        public void DecreaseStock(int productId, int quantity)
        {
            if (productId <= 0) throw new ArgumentOutOfRangeException(nameof(productId));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));

            var existing = _inventoryRepository.GetByProductId(productId) 
                ?? throw new InvalidOperationException($"Inventory not found for product ID {productId}");
            
            if (existing.QuantityInStock < quantity) 
                throw new InvalidOperationException($"Insufficient stock. Available: {existing.QuantityInStock}, Requested: {quantity}");
            
            existing.QuantityInStock -= quantity;
            existing.LastUpdated = DateTime.Now;
            _inventoryRepository.Update(existing);
        }

        /// <summary>
        /// Check if there is enough stock for a product.
        /// </summary>
        public bool CheckStockAvailability(int productId, int quantity)
        {
            if (productId <= 0) throw new ArgumentOutOfRangeException(nameof(productId));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));

            var inventory = _inventoryRepository.GetByProductId(productId);
            return inventory != null && inventory.QuantityInStock >= quantity;
        }

        /// <summary>
        /// Get inventory items with stock below threshold.
        /// </summary>
        public IEnumerable<Inventory> GetLowStock(int threshold = 10)
        {
            if (threshold < 0) throw new ArgumentOutOfRangeException(nameof(threshold));
            
            var allInventories = _inventoryRepository.GetAll();
            var lowStockItems = new List<Inventory>();
            foreach (var inventory in allInventories)
            {
                if (inventory.QuantityInStock < threshold)
                {
                    lowStockItems.Add(inventory);
                }
            }
            return lowStockItems;
        }
    }
}