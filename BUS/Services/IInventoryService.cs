using System.Collections.Generic;
using WinFormsFashionShop.Data.Entities;

namespace WinFormsFashionShop.Business.Services
{
    public interface IInventoryService
    {
        IEnumerable<Inventory> GetAllInventories();
        Inventory? GetInventoryById(int id);
        Inventory? GetInventoryByProductId(int productId);
        void UpdateInventory(Inventory inventory);
        void AdjustInventoryForNewStock(int productId, int quantity);
        void DecreaseInventoryForOrder(int productId, int quantity);
        void IncreaseStock(int productId, int quantity);
        void DecreaseStock(int productId, int quantity);
        bool CheckStockAvailability(int productId, int quantity);
        IEnumerable<Inventory> GetLowStock(int threshold = 10);
    }
}