using System;
using System.Collections.Generic;
using WinFormsFashionShop.DTO;

namespace WinFormsFashionShop.Business.Services
{
    public interface IOrderService
    {
        IEnumerable<OrderDTO> GetAllOrders();
        OrderDTO? GetOrderById(int id);
        OrderDTO CreateOrder(CreateOrderDTO order);
        void UpdateOrder(UpdateOrderDTO order);
        void DeleteOrder(int id);
        IEnumerable<OrderDTO> GetOrdersByDateRange(DateTime from, DateTime to);
        IEnumerable<OrderDTO> GetOrdersByCustomerId(int customerId);
        IEnumerable<OrderDTO> GetOrdersByUserId(int userId);
        bool CheckStockAvailability(int productId, int quantity);
        string GenerateOrderCode();
        void CancelOrder(int orderId);
    }
}