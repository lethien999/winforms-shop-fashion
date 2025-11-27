using System.Collections.Generic;
using WinFormsFashionShop.Data.Entities;

namespace WinFormsFashionShop.Business.Services
{
    public interface IInventoryService
    {
        IEnumerable<Inventory> GetAllInventories();
        Inventory? GetInventoryById(int id);
        void UpdateInventory(Inventory inventory);
        void AdjustInventoryForNewStock(int productId, int quantity);
        void DecreaseInventoryForOrder(int productId, int quantity);
    }
}