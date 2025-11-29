using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.DTO;

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
            // Setup grid columns
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

            // Wire up event handlers
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

        private void LoadCustomers()
        {
            try
            {
                var customers = _customerService.GetAllCustomers().Where(c => c.IsActive).ToList();
                cmbCustomer.Items.Clear();
                cmbCustomer.Items.Add("-- Chọn khách hàng (tùy chọn) --");
                foreach (var customer in customers)
                {
                    cmbCustomer.Items.Add(customer);
                }
                cmbCustomer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        // Find and select the newly created customer
                        var newCustomer = _customerService.GetAllCustomers()
                            .FirstOrDefault(c => c.CustomerName == dialog.CreateCustomerDTO.CustomerName);
                        if (newCustomer != null)
                        {
                            cmbCustomer.SelectedItem = newCustomer;
                        }
                    }
                    MessageBox.Show("Thêm khách hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi thêm khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AddProductToOrder()
        {
            var searchText = txtProductSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                MessageBox.Show("Vui lòng nhập mã hoặc tên sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var products = _productService.GetAllProducts()
                    .Where(p => p.IsActive && 
                        (p.ProductCode.ToLower().Contains(searchText.ToLower()) || 
                         p.Name.ToLower().Contains(searchText.ToLower())))
                    .ToList();

                if (products.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ProductDTO selectedProduct;
                if (products.Count == 1)
                {
                    selectedProduct = products.First();
                }
                else
                {
                    // Show selection dialog if multiple products found
                    using var dialog = new ProductSelectionDialog(products);
                    if (dialog.ShowDialog() != DialogResult.OK || dialog.SelectedProduct == null)
                        return;
                    selectedProduct = dialog.SelectedProduct;
                }

                // Check if product already in order
                var existingRow = gridOrderItems.Rows.Cast<DataGridViewRow>()
                    .FirstOrDefault(r => r.Cells["ProductCode"].Value?.ToString() == selectedProduct.ProductCode);

                if (existingRow != null)
                {
                    var currentQty = Convert.ToInt32(existingRow.Cells["Quantity"].Value ?? 0);
                    existingRow.Cells["Quantity"].Value = currentQty + 1;
                    UpdateLineTotal(existingRow.Index);
                    CalculateTotal();
                    txtProductSearch.Clear();
                    return;
                }

                // Check stock availability
                var inventory = _inventoryService.GetInventoryByProductId(selectedProduct.Id);
                var stock = inventory?.QuantityInStock ?? 0;

                if (stock <= 0)
                {
                    MessageBox.Show($"Sản phẩm '{selectedProduct.Name}' đã hết hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Add to grid
                var rowIndex = gridOrderItems.Rows.Add();
                var row = gridOrderItems.Rows[rowIndex];
                row.Cells["ProductCode"].Value = selectedProduct.ProductCode;
                row.Cells["ProductName"].Value = selectedProduct.Name;
                row.Cells["Quantity"].Value = 1;
                row.Cells["UnitPrice"].Value = selectedProduct.UnitPrice;
                row.Cells["Stock"].Value = stock;
                row.Cells["Delete"].Value = "Xóa";
                row.Tag = selectedProduct; // Store product DTO for later use

                UpdateLineTotal(rowIndex);
                CalculateTotal();
                txtProductSearch.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateLineTotal(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= gridOrderItems.Rows.Count) return;

            var row = gridOrderItems.Rows[rowIndex];
            if (int.TryParse(row.Cells["Quantity"].Value?.ToString(), out var qty) &&
                decimal.TryParse(row.Cells["UnitPrice"].Value?.ToString(), out var price))
            {
                var stock = Convert.ToInt32(row.Cells["Stock"].Value ?? 0);
                
                if (qty > stock)
                {
                    MessageBox.Show($"Số lượng vượt quá tồn kho ({stock})!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    row.Cells["Quantity"].Value = stock;
                    qty = stock;
                }

                var lineTotal = qty * price;
                row.Cells["LineTotal"].Value = lineTotal;
            }
        }

        private void RemoveOrderItem(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < gridOrderItems.Rows.Count)
            {
                gridOrderItems.Rows.RemoveAt(rowIndex);
                CalculateTotal();
            }
        }

        private void CalculateTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in gridOrderItems.Rows)
            {
                if (decimal.TryParse(row.Cells["LineTotal"].Value?.ToString(), out var lineTotal))
                {
                    total += lineTotal;
                }
            }

            // Apply discount
            if (decimal.TryParse(txtDiscountPercent.Text, out var discountPercent) && discountPercent > 0)
            {
                total = total * (1 - discountPercent / 100);
            }
            else if (decimal.TryParse(txtDiscountAmount.Text, out var discountAmount) && discountAmount > 0)
            {
                total = Math.Max(0, total - discountAmount);
            }

            lblTotal.Text = $"Tổng tiền: {total:N0} VNĐ";
        }

        private void SaveOrder()
        {
            if (gridOrderItems.Rows.Count == 0)
            {
                MessageBox.Show("Hóa đơn phải có ít nhất một sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Build order items
                _orderItems.Clear();
                foreach (DataGridViewRow row in gridOrderItems.Rows)
                {
                    var product = row.Tag as ProductDTO;
                    if (product == null) continue;

                    var qty = Convert.ToInt32(row.Cells["Quantity"].Value ?? 0);
                    var unitPrice = Convert.ToDecimal(row.Cells["UnitPrice"].Value ?? 0);

                    // Check stock one more time
                    if (!_orderService.CheckStockAvailability(product.Id, qty))
                    {
                        MessageBox.Show($"Sản phẩm '{product.Name}' không đủ tồn kho!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    _orderItems.Add(new CreateOrderItemDTO
                    {
                        ProductId = product.Id,
                        Quantity = qty,
                        UnitPrice = unitPrice
                    });
                }

                // Set customer
                int? customerId = null;
                if (cmbCustomer.SelectedIndex > 0 && cmbCustomer.SelectedItem is CustomerDTO customer)
                {
                    customerId = customer.Id;
                }

                // Apply discount
                decimal? discountPercent = null;
                decimal? discountAmount = null;
                if (decimal.TryParse(txtDiscountPercent.Text, out var dp) && dp > 0)
                {
                    discountPercent = dp;
                }
                else if (decimal.TryParse(txtDiscountAmount.Text, out var da) && da > 0)
                {
                    discountAmount = da;
                }

                // Create order using DTO
                var createOrderDTO = new CreateOrderDTO
                {
                    CustomerId = customerId,
                    UserId = _currentUser.Id,
                    Status = "Paid",
                    Items = _orderItems,
                    DiscountPercent = discountPercent,
                    DiscountAmount = discountAmount
                };

                _orderService.CreateOrder(createOrderDTO);

                MessageBox.Show("Lưu hóa đơn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
