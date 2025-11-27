using System;
using System.Collections.Generic;
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

        public void UpdateInventory(Inventory inventory)
        {
            if (inventory == null) throw new ArgumentNullException(nameof(inventory));
            if (inventory.Id <= 0) throw new ArgumentException("Inventory Id invalid", nameof(inventory.Id));
            if (inventory.QuantityOnHand < 0) throw new ArgumentException("Quantity cannot be negative", nameof(inventory.QuantityOnHand));
            _inventoryRepository.Update(inventory);
        }

        /// <summary>
        /// Adjust inventory when new stock arrives.
        /// </summary>
        public void AdjustInventoryForNewStock(int productId, int quantity)
        {
            if (productId <= 0) throw new ArgumentOutOfRangeException(nameof(productId));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));

            var existing = FindByProduct(productId);
            if (existing != null)
            {
                existing.QuantityOnHand += quantity;
                _inventoryRepository.Update(existing);
                return;
            }

            var newInventory = new Inventory { ProductId = productId, QuantityOnHand = quantity };
            _inventoryRepository.Insert(newInventory);
        }

        /// <summary>
        /// Decrease inventory when order is placed.
        /// </summary>
        public void DecreaseInventoryForOrder(int productId, int quantity)
        {
            if (productId <= 0) throw new ArgumentOutOfRangeException(nameof(productId));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));

            var existing = FindByProduct(productId) ?? throw new InvalidOperationException("Inventory not found");
            if (existing.QuantityOnHand < quantity) throw new InvalidOperationException("Insufficient stock");
            existing.QuantityOnHand -= quantity;
            _inventoryRepository.Update(existing);
        }

        private Inventory? FindByProduct(int productId)
        {
            foreach (var inv in _inventoryRepository.GetAll())
            {
                if (inv.ProductId == productId) return inv;
            }
            return null;
        }
    }
}