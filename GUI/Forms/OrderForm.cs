using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Constants;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.DTO;
using WinFormsFashionShop.Presentation.Helpers;

namespace WinFormsFashionShop.Presentation.Forms
{
    public partial class OrderForm : Form
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly IInventoryService _inventoryService;
        private readonly UserDTO _currentUser;

        private List<CreateOrderItemDTO> _orderItems = new List<CreateOrderItemDTO>();

        public OrderForm(IOrderService orderService, IProductService productService, 
            ICustomerService customerService, IInventoryService inventoryService, UserDTO currentUser)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _inventoryService = inventoryService ?? throw new ArgumentNullException(nameof(inventoryService));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

            InitializeComponent();
            InitializeControls();
            LoadCustomers();
        }

        private void InitializeControls()
        {
            SetupGridColumns();
            SetupPaymentMethodComboBox();
            WireUpEventHandlers();
        }

        /// <summary>
        /// Sets up grid columns for order items.
        /// Single responsibility: only configures grid columns.
        /// </summary>
        private void SetupGridColumns()
        {
            gridOrderItems.Columns.Add("ProductCode", "Mã SP");
            gridOrderItems.Columns.Add("ProductName", "Tên SP");
            gridOrderItems.Columns.Add("Quantity", "Số lượng");
            gridOrderItems.Columns.Add("UnitPrice", "Đơn giá");
            gridOrderItems.Columns.Add("LineTotal", "Thành tiền");
            gridOrderItems.Columns.Add("Stock", "Tồn kho");
            gridOrderItems.Columns.Add("Delete", "Xóa");
            
            foreach (DataGridViewColumn col in gridOrderItems.Columns)
            {
                if (col.Name != "Delete" && col.Name != "Quantity")
                    col.ReadOnly = true;
            }
        }

        /// <summary>
        /// Sets up payment method ComboBox with available options.
        /// Single responsibility: only configures payment method ComboBox.
        /// </summary>
        private void SetupPaymentMethodComboBox()
        {
            cmbPaymentMethod.Items.Add(PaymentMethod.Cash);
            cmbPaymentMethod.Items.Add(PaymentMethod.Card);
            cmbPaymentMethod.Items.Add(PaymentMethod.Transfer);
            cmbPaymentMethod.Items.Add(PaymentMethod.Other);
            cmbPaymentMethod.SelectedIndex = 0; // Default to Cash
        }

        /// <summary>
        /// Wires up all event handlers.
        /// Single responsibility: only sets up event handlers.
        /// </summary>
        private void WireUpEventHandlers()
        {
            btnNewCustomer.Click += (s, e) => AddNewCustomer();
            btnAddProduct.Click += (s, e) => AddProductToOrder();
            txtProductSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) AddProductToOrder(); };
            
            gridOrderItems.CellValueChanged += (s, e) =>
            {
                if (e.ColumnIndex == gridOrderItems.Columns["Quantity"].Index)
                {
                    UpdateLineTotal(e.RowIndex);
                    CalculateTotal();
                }
            };

            gridOrderItems.CellContentClick += (s, e) =>
            {
                if (e.ColumnIndex == gridOrderItems.Columns["Delete"].Index)
                {
                    RemoveOrderItem(e.RowIndex);
                }
            };

            txtDiscountPercent.TextChanged += (s, e) => CalculateTotal();
            txtDiscountAmount.TextChanged += (s, e) => CalculateTotal();
            btnSaveOrder.Click += (s, e) => SaveOrder();
            btnCancel.Click += (s, e) => Close();
        }

        /// <summary>
        /// Loads customers into the ComboBox with proper display.
        /// Single responsibility: only loads and configures customers.
        /// </summary>
        private void LoadCustomers()
        {
            try
            {
                var customers = _customerService.GetAllCustomers().Where(c => c.IsActive).ToList();
                cmbCustomer.Items.Clear();
                
                // Set display member to show CustomerName instead of object type
                cmbCustomer.DisplayMember = "CustomerName";
                cmbCustomer.ValueMember = "Id";
                
                cmbCustomer.Items.Add("-- Chọn khách hàng (tùy chọn) --");
                foreach (var customer in customers)
                {
                    cmbCustomer.Items.Add(customer);
                }
                cmbCustomer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex);
            }
        }

        private void AddNewCustomer()
        {
            using var dialog = new CustomerEditDialog(null);
            if (dialog.ShowDialog() == DialogResult.OK && dialog.CustomerDTO != null)
            {
                try
                {
                    if (dialog.CreateCustomerDTO != null)
                    {
                        _customerService.CreateCustomer(dialog.CreateCustomerDTO);
                        LoadCustomers();
                        SelectNewlyCreatedCustomer(dialog.CreateCustomerDTO.CustomerName);
                    }
                    ErrorHandler.ShowSuccess("Thêm khách hàng thành công!");
                }
                catch (Exception ex)
                {
                    ErrorHandler.ShowError(ex);
                }
            }
        }

        /// <summary>
        /// Finds and selects the newly created customer in the combo box.
        /// Single responsibility: only handles customer selection.
        /// </summary>
        private void SelectNewlyCreatedCustomer(string customerName)
        {
            var newCustomer = _customerService.GetAllCustomers()
                .FirstOrDefault(c => c.CustomerName == customerName);
            if (newCustomer != null)
            {
                cmbCustomer.SelectedItem = newCustomer;
            }
        }

        /// <summary>
        /// Main method to add a product to the order.
        /// Orchestrates the flow but delegates to smaller methods.
        /// </summary>
        private void AddProductToOrder()
        {
            var searchText = ValidateProductSearchInput();
            if (searchText == null) return;

            try
            {
                var products = SearchProducts(searchText);
                if (products == null || products.Count == 0) return;

                var selectedProduct = SelectProductFromResults(products);
                if (selectedProduct == null) return;

                if (TryIncrementExistingProduct(selectedProduct))
                {
                    txtProductSearch.Clear();
                    return;
                }

                if (!ValidateStockAvailability(selectedProduct, out var stock))
                {
                    return;
                }

                AddProductRowToGrid(selectedProduct, stock);
                txtProductSearch.Clear();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex);
            }
        }

        /// <summary>
        /// Validates the product search input.
        /// Single responsibility: only validates input.
        /// </summary>
        private string? ValidateProductSearchInput()
        {
            var searchText = txtProductSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                ErrorHandler.ShowWarning("Vui lòng nhập mã hoặc tên sản phẩm!");
                return null;
            }
            return searchText;
        }

        /// <summary>
        /// Searches for products matching the search text.
        /// Single responsibility: only performs product search.
        /// </summary>
        private List<ProductDTO>? SearchProducts(string searchText)
        {
            var products = _productService.GetAllProducts()
                .Where(p => p.IsActive && 
                    (p.ProductCode.ToLower().Contains(searchText.ToLower()) || 
                     p.Name.ToLower().Contains(searchText.ToLower())))
                .ToList();

            if (products.Count == 0)
            {
                ErrorHandler.ShowWarning("Không tìm thấy sản phẩm!");
                return null;
            }

            return products;
        }

        /// <summary>
        /// Selects a product from search results, showing dialog if multiple matches.
        /// Single responsibility: only handles product selection.
        /// </summary>
        private ProductDTO? SelectProductFromResults(List<ProductDTO> products)
        {
            if (products.Count == 1)
            {
                return products.First();
            }

            // Show selection dialog if multiple products found
            using var dialog = new ProductSelectionDialog(products);
            if (dialog.ShowDialog() != DialogResult.OK || dialog.SelectedProduct == null)
                return null;

            return dialog.SelectedProduct;
        }

        /// <summary>
        /// Checks if product already exists in order and increments quantity if found.
        /// Single responsibility: only handles existing product increment.
        /// </summary>
        private bool TryIncrementExistingProduct(ProductDTO product)
        {
            var existingRow = gridOrderItems.Rows.Cast<DataGridViewRow>()
                .FirstOrDefault(r => r.Cells["ProductCode"].Value?.ToString() == product.ProductCode);

            if (existingRow != null)
            {
                var currentQty = Convert.ToInt32(existingRow.Cells["Quantity"].Value ?? 0);
                existingRow.Cells["Quantity"].Value = currentQty + 1;
                UpdateLineTotal(existingRow.Index);
                CalculateTotal();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Validates stock availability for a product.
        /// Single responsibility: only validates stock.
        /// </summary>
        private bool ValidateStockAvailability(ProductDTO product, out int stock)
        {
            stock = 0;
            var inventory = _inventoryService.GetInventoryByProductId(product.Id);
            stock = inventory?.QuantityInStock ?? 0;

            if (stock <= 0)
            {
                ErrorHandler.ShowWarning($"Sản phẩm '{product.Name}' đã hết hàng!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Adds a product row to the order grid.
        /// Single responsibility: only adds row to grid.
        /// </summary>
        private void AddProductRowToGrid(ProductDTO product, int stock)
        {
            var rowIndex = gridOrderItems.Rows.Add();
            var row = gridOrderItems.Rows[rowIndex];
            row.Cells["ProductCode"].Value = product.ProductCode;
            row.Cells["ProductName"].Value = product.Name;
            row.Cells["Quantity"].Value = 1;
            row.Cells["UnitPrice"].Value = product.UnitPrice;
            row.Cells["Stock"].Value = stock;
            row.Cells["Delete"].Value = "Xóa";
            row.Tag = product; // Store product DTO for later use

            UpdateLineTotal(rowIndex);
            CalculateTotal();
        }

        /// <summary>
        /// Updates the line total for a specific row.
        /// Single responsibility: only calculates and updates line total.
        /// </summary>
        private void UpdateLineTotal(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= gridOrderItems.Rows.Count) return;

            var row = gridOrderItems.Rows[rowIndex];
            if (!TryParseRowValues(row, out var qty, out var price, out var stock))
                return;

            qty = EnforceStockLimit(row, qty, stock);
            var lineTotal = qty * price;
            row.Cells["LineTotal"].Value = lineTotal;
        }

        /// <summary>
        /// Parses quantity, price, and stock from a grid row.
        /// Single responsibility: only parses values.
        /// </summary>
        private bool TryParseRowValues(DataGridViewRow row, out int qty, out decimal price, out int stock)
        {
            qty = 0;
            price = 0;
            stock = 0;

            if (!int.TryParse(row.Cells["Quantity"].Value?.ToString(), out qty) ||
                !decimal.TryParse(row.Cells["UnitPrice"].Value?.ToString(), out price))
            {
                return false;
            }

            stock = Convert.ToInt32(row.Cells["Stock"].Value ?? 0);
            return true;
        }

        /// <summary>
        /// Enforces stock limit and adjusts quantity if needed.
        /// Single responsibility: only enforces stock limits.
        /// </summary>
        private int EnforceStockLimit(DataGridViewRow row, int qty, int stock)
        {
            if (qty > stock)
            {
                ErrorHandler.ShowWarning($"Số lượng vượt quá tồn kho ({stock})!");
                row.Cells["Quantity"].Value = stock;
                return stock;
            }
            return qty;
        }

        private void RemoveOrderItem(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < gridOrderItems.Rows.Count)
            {
                gridOrderItems.Rows.RemoveAt(rowIndex);
                CalculateTotal();
            }
        }

        /// <summary>
        /// Calculates and displays the order total including discounts.
        /// Single responsibility: only calculates and displays total.
        /// </summary>
        private void CalculateTotal()
        {
            var subtotal = CalculateSubtotal();
            var total = ApplyDiscount(subtotal);
            DisplayTotal(total);
        }

        /// <summary>
        /// Calculates subtotal from all order items.
        /// Single responsibility: only calculates subtotal.
        /// </summary>
        private decimal CalculateSubtotal()
        {
            decimal subtotal = 0;
            foreach (DataGridViewRow row in gridOrderItems.Rows)
            {
                if (decimal.TryParse(row.Cells["LineTotal"].Value?.ToString(), out var lineTotal))
                {
                    subtotal += lineTotal;
                }
            }
            return subtotal;
        }

        /// <summary>
        /// Applies discount to the subtotal.
        /// Single responsibility: only applies discount.
        /// </summary>
        private decimal ApplyDiscount(decimal subtotal)
        {
            if (decimal.TryParse(txtDiscountPercent.Text, out var discountPercent) && discountPercent > 0)
            {
                return subtotal * (1 - discountPercent / 100);
            }
            
            if (decimal.TryParse(txtDiscountAmount.Text, out var discountAmount) && discountAmount > 0)
            {
                return Math.Max(0, subtotal - discountAmount);
            }

            return subtotal;
        }

        /// <summary>
        /// Displays the total amount in the label.
        /// Single responsibility: only updates UI.
        /// </summary>
        private void DisplayTotal(decimal total)
        {
            lblTotal.Text = $"Tổng tiền: {total:N0} VNĐ";
        }

        /// <summary>
        /// Main method to save the order.
        /// Orchestrates the flow but delegates to smaller methods.
        /// </summary>
        private void SaveOrder()
        {
            if (!ValidateOrderBeforeSave())
                return;

            try
            {
                var orderItems = BuildOrderItemsFromGrid();
                if (orderItems == null)
                    return;

                var customerId = GetSelectedCustomerId();
                var (discountPercent, discountAmount) = ParseDiscountFromInputs();
                var createOrderDTO = CreateOrderDTO(orderItems, customerId, discountPercent, discountAmount);

                SaveOrderToDatabase(createOrderDTO);
                ErrorHandler.ShowSuccess("Lưu hóa đơn thành công!");
                Close();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex);
            }
        }

        /// <summary>
        /// Validates that the order has at least one item.
        /// Single responsibility: only validates order.
        /// </summary>
        private bool ValidateOrderBeforeSave()
        {
            if (gridOrderItems.Rows.Count == 0)
            {
                ErrorHandler.ShowWarning("Hóa đơn phải có ít nhất một sản phẩm!");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Builds order items from the grid, validating stock for each.
        /// Single responsibility: only builds order items.
        /// </summary>
        private List<CreateOrderItemDTO>? BuildOrderItemsFromGrid()
        {
            _orderItems.Clear();
            foreach (DataGridViewRow row in gridOrderItems.Rows)
            {
                var product = row.Tag as ProductDTO;
                if (product == null) continue;

                var qty = Convert.ToInt32(row.Cells["Quantity"].Value ?? 0);
                var unitPrice = Convert.ToDecimal(row.Cells["UnitPrice"].Value ?? 0);

                if (!ValidateStockBeforeSave(product, qty))
                    return null;

                _orderItems.Add(new CreateOrderItemDTO
                {
                    ProductId = product.Id,
                    Quantity = qty,
                    UnitPrice = unitPrice
                });
            }
            return _orderItems;
        }

        /// <summary>
        /// Validates stock availability before saving order.
        /// Single responsibility: only validates stock.
        /// </summary>
        private bool ValidateStockBeforeSave(ProductDTO product, int quantity)
        {
            if (!_orderService.CheckStockAvailability(product.Id, quantity))
            {
                ErrorHandler.ShowError($"Sản phẩm '{product.Name}' không đủ tồn kho!");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Gets the selected customer ID from the combo box.
        /// Single responsibility: only extracts customer ID.
        /// </summary>
        private int? GetSelectedCustomerId()
        {
            if (cmbCustomer.SelectedIndex > 0 && cmbCustomer.SelectedItem is CustomerDTO customer)
            {
                return customer.Id;
            }
            return null;
        }

        /// <summary>
        /// Parses discount values from input fields.
        /// Single responsibility: only parses discount inputs.
        /// </summary>
        private (decimal? discountPercent, decimal? discountAmount) ParseDiscountFromInputs()
        {
            if (decimal.TryParse(txtDiscountPercent.Text, out var dp) && dp > 0)
            {
                return (dp, null);
            }

            if (decimal.TryParse(txtDiscountAmount.Text, out var da) && da > 0)
            {
                return (null, da);
            }

            return (null, null);
        }

        /// <summary>
        /// Creates the order DTO from collected data.
        /// Single responsibility: only creates DTO.
        /// </summary>
        private CreateOrderDTO CreateOrderDTO(
            List<CreateOrderItemDTO> items, 
            int? customerId, 
            decimal? discountPercent, 
            decimal? discountAmount)
        {
            return new CreateOrderDTO
            {
                CustomerId = customerId,
                UserId = _currentUser.Id,
                PaymentMethod = GetSelectedPaymentMethod(),
                Status = OrderStatus.Paid,
                Items = items,
                DiscountPercent = discountPercent,
                DiscountAmount = discountAmount
            };
        }

        /// <summary>
        /// Gets the selected payment method from ComboBox.
        /// Single responsibility: only retrieves selected payment method.
        /// </summary>
        private string? GetSelectedPaymentMethod()
        {
            if (cmbPaymentMethod.SelectedItem != null)
            {
                return cmbPaymentMethod.SelectedItem.ToString();
            }
            return PaymentMethod.Cash; // Default to Cash
        }

        /// <summary>
        /// Saves the order to the database.
        /// Single responsibility: only saves to database.
        /// </summary>
        private void SaveOrderToDatabase(CreateOrderDTO createOrderDTO)
        {
            _orderService.CreateOrder(createOrderDTO);
        }
    }

    // Dialog for selecting product when multiple matches found
    public class ProductSelectionDialog : Form
    {
        private DataGridView _grid;
        public ProductDTO? SelectedProduct { get; private set; }

        public ProductSelectionDialog(List<ProductDTO> products)
        {
            Text = "Chọn sản phẩm";
            Width = 600;
            Height = 400;
            StartPosition = FormStartPosition.CenterParent;

            _grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true,
                AllowUserToAddRows = false
            };
            _grid.DataSource = products.Select(p => new { p.Id, p.ProductCode, p.Name, p.UnitPrice }).ToList();
            _grid.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    var productId = (int)_grid.Rows[e.RowIndex].Cells["Id"].Value;
                    SelectedProduct = products.First(p => p.Id == productId);
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };

            var btnOK = new Button { Text = "Chọn", Dock = DockStyle.Bottom, Height = 40, DialogResult = DialogResult.OK };
            btnOK.Click += (s, e) =>
            {
                if (_grid.SelectedRows.Count > 0)
                {
                    var productId = (int)_grid.SelectedRows[0].Cells["Id"].Value;
                    SelectedProduct = products.First(p => p.Id == productId);
                }
            };

            Controls.Add(_grid);
            Controls.Add(btnOK);
        }
    }
}
