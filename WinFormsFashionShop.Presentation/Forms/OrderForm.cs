using System;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.Data.Entities;
using System.Collections.Generic;

namespace WinFormsFashionShop.Presentation.Forms
{
    public class OrderForm : Form
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly IInventoryService _inventoryService;

        private DataGridView _gridOrders = new DataGridView { Dock = DockStyle.Top, Height = 300 };
        private Button _btnNewOrder = new Button { Text = "New Order" };

        public OrderForm(IOrderService orderService, IProductService productService, ICustomerService customerService, IInventoryService inventoryService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _inventoryService = inventoryService ?? throw new ArgumentNullException(nameof(inventoryService));

            Text = "Orders";
            Width = 1000;
            Height = 700;
            InitializeControls();
            LoadOrdersIntoGrid();
        }

        private void InitializeControls()
        {
            _btnNewOrder.Top = 320; _btnNewOrder.Left = 20; _btnNewOrder.Click += OnNewOrderClicked;
            Controls.Add(_gridOrders);
            Controls.Add(_btnNewOrder);
        }

        private void OnNewOrderClicked(object? sender, EventArgs e)
        {
            // Open a dialog to create an order. Keep this method small and delegate to services.
            var order = new Order
            {
                CustomerId = 1,
                OrderDate = DateTime.Now,
                Items = new List<OrderItem>
                {
                    new OrderItem { ProductId = 1, Quantity = 1, UnitPrice = 100 }
                }
            };

            try
            {
                _orderService.CreateOrder(order);
                _inventoryService.DecreaseInventoryForOrder(1, 1);
                LoadOrdersIntoGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating order: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {

        }

        private void LoadOrdersIntoGrid()
        {
            try
            {
                var orders = _orderService.GetAllOrders();
                var list = new List<Order>(orders);
                _gridOrders.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load orders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}