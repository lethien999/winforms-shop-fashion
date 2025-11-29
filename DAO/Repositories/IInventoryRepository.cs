using WinFormsFashionShop.Data.Entities;

namespace WinFormsFashionShop.Data.Repositories
{
    public interface IInventoryRepository : IRepositoryBase<Inventory>
    {
        Inventory? GetByProductId(int productId);
    }
}