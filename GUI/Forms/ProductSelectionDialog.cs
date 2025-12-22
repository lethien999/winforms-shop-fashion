using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.DTO;
using WinFormsFashionShop.Presentation.Helpers;
using UIThemeConstants = WinFormsFashionShop.Presentation.Helpers.UIThemeConstants;

namespace WinFormsFashionShop.Presentation.Forms
{
    /// <summary>
    /// Dialog for selecting products with visual interface.
    /// Single responsibility: only handles product selection UI.
    /// </summary>
    public partial class ProductSelectionDialog : Form
    {
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;
        private readonly ICategoryService _categoryService;
        // UI controls are declared in ProductSelectionDialog.Designer.cs
        private List<ProductDTO> _allProducts = new List<ProductDTO>();
        private List<ProductDTO> _filteredProducts = new List<ProductDTO>();

        public ProductDTO? SelectedProduct { get; private set; }

        public ProductSelectionDialog(IProductService productService, IInventoryService inventoryService, ICategoryService categoryService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _inventoryService = inventoryService ?? throw new ArgumentNullException(nameof(inventoryService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            
            InitializeComponent();
            LoadLogo();
            SetupGridColumns();
            SetupEventHandlers();
            LoadProducts(); // Load products after grid is set up
        }

        /// <summary>
        /// Loads brand logo into header PictureBox.
        /// Single responsibility: only loads logo.
        /// </summary>
        private void LoadLogo()
        {
            // Logo is loaded from Resources in Designer file
            // If logo needs to be loaded dynamically, use LogoHelper here
            try
            {
                var logo = LogoHelper.LoadLogo(UIThemeConstants.Spacing.LogoSizeMedium);
                if (logo != null && picLogo != null)
                {
                    picLogo.Image = logo;
                }
                else if (picLogo != null && picLogo.Image == null)
                {
                    // Hide logo if not found and no image is set
                    picLogo.Visible = false;
                }
            }
            catch
            {
                // Ignore logo loading errors
                if (picLogo != null)
                {
                    picLogo.Visible = false;
                }
            }
        }

        /// <summary>
        /// Sets up grid columns and styling after InitializeComponent.
        /// Single responsibility: only configures grid columns.
        /// </summary>
        private void SetupGridColumns()
        {
            // Clear existing columns if any
            _gridProducts.Columns.Clear();
            
            // Setup grid columns
            var imageColumn = new DataGridViewImageColumn
            {
                Name = "Image",
                HeaderText = "Hình ảnh",
                Width = 80,
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };
            _gridProducts.Columns.Add(imageColumn);

            _gridProducts.Columns.Add("ProductCode", "Mã SP");
            _gridProducts.Columns["ProductCode"].Width = 100;

            _gridProducts.Columns.Add("Name", "Tên sản phẩm");
            _gridProducts.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            _gridProducts.Columns.Add("CategoryName", "Danh mục");
            _gridProducts.Columns["CategoryName"].Width = 150;

            _gridProducts.Columns.Add("UnitPrice", "Đơn giá");
            _gridProducts.Columns["UnitPrice"].Width = 120;
            _gridProducts.Columns["UnitPrice"].DefaultCellStyle.Format = "N0";
            _gridProducts.Columns["UnitPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            _gridProducts.Columns.Add("Stock", "Tồn kho");
            _gridProducts.Columns["Stock"].Width = 100;
            _gridProducts.Columns["Stock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Grid styling
            _gridProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 130, 180);
            _gridProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            _gridProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            _gridProducts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 230, 255);
            _gridProducts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
        }

        private void LoadProducts()
        {
            try
            {
                // Load all products first
                var allProducts = _productService.GetAllProducts();
                if (allProducts == null)
                {
                    ErrorHandler.ShowWarning("Không thể tải danh sách sản phẩm. Dịch vụ trả về null.");
                    _allProducts = new List<ProductDTO>();
                    _filteredProducts = new List<ProductDTO>();
                    PopulateGrid();
                    return;
                }

                var allProductsList = allProducts.ToList();
                System.Diagnostics.Debug.WriteLine($"ProductSelectionDialog: GetAllProducts returned {allProductsList.Count} products");

                _allProducts = allProductsList
                    .Where(p => p != null && p.IsActive)
                    .ToList();

                // Debug output
                System.Diagnostics.Debug.WriteLine($"ProductSelectionDialog: After filtering IsActive, {_allProducts.Count} active products remain");

                if (_allProducts.Count == 0 && allProductsList.Count > 0)
                {
                    // Có sản phẩm nhưng không có sản phẩm nào active
                    ErrorHandler.ShowInfo($"Có {allProductsList.Count} sản phẩm trong hệ thống nhưng không có sản phẩm nào đang hoạt động.\n\nVui lòng kích hoạt sản phẩm trước khi tạo đơn hàng.");
                }

                LoadCategories();
                ApplyFilters();
                
                // Debug: Verify filters applied
                System.Diagnostics.Debug.WriteLine($"ProductSelectionDialog: After filtering, {_filteredProducts?.Count ?? 0} products to display");
            }
            catch (Exception ex)
            {
                // Show detailed error for debugging
                var errorMsg = $"Lỗi khi tải danh sách sản phẩm:\n{ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMsg += $"\n\nChi tiết: {ex.InnerException.Message}";
                }
                errorMsg += $"\n\nStack trace: {ex.StackTrace}";
                ErrorHandler.ShowError(errorMsg);
                _allProducts = new List<ProductDTO>();
                _filteredProducts = new List<ProductDTO>();
                PopulateGrid(); // Clear grid on error
            }
        }

        private void LoadCategories()
        {
            try
            {
                _cmbCategoryFilter.Items.Clear();
                var categories = _categoryService.GetAllCategories().ToList();
                _cmbCategoryFilter.Items.Add("Tất cả danh mục");
                foreach (var category in categories)
                {
                    _cmbCategoryFilter.Items.Add(category);
                }
                _cmbCategoryFilter.DisplayMember = "CategoryName";
                if (_cmbCategoryFilter.Items.Count > 0)
                {
                _cmbCategoryFilter.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Lỗi khi tải danh mục: {ex.Message}");
                // Add default "Tất cả danh mục" even on error
                if (_cmbCategoryFilter.Items.Count == 0)
                {
                    _cmbCategoryFilter.Items.Add("Tất cả danh mục");
                    _cmbCategoryFilter.SelectedIndex = 0;
                }
            }
        }

        private void SetupEventHandlers()
        {
            _txtSearch.TextChanged += (s, e) => ApplyFilters();
            _cmbCategoryFilter.SelectedIndexChanged += (s, e) => ApplyFilters();
            _gridProducts.SelectionChanged += (s, e) => UpdateProductDetail();
            _gridProducts.CellDoubleClick += (s, e) => SelectProduct();
            _gridProducts.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SelectProduct();
                    e.Handled = true;
                }
            };
            _btnSelect.Click += (s, e) => SelectProduct();
            _btnCancel.Click += (s, e) => Close();
        }

        private void ApplyFilters()
        {
            try
            {
                if (_allProducts == null)
                {
                    _filteredProducts = new List<ProductDTO>();
                    PopulateGrid();
                    return;
                }

                var searchText = _txtSearch?.Text?.Trim().ToLower() ?? "";
                var selectedCategory = _cmbCategoryFilter?.SelectedItem;

                _filteredProducts = _allProducts.Where(p =>
                {
                    if (p == null) return false;
                    
                    var matchesSearch = string.IsNullOrWhiteSpace(searchText) ||
                                       (p.ProductCode?.ToLower().Contains(searchText) ?? false) ||
                                       (p.Name?.ToLower().Contains(searchText) ?? false);

                    var matchesCategory = selectedCategory == null ||
                                        selectedCategory?.ToString() == "Tất cả danh mục" ||
                                        (selectedCategory is CategoryDTO category && p.CategoryId == category.Id);

                    return matchesSearch && matchesCategory;
                }).ToList();

                PopulateGrid();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Lỗi khi lọc sản phẩm: {ex.Message}");
                _filteredProducts = new List<ProductDTO>();
                PopulateGrid();
            }
        }

        private void PopulateGrid()
        {
            try
            {
                // Ensure grid is initialized
                if (_gridProducts == null)
                {
                    ErrorHandler.ShowError("Grid chưa được khởi tạo!");
                    return;
                }

                // Ensure columns are set up
                if (_gridProducts.Columns.Count == 0)
                {
                    SetupGridColumns();
                }

            _gridProducts.Rows.Clear();

                if (_filteredProducts == null || _filteredProducts.Count == 0)
                {
                    // No products to display - grid will be empty
                    System.Diagnostics.Debug.WriteLine("ProductSelectionDialog: No filtered products to display");
                    
                    // Show message in product info panel
                    if (_lblProductInfo != null)
                    {
                        if (_allProducts == null || _allProducts.Count == 0)
                        {
                            _lblProductInfo.Text = "Không có sản phẩm nào trong hệ thống.\n\nVui lòng thêm sản phẩm trước khi tạo đơn hàng.";
                        }
                        else
                        {
                            _lblProductInfo.Text = "Không tìm thấy sản phẩm phù hợp với bộ lọc.\n\nVui lòng thử lại với từ khóa hoặc danh mục khác.";
                        }
                    }
                    return;
                }

                System.Diagnostics.Debug.WriteLine($"ProductSelectionDialog: Populating grid with {_filteredProducts.Count} products");

            foreach (var product in _filteredProducts)
            {
                    try
                    {
                        if (product == null) continue;

                var inventory = _inventoryService.GetInventoryByProductId(product.Id);
                var stock = inventory?.QuantityInStock ?? 0;
                var categoryName = GetCategoryName(product.CategoryId);

                // Load product image
                Image? thumbnail = null;
                if (!string.IsNullOrWhiteSpace(product.ImagePath))
                {
                    try
                    {
                        var image = ImageHelper.LoadProductImage(product.ImagePath);
                        if (image != null)
                        {
                            thumbnail = ImageHelper.ResizeImage(image, 60, 60);
                        }
                    }
                    catch
                    {
                        // Ignore image loading errors
                    }
                }

                        // Verify column count matches
                        if (_gridProducts.Columns.Count != 6)
                        {
                            var errorMsg = $"Lỗi: Số lượng cột không khớp. Cần 6 cột, hiện có {_gridProducts.Columns.Count} cột.";
                            System.Diagnostics.Debug.WriteLine($"ProductSelectionDialog: {errorMsg}");
                            ErrorHandler.ShowError(errorMsg);
                            return; // Stop adding rows if column count is wrong
                        }

                        try
                        {
                var rowIndex = _gridProducts.Rows.Add(
                                thumbnail ?? CreatePlaceholderImage(), // Image (column 0)
                                product.ProductCode ?? "", // ProductCode (column 1)
                                product.Name ?? "", // Name (column 2)
                                categoryName, // CategoryName (column 3)
                                product.UnitPrice, // UnitPrice (column 4)
                                stock // Stock (column 5)
                );

                            if (rowIndex >= 0 && rowIndex < _gridProducts.Rows.Count)
                            {
                var row = _gridProducts.Rows[rowIndex];
                row.Tag = product;
                            }
                        }
                        catch (Exception addEx)
                        {
                            // Log detailed error for row addition
                            var errorMsg = $"Lỗi khi thêm sản phẩm '{product.ProductCode}' vào grid: {addEx.Message}";
                            System.Diagnostics.Debug.WriteLine($"ProductSelectionDialog: {errorMsg}");
                            System.Diagnostics.Debug.WriteLine($"ProductSelectionDialog: Stack trace: {addEx.StackTrace}");
                            // Continue with next product instead of stopping
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log error for individual product but continue with others
                        System.Diagnostics.Debug.WriteLine($"Error loading product {product?.ProductCode}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Lỗi khi hiển thị danh sách sản phẩm: {ex.Message}\n\nChi tiết: {ex.StackTrace}");
            }
        }

        private string GetCategoryName(int? categoryId)
        {
            if (categoryId == null) return "N/A";
            try
            {
                var category = _categoryService.GetCategoryById(categoryId.Value);
                return category?.CategoryName ?? "N/A";
            }
            catch
            {
                return "N/A";
            }
        }

        private void UpdateProductDetail()
        {
            if (_gridProducts.SelectedRows.Count == 0)
            {
                _picProductImage.Image = null;
                _lblProductInfo.Text = "Chọn sản phẩm để xem thông tin";
                return;
            }

            var selectedRow = _gridProducts.SelectedRows[0];
            var product = selectedRow.Tag as ProductDTO;

            if (product == null) return;

            // Load product image
            if (!string.IsNullOrWhiteSpace(product.ImagePath))
            {
                try
                {
                    var image = ImageHelper.LoadProductImage(product.ImagePath);
                    if (image != null)
                    {
                        _picProductImage.Image?.Dispose();
                        _picProductImage.Image = ImageHelper.ResizeImage(image, 200, 200);
                    }
                    else
                    {
                        _picProductImage.Image = null;
                    }
                }
                catch
                {
                    _picProductImage.Image = null;
                }
            }
            else
            {
                _picProductImage.Image = null;
            }

            // Load inventory info
            var inventory = _inventoryService.GetInventoryByProductId(product.Id);
            var stock = inventory?.QuantityInStock ?? 0;

            _lblProductInfo.Text = $"Mã SP: {product.ProductCode}\n\n" +
                                  $"Tên: {product.Name}\n\n" +
                                  $"Danh mục: {GetCategoryName(product.CategoryId)}\n\n" +
                                  $"Giá: {product.UnitPrice:N0} VNĐ\n\n" +
                                  $"Tồn kho: {stock}";
        }

        private void SelectProduct()
        {
            if (_gridProducts.SelectedRows.Count == 0)
            {
                ErrorHandler.ShowWarning("Vui lòng chọn sản phẩm!");
                return;
            }

            var selectedRow = _gridProducts.SelectedRows[0];
            SelectedProduct = selectedRow.Tag as ProductDTO;

            if (SelectedProduct != null)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        /// <summary>
        /// Creates a placeholder image when product has no image.
        /// Single responsibility: only creates placeholder image.
        /// </summary>
        private Image CreatePlaceholderImage()
        {
            var img = new Bitmap(60, 60);
            using (var g = Graphics.FromImage(img))
            {
                g.Clear(Color.FromArgb(240, 240, 240));
                using (var pen = new Pen(Color.FromArgb(200, 200, 200), 1))
                {
                    g.DrawRectangle(pen, 0, 0, 59, 59);
                }
                using (var font = new Font("Arial", 8))
                using (var brush = new SolidBrush(Color.FromArgb(150, 150, 150)))
                {
                    g.DrawString("No Image", font, brush, new PointF(10, 20));
                }
            }
            return img;
        }
    }
}

