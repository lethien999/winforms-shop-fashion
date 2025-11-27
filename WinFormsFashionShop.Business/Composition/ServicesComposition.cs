using WinFormsFashionShop.Data.Repositories;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.Data;

namespace WinFormsFashionShop.Business.Composition
{
    /// <summary>
    /// Composition root living in the Business layer. Responsible only for wiring concrete
    /// repository implementations (Data layer) to service implementations (Business layer).
    /// </summary>
    public static class ServicesComposition
    {
        /// <summary>
        /// Creates and returns a ServiceRegistry containing instantiated service implementations.
        /// Single responsibility: wiring dependencies. Keep this method small and explicit.
        /// </summary>
        public static ServiceRegistry Create()
        {
            // Instantiate repositories (concrete types from Data project)
            var userRepo = new UserRepository();
            var productRepo = new ProductRepository();
            var categoryRepo = new CategoryRepository();
            var customerRepo = new CustomerRepository();
            var orderRepo = new OrderRepository();
            var orderItemRepo = new OrderItemRepository();
            var inventoryRepo = new InventoryRepository();

            // Instantiate services (Business layer) using constructor injection
            var authService = new AuthService(userRepo);
            var productService = new ProductService(productRepo);
            var customerService = new CustomerService(customerRepo);
            var inventoryService = new InventoryService(inventoryRepo);
            var orderService = new OrderService(orderRepo, orderItemRepo, inventoryService);

            return new ServiceRegistry
            {
                AuthService = authService,
                ProductService = productService,
                CustomerService = customerService,
                OrderService = orderService,
                InventoryService = inventoryService
            };
        }
    }

    /// <summary>
    /// Simple DTO-like registry that exposes common service interfaces to the Presentation layer.
    /// Presentation should depend on this registry and service interfaces only.
    /// </summary>
    public class ServiceRegistry
    {
        public IAuthService AuthService { get; init; } = null!;
        public IProductService ProductService { get; init; } = null!;
        public ICustomerService CustomerService { get; init; } = null!;
        public IOrderService OrderService { get; init; } = null!;
        public IInventoryService InventoryService { get; init; } = null!;
    }
}