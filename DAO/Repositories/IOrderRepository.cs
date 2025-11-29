using System;
using System.Collections.Generic;
using WinFormsFashionShop.Data.Entities;

namespace WinFormsFashionShop.Data.Repositories
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        IEnumerable<Order> GetByDateRange(DateTime from, DateTime to);
        IEnumerable<Order> GetByCustomerId(int customerId);
        IEnumerable<Order> GetByUserId(int userId);
    }
}