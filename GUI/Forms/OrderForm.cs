using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Constants;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.DTO;
using WinFormsFashionShop.Presentation.Helpers;
using UIThemeConstants = WinFormsFashionShop.Presentation.Helpers.UIThemeConstants;

namespace WinFormsFashionShop.Presentation.Forms
{
    public partial class OrderForm : Form
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly IInventoryService _inventoryService;
        private readonly ICategoryService _categoryService;
        private readonly IErrorHandler _errorHandler;
        private readonly UserDTO _currentUser;

        private List<CreateOrderItemDTO> _orderItems = new List<CreateOrderItemDTO>();
        private DateTime _lastKeyPressTime = DateTime.MinValue;
        private const int BarcodeScanThresholdMs = 100; // Nếu input trong vòng 100ms thì coi như barcode scan

        public OrderForm(IOrderService orderService, IProductService productService, 
            ICustomerService customerService, IInventoryService inventoryService, 
            ICategoryService categoryService, IErrorHandler errorHandler, UserDTO currentUser)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _inventoryService = inventoryService ?? throw new ArgumentNullException(nameof(inventoryService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

            InitializeComponent();
            InitializeControls();
            LoadCustomers();
            
            // Enable keyboard shortcuts
            this.KeyPreview = true;
            this.KeyDown += OrderForm_KeyDown;
        }

        /// <summary>
        /// Handles keyboard shortcuts for POS workflow
        /// F12 = Save/Pay, Escape = Cancel, F1 = Focus search
        /// </summary>
        private void OrderForm_KeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F12:
                    // Quick pay shortcut
                    btnSaveOrder.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    // Cancel/Close
                    if (string.IsNullOrEmpty(txtProductSearch.Text))
                    {
                        btnCancel.PerformClick();
                    }
                    else
                    {
                        txtProductSearch.Clear();
                        txtProductSearch.Focus();
                    }
                    e.Handled = true;
                    break;
                case Keys.F1:
                    // Focus product search
                    txtProductSearch.Focus();
                    txtProductSearch.SelectAll();
                    e.Handled = true;
                    break;
                case Keys.F2:
                    // Focus customer
                    cmbCustomer.Focus();
                    e.Handled = true;
                    break;
                case Keys.F3:
                    // Open product selection dialog
                    OpenProductSelectionDialog();
                    e.Handled = true;
                    break;
            }
        }

        private void InitializeControls()
        {
            // Load logo if available
            var logo = LogoHelper.LoadLogo(UIThemeConstants.Spacing.LogoSizeMedium);
            if (logo != null && picLogo != null)
            {
                picLogo.Image = logo;
            }
            else if (picLogo != null)
            {
                picLogo.Visible = false;
            }

            // Apply grid styling
            UIThemeConstants.ThemeHelper.ApplyGridStyle(gridOrderItems);

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
            
            // Add VietQR option if PayOS is configured
            if (Helpers.PayOSConfig.IsConfigured())
            {
                cmbPaymentMethod.Items.Add(PaymentMethod.VietQR);
            }
            
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
            btnAddProduct.Click += (s, e) => OpenProductSelectionDialog();
            
            // Setup AutoComplete for product search
            SetupProductSearchAutoComplete();
            
            txtProductSearch.KeyPress += (s, e) =>
            {
                // Track timing for barcode scanner detection
                var now = DateTime.Now;
                if (_lastKeyPressTime != DateTime.MinValue)
                {
                    var timeDiff = (now - _lastKeyPressTime).TotalMilliseconds;
                    // If characters come very quickly (less than threshold), likely barcode scanner
                    // Timer logic removed - using direct timing detection instead
                }
                _lastKeyPressTime = now;
            };
            
            txtProductSearch.KeyDown += (s, e) => 
            { 
                if (e.KeyCode == Keys.Enter) 
                {
                    // If text is entered, use quick search, otherwise open dialog
                    if (!string.IsNullOrWhiteSpace(txtProductSearch.Text))
                    {
                        // Detect if this might be from barcode scanner
                        // Barcode scanners: fast input + Enter immediately after
                        var isLikelyBarcode = (_lastKeyPressTime != DateTime.MinValue && 
                            (DateTime.Now - _lastKeyPressTime).TotalMilliseconds < BarcodeScanThresholdMs * 2);
                        
                        AddProductToOrder(isBarcodeScan: isLikelyBarcode);
                        _lastKeyPressTime = DateTime.MinValue; // Reset
                    }
                    else
                    {
                        OpenProductSelectionDialog();
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    // Clear search on Escape
                    txtProductSearch.Clear();
                    txtProductSearch.Focus();
                }
            };
            
            // Text changed event for live search suggestions
            txtProductSearch.TextChanged += (s, e) => HandleProductSearchTextChanged();
            
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

            gridOrderItems.SelectionChanged += (s, e) => UpdateProductImageDisplay();

            txtDiscountPercent.TextChanged += (s, e) => CalculateTotal();
            txtDiscountAmount.TextChanged += (s, e) => CalculateTotal();
            btnSaveOrder.Click += (s, e) => SaveOrder();
            btnCancel.Click += (s, e) => Close();
            
            // Cleanup on form closing
            FormClosing += OrderForm_FormClosing;
        }

        /// <summary>
        /// Handles form closing to cleanup resources.
        /// Single responsibility: only handles cleanup.
        /// </summary>
        private void OrderForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // Cleanup if needed
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
                _errorHandler.ShowError(ex);
            }
        }

        private void AddNewCustomer()
        {
            using var dialog = new CustomerEditDialog(null);
            if (dialog.ShowDialog(this) == DialogResult.OK && dialog.CustomerDTO != null)
            {
                try
                {
                    if (dialog.CreateCustomerDTO != null)
                    {
                        _customerService.CreateCustomer(dialog.CreateCustomerDTO);
                        LoadCustomers();
                        SelectNewlyCreatedCustomer(dialog.CreateCustomerDTO.CustomerName);
                    }
                    _errorHandler.ShowSuccess("Thêm khách hàng thành công!");
                }
                catch (Exception ex)
                {
                    _errorHandler.ShowError(ex);
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
        /// Opens the product selection dialog for visual product browsing.
        /// Single responsibility: only opens the selection dialog.
        /// </summary>
        private void OpenProductSelectionDialog()
        {
            try
            {
                using var dialog = new ProductSelectionDialog(_productService, _inventoryService, _categoryService);
                if (dialog.ShowDialog(this) == DialogResult.OK && dialog.SelectedProduct != null)
                {
                    AddSelectedProductToOrder(dialog.SelectedProduct);
                }
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError(ex);
            }
        }

        /// <summary>
        /// Adds a selected product to the order.
        /// Single responsibility: only adds product to order.
        /// </summary>
        private void AddSelectedProductToOrder(ProductDTO product)
        {
            try
            {
                if (TryIncrementExistingProduct(product))
                {
                    txtProductSearch.Clear();
                    return;
                }

                if (!ValidateStockAvailability(product, out var stock))
                {
                    return;
                }

                AddProductRowToGrid(product, stock);
                txtProductSearch.Clear();
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError(ex);
            }
        }

        /// <summary>
        /// Sets up AutoComplete for product search textbox.
        /// Single responsibility: only configures AutoComplete.
        /// </summary>
        private void SetupProductSearchAutoComplete()
        {
            try
            {
                var products = _productService.GetAllProducts()
                    .Where(p => p.IsActive)
                    .ToList();

                var autoCompleteCollection = new AutoCompleteStringCollection();
                foreach (var product in products)
                {
                    // Add both product code and name for autocomplete
                    autoCompleteCollection.Add(product.ProductCode);
                    autoCompleteCollection.Add(product.Name);
                }

                txtProductSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtProductSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtProductSearch.AutoCompleteCustomSource = autoCompleteCollection;
                
                // Add tooltip
                var tooltip = new ToolTip
                {
                    IsBalloon = true,
                    ToolTipTitle = "Hướng dẫn",
                    AutoPopDelay = 5000
                };
                tooltip.SetToolTip(txtProductSearch, 
                    "Nhập mã/tên sản phẩm và nhấn Enter\n" +
                    "Hoặc quét mã vạch/barcode để tự động thêm\n" +
                    "Hoặc click nút 'Thêm' để chọn từ danh sách");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error setting up AutoComplete: {ex.Message}");
            }
        }

        /// <summary>
        /// Handles text changed event for live search (optional enhancement).
        /// Single responsibility: only handles text change events.
        /// </summary>
        private void HandleProductSearchTextChanged()
        {
            // Optional: Could add live search dropdown here
            // For now, AutoComplete handles suggestions
        }

        /// <summary>
        /// Main method to add a product to the order using quick search.
        /// Orchestrates the flow but delegates to smaller methods.
        /// </summary>
        /// <param name="isBarcodeScan">Indicates if this is from a barcode scanner</param>
        private void AddProductToOrder(bool isBarcodeScan = false)
        {
            var searchText = ValidateProductSearchInput();
            if (searchText == null) return;

            try
            {
                // For barcode scans, try exact match first
                ProductDTO? selectedProduct = null;
                
                if (isBarcodeScan)
                {
                    // Barcode scanner typically sends exact product code
                    selectedProduct = _productService.GetAllProducts()
                        .FirstOrDefault(p => p.IsActive && 
                            p.ProductCode.Equals(searchText, StringComparison.OrdinalIgnoreCase));
                }
                
                // If not found with exact match, do normal search
                if (selectedProduct == null)
                {
                    var products = SearchProducts(searchText);
                    if (products == null || products.Count == 0) return;

                    selectedProduct = SelectProductFromResults(products, isBarcodeScan);
                    if (selectedProduct == null) return;
                }

                if (TryIncrementExistingProduct(selectedProduct))
                {
                    txtProductSearch.Clear();
                    txtProductSearch.Focus();
                    return;
                }

                if (!ValidateStockAvailability(selectedProduct, out var stock))
                {
                    txtProductSearch.Focus();
                    return;
                }

                AddProductRowToGrid(selectedProduct, stock);
                txtProductSearch.Clear();
                txtProductSearch.Focus(); // Focus lại để tiếp tục quét/nhập sản phẩm tiếp theo
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError(ex);
                txtProductSearch.Focus();
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
                _errorHandler.ShowWarning("Vui lòng nhập mã hoặc tên sản phẩm!");
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
                _errorHandler.ShowWarning("Không tìm thấy sản phẩm!");
                return null;
            }

            return products;
        }

        /// <summary>
        /// Selects a product from search results, showing dialog if multiple matches.
        /// Single responsibility: only handles product selection.
        /// </summary>
        /// <param name="products">List of matching products</param>
        /// <param name="isBarcodeScan">If true and multiple matches, prefer exact code match</param>
        private ProductDTO? SelectProductFromResults(List<ProductDTO> products, bool isBarcodeScan = false)
        {
            if (products.Count == 1)
            {
                return products.First();
            }

            // For barcode scans, prefer exact code match if available
            if (isBarcodeScan && !string.IsNullOrWhiteSpace(txtProductSearch?.Text))
            {
                var exactMatch = products.FirstOrDefault(p => 
                    p.ProductCode.Equals(txtProductSearch.Text.Trim(), StringComparison.OrdinalIgnoreCase));
                if (exactMatch != null)
                {
                    return exactMatch;
                }
            }

            // Show selection dialog if multiple products found
            using var dialog = new ProductSelectionDialogLegacy(products);
            if (dialog.ShowDialog(this) != DialogResult.OK || dialog.SelectedProduct == null)
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
                _errorHandler.ShowWarning($"Sản phẩm '{product.Name}' đã hết hàng!");
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
                _errorHandler.ShowWarning($"Số lượng vượt quá tồn kho ({stock})!");
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
                var paymentMethod = GetSelectedPaymentMethod();
                var total = ApplyDiscount(CalculateSubtotal());

                // Xử lý theo phương thức thanh toán
                if (paymentMethod == PaymentMethod.VietQR)
                {
                    // Tạo đơn hàng trước với Status = "Pending"
                    createOrderDTO.Status = "Pending";
                    var createdOrder = SaveOrderToDatabase(createOrderDTO);
                    
                    if (createdOrder == null)
                    {
                        _errorHandler.ShowError("Không thể tạo đơn hàng!");
                        return;
                    }

                    // Hiển thị QR code payment với OrderId thực từ database
                    if (!ProcessVietQRPayment(createdOrder, total))
                    {
                        // Nếu hủy thanh toán, có thể xóa order hoặc giữ lại với Status = "Pending"
                        // Tạm thời giữ lại để có thể thanh toán sau
                        _errorHandler.ShowInfo("Đơn hàng đã được tạo với trạng thái 'Chờ thanh toán'. Bạn có thể thanh toán sau.");
                        Close();
                        return;
                    }

                    // Thanh toán thành công (đã được xử lý trong ProcessVietQRPayment)
                    _errorHandler.ShowSuccess("Thanh toán thành công! Hóa đơn đã được lưu.");
                    ShowOrderDetailAfterCreate(createdOrder);
                    Close();
                }
                else
                {
                    // Các phương thức thanh toán khác: xác nhận trước khi lưu
            if (!ConfirmPaymentBeforeSaveWithDTO(createOrderDTO))
                return;

            var createdOrder = SaveOrderToDatabase(createOrderDTO);
                if (createdOrder != null)
                {
                    _errorHandler.ShowSuccess("Lưu hóa đơn thành công!");
                    ShowOrderDetailAfterCreate(createdOrder);
                }
                else
                {
                    _errorHandler.ShowSuccess("Lưu hóa đơn thành công!");
                }
                
                Close();
                }
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError(ex);
            }
        }

        /// <summary>
        /// Xử lý thanh toán VietQR: hiển thị QR code và chờ thanh toán
        /// </summary>
        private bool ProcessVietQRPayment(OrderDTO order, decimal total)
        {
            try
            {
                // Tạo description cho PayOS (max 25 ký tự)
                var orderDescription = order.OrderCode.Length > 23 
                    ? $"DH {order.OrderCode.Substring(0, 21)}" 
                    : $"DH {order.OrderCode}";
                
                // Hiển thị QR code payment dialog với OrderId thực từ database
                using var qrPaymentDialog = new QRCodePaymentDialog(order.Id, total, orderDescription);
                if (qrPaymentDialog.ShowDialog(this) == DialogResult.OK && qrPaymentDialog.IsPaymentConfirmed)
                {
                    // Thanh toán thành công - webhook đã cập nhật Status = "Paid" và PaidAt
                    // Refresh order để lấy trạng thái mới nhất từ database
                    var updatedOrder = _orderService.GetOrderById(order.Id);
                    if (updatedOrder != null && updatedOrder.Status == "Paid")
                    {
                        // Lưu thông tin PayOS vào Notes nếu cần (tùy chọn)
                        if (qrPaymentDialog.PaymentData != null)
                        {
                            var notes = FormatPayOSPaymentInfo(qrPaymentDialog.PaymentData);
                            // Có thể update Notes nếu cần, nhưng webhook đã xử lý rồi
                        }
                        return true;
                    }
                    else
                    {
                        // Chưa thanh toán thành công, nhưng user đã đóng dialog
                        // Order vẫn ở trạng thái "Pending", có thể thanh toán sau
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError($"Lỗi khi xử lý thanh toán VietQR: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Confirms payment with pre-built order DTO (allows updating Notes for bank transfer).
        /// Single responsibility: only handles payment confirmation with DTO.
        /// </summary>
        private bool ConfirmPaymentBeforeSaveWithDTO(CreateOrderDTO createOrderDTO)
        {
            var subtotal = CalculateSubtotal();
            var total = ApplyDiscount(subtotal);
            var paymentMethod = GetSelectedPaymentMethod();

            // Show payment dialog for cash payment
            if (paymentMethod == PaymentMethod.Cash)
            {
                using var paymentDialog = new PaymentDialog(total);
                if (paymentDialog.ShowDialog(this) == DialogResult.OK)
                {
                    if (paymentDialog.Change > 0)
                    {
                        var changeMessage = $"Tiền thừa: {paymentDialog.Change:N0} VNĐ\n\n" +
                                           $"Bạn có chắc chắn muốn lưu hóa đơn này?";
                        return ErrorHandler.ShowConfirmation(changeMessage, "Xác nhận thanh toán");
                    }
                    return true;
                }
                return false;
            }
            else if (paymentMethod == PaymentMethod.VietQR)
            {
                // VietQR payment được xử lý trong SaveOrder() sau khi tạo order
                // Không xử lý ở đây nữa
                return true; // Cho phép tiếp tục để tạo order
            }
            else
            {
                // For other payment methods (Card, Other), show simple confirmation
                var message = $"Xác nhận thanh toán:\n\n" +
                             $"Tổng tiền: {total:N0} VNĐ\n" +
                             $"Phương thức: {paymentMethod}\n\n" +
                             $"Bạn có chắc chắn muốn lưu hóa đơn này?";

                return ErrorHandler.ShowConfirmation(message, "Xác nhận thanh toán");
            }
        }

        /// <summary>
        /// Formats PayOS payment information for storage in Notes field.
        /// Single responsibility: only formats PayOS payment info.
        /// </summary>
        private string FormatPayOSPaymentInfo(WinFormsFashionShop.Presentation.Models.PaymentData paymentData)
        {
            return $"VIETQR PAYOS:\n" +
                   $"Mã đơn PayOS: {paymentData.OrderCode}\n" +
                   $"Link thanh toán: {paymentData.CheckoutUrl}\n" +
                   $"Ngày tạo: {DateTime.Now:dd/MM/yyyy HH:mm}";
        }


        /// <summary>
        /// Shows order detail dialog after successful creation.
        /// Single responsibility: only displays order details.
        /// </summary>
        private void ShowOrderDetailAfterCreate(OrderDTO order)
        {
            // Reload order from database to get latest status (webhook may have updated it)
            var refreshedOrder = _orderService.GetOrderById(order.Id);
            if (refreshedOrder == null)
            {
                refreshedOrder = order; // Fallback to original order if reload fails
            }

            var statusText = refreshedOrder.Status == OrderStatus.Paid ? "Đã thanh toán" :
                            refreshedOrder.Status == OrderStatus.Cancelled ? "Đã hủy" : "Chờ thanh toán";
            
            var message = $"Hóa đơn đã được tạo thành công!\n\n" +
                         $"Mã đơn: {refreshedOrder.OrderCode}\n" +
                         $"Tổng tiền: {refreshedOrder.TotalAmount:N0} VNĐ\n" +
                         $"Phương thức: {refreshedOrder.PaymentMethod}\n" +
                         $"Trạng thái: {statusText}\n\n" +
                         $"Bạn có muốn xem chi tiết hóa đơn không?";

            if (_errorHandler.ShowConfirmation(message, "Hóa đơn đã tạo"))
            {
                using var dialog = new OrderDetailDialog(refreshedOrder);
                dialog.ShowDialog(this);
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
                _errorHandler.ShowWarning("Hóa đơn phải có ít nhất một sản phẩm!");
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
                _errorHandler.ShowError($"Sản phẩm '{product.Name}' không đủ tồn kho!");
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
        /// Saves the order to the database and returns the created order.
        /// Single responsibility: only saves to database.
        /// </summary>
        private OrderDTO? SaveOrderToDatabase(CreateOrderDTO createOrderDTO)
        {
            return _orderService.CreateOrder(createOrderDTO);
        }

        /// <summary>
        /// Updates product image display when selection changes.
        /// Single responsibility: only updates image display.
        /// </summary>
        private void UpdateProductImageDisplay()
        {
            if (gridOrderItems.SelectedRows.Count == 0)
            {
                picProductImage.Image = null;
                lblProductImage.Text = "Ảnh sản phẩm";
                return;
            }

            var selectedRow = gridOrderItems.SelectedRows[0];
            var product = selectedRow.Tag as ProductDTO;

            if (product != null && !string.IsNullOrWhiteSpace(product.ImagePath))
            {
                try
                {
                    var image = ImageHelper.LoadProductImage(product.ImagePath);
                    if (image != null)
                    {
                        picProductImage.Image?.Dispose();
                        picProductImage.Image = ImageHelper.ResizeImage(image, picProductImage.Width, picProductImage.Height);
                        lblProductImage.Text = product.Name;
                    }
                    else
                    {
                        picProductImage.Image = null;
                        lblProductImage.Text = product.Name;
                    }
                }
                catch
                {
                    picProductImage.Image = null;
                    lblProductImage.Text = product.Name;
                }
            }
            else
            {
                picProductImage.Image = null;
                lblProductImage.Text = selectedRow.Cells["ProductName"]?.Value?.ToString() ?? "Ảnh sản phẩm";
            }
        }
    }

    // Legacy dialog for selecting product when multiple matches found (kept for backward compatibility)
    public class ProductSelectionDialogLegacy : Form
    {
        private DataGridView _grid;
        public ProductDTO? SelectedProduct { get; private set; }

        public ProductSelectionDialogLegacy(List<ProductDTO> products)
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
