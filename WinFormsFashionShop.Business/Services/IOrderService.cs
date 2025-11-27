using System;
using System.Collections.Generic;
using WinFormsFashionShop.Data.Entities;

namespace WinFormsFashionShop.Business.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();
        Order? GetOrderById(int id);
        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int id);
        IEnumerable<Order> GetOrdersByDateRange(DateTime from, DateTime to);
    }
}