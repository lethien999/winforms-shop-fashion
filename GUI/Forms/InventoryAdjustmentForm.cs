using System;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.DTO;
using WinFormsFashionShop.Presentation.Helpers;

namespace WinFormsFashionShop.Presentation.Forms
{
    public partial class InventoryAdjustmentForm : Form
    {
        private readonly IInventoryService _inventoryService;
        private readonly IProductService _productService;
        private readonly IErrorHandler _errorHandler;
        private System.Collections.Generic.List<InventoryAdjustmentItem> _adjustmentItems;

        public InventoryAdjustmentForm(IInventoryService inventoryService, IProductService productService, IErrorHandler errorHandler)
        {
            _inventoryService = inventoryService ?? throw new ArgumentNullException(nameof(inventoryService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
            _adjustmentItems = new System.Collections.Generic.List<InventoryAdjustmentItem>();

            // InitializeComponent() must be called first to initialize all controls
            InitializeComponent();
            InitializeControls();
            LoadInventory();
        }

        private void InitializeControls()
        {
            // Validate grid is initialized
            if (grid == null)
            {
                _errorHandler.ShowError("Lỗi khởi tạo form: Grid chưa được khởi tạo!");
                return;
            }

            // Clear existing columns if any
            grid.Columns.Clear();

            // Setup grid columns
            grid.Columns.Add("ProductCode", "Mã SP");
            grid.Columns.Add("ProductName", "Tên SP");
            grid.Columns.Add("CurrentStock", "Tồn hiện tại");
            grid.Columns.Add("AdjustQuantity", "Số lượng nhập");
            grid.Columns.Add("NewStock", "Tồn mới");
            grid.Columns.Add("Delete", "Xóa");

            foreach (DataGridViewColumn col in grid.Columns)
            {
                if (col.Name == "AdjustQuantity")
                {
                    col.ReadOnly = false;
                }
                else if (col.Name == "Delete")
                {
                    col.ReadOnly = true;
                }
                else
                {
                    col.ReadOnly = true;
                }
            }

            // Wire up event handlers
            if (btnAdd != null)
                btnAdd.Click += (s, e) => AddProductToAdjustment();
            
            if (txtProductSearch != null)
                txtProductSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) AddProductToAdjustment(); };
            
            grid.CellValueChanged += (s, e) =>
            {
                if (e.ColumnIndex == grid.Columns["AdjustQuantity"].Index)
                {
                    UpdateNewStock(e.RowIndex);
                }
            };

            grid.CellContentClick += (s, e) =>
            {
                if (e.ColumnIndex == grid.Columns["Delete"].Index)
                {
                    RemoveAdjustmentItem(e.RowIndex);
                }
            };

            if (btnSave != null)
                btnSave.Click += (s, e) => SaveAdjustments();
            
            if (btnRefresh != null)
                btnRefresh.Click += (s, e) => LoadInventory();
        }

        private void LoadInventory()
        {
            try
            {
                var inventories = _inventoryService.GetAllInventories().ToList();
                var products = _productService.GetAllProducts().Where(p => p.IsActive).ToList();

                grid.Rows.Clear();
                _adjustmentItems.Clear();

                foreach (var inv in inventories)
                {
                    var product = products.FirstOrDefault(p => p.Id == inv.ProductId);
                    if (product == null) continue;

                    var rowIndex = grid.Rows.Add();
                    var row = grid.Rows[rowIndex];
                    row.Cells["ProductCode"].Value = product.ProductCode;
                    row.Cells["ProductName"].Value = product.Name;
                    row.Cells["CurrentStock"].Value = inv.QuantityInStock;
                    row.Cells["AdjustQuantity"].Value = 0;
                    row.Cells["NewStock"].Value = inv.QuantityInStock;
                    row.Cells["Delete"].Value = "Xóa";
                    row.Tag = new { Product = product, Inventory = inv };
                }
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError(ex);
            }
        }

        private void AddProductToAdjustment()
        {
            if (txtProductSearch == null)
            {
                _errorHandler.ShowError("Lỗi: txtProductSearch chưa được khởi tạo!");
                return;
            }

            var searchText = txtProductSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                _errorHandler.ShowWarning("Vui lòng nhập mã hoặc tên sản phẩm!");
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
                    _errorHandler.ShowWarning("Không tìm thấy sản phẩm!");
                    return;
                }

                ProductDTO selectedProduct;
                if (products.Count == 1)
                {
                    selectedProduct = products.First();
                }
                else
                {
                    using var dialog = new ProductSelectionDialogLegacy(products);
                    if (dialog.ShowDialog(this) != DialogResult.OK || dialog.SelectedProduct == null)
                        return;
                    selectedProduct = dialog.SelectedProduct;
                }

                // Check if already in grid
                var existingRow = grid.Rows.Cast<DataGridViewRow>()
                    .FirstOrDefault(r => r.Cells["ProductCode"].Value?.ToString() == selectedProduct.ProductCode);

                if (existingRow != null)
                {
                    var currentQty = Convert.ToInt32(existingRow.Cells["AdjustQuantity"].Value ?? 0);
                    if (numQuantity != null)
                    {
                        existingRow.Cells["AdjustQuantity"].Value = currentQty + (int)numQuantity.Value;
                    }
                    UpdateNewStock(existingRow.Index);
                    if (txtProductSearch != null)
                        txtProductSearch.Clear();
                    return;
                }

                // Add new row
                var inventory = _inventoryService.GetInventoryByProductId(selectedProduct.Id);
                var currentStock = inventory?.QuantityInStock ?? 0;

                var rowIndex = grid.Rows.Add();
                var row = grid.Rows[rowIndex];
                row.Cells["ProductCode"].Value = selectedProduct.ProductCode;
                row.Cells["ProductName"].Value = selectedProduct.Name;
                row.Cells["CurrentStock"].Value = currentStock;
                
                var adjustQty = numQuantity != null ? (int)numQuantity.Value : 1;
                row.Cells["AdjustQuantity"].Value = adjustQty;
                row.Cells["NewStock"].Value = currentStock + adjustQty;
                row.Cells["Delete"].Value = "Xóa";
                row.Tag = new { Product = selectedProduct, Inventory = inventory };

                if (txtProductSearch != null)
                    txtProductSearch.Clear();
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError(ex);
            }
        }

        private void UpdateNewStock(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= grid.Rows.Count) return;

            var row = grid.Rows[rowIndex];
            if (int.TryParse(row.Cells["CurrentStock"].Value?.ToString(), out var currentStock) &&
                int.TryParse(row.Cells["AdjustQuantity"].Value?.ToString(), out var adjustQty))
            {
                row.Cells["NewStock"].Value = currentStock + adjustQty;
            }
        }

        private void RemoveAdjustmentItem(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < grid.Rows.Count)
            {
                grid.Rows.RemoveAt(rowIndex);
            }
        }

        private void SaveAdjustments()
        {
            if (grid.Rows.Count == 0)
            {
                _errorHandler.ShowWarning("Không có sản phẩm nào để cập nhật!");
                return;
            }

            try
            {
                int successCount = 0;
                foreach (DataGridViewRow row in grid.Rows)
                {
                    var tag = row.Tag as dynamic;
                    if (tag == null) continue;

                    var product = tag.Product as ProductDTO;
                    if (product == null) continue;

                    var adjustQty = Convert.ToInt32(row.Cells["AdjustQuantity"].Value ?? 0);
                    if (adjustQty <= 0) continue;

                    _inventoryService.IncreaseStock(product.Id, adjustQty);
                    successCount++;
                }

                if (successCount > 0)
                {
                    _errorHandler.ShowSuccess($"Cập nhật tồn kho thành công cho {successCount} sản phẩm!");
                    LoadInventory();
                }
                else
                {
                    _errorHandler.ShowWarning("Không có sản phẩm nào được cập nhật!");
                }
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError(ex);
            }
        }
    }

    internal class InventoryAdjustmentItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

