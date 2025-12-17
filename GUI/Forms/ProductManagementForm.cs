using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Constants;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.DTO;
using WinFormsFashionShop.Presentation.Helpers;
using UIThemeConstants = WinFormsFashionShop.Presentation.Helpers.UIThemeConstants;

namespace WinFormsFashionShop.Presentation.Forms
{
    public partial class ProductManagementForm : Form
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IErrorHandler _errorHandler;
        private readonly UserDTO? _currentUser;

        public ProductManagementForm(IProductService productService, ICategoryService categoryService, IErrorHandler errorHandler, UserDTO? currentUser = null)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
            _currentUser = currentUser;
            InitializeComponent();
            InitializeControls();
            LoadProducts();
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

            // Load categories for filter
            LoadCategoriesForFilter();

            // Wire up event handlers
            cmbCategoryFilter.SelectedIndexChanged += (s, e) => LoadProducts();
            btnSearch.Click += (s, e) => LoadProducts();
            grid.CellDoubleClick += (s, e) => 
            {
                if (e.ColumnIndex == grid.Columns["Ảnh"]?.Index)
                {
                    ShowImagePreview(e.RowIndex);
                }
                else
                {
                    EditSelectedProduct();
                }
            };
            grid.CellClick += (s, e) =>
            {
                if (e.ColumnIndex == grid.Columns["Ảnh"]?.Index)
                {
                    ShowImagePreview(e.RowIndex);
                }
            };
            btnAdd.Click += (s, e) => AddProduct();
            btnEdit.Click += (s, e) => EditSelectedProduct();
            btnDelete.Click += (s, e) => DeleteSelectedProduct();
            btnDeactivate.Click += (s, e) => DeactivateSelectedProduct();
            btnRefresh.Click += (s, e) => LoadProducts();

            // Kiểm tra quyền: Chỉ Admin mới được thêm/sửa/xóa/ngừng kinh doanh sản phẩm
            if (_currentUser == null || _currentUser.Role != UserRole.Admin)
            {
                btnAdd.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                btnDeactivate.Visible = false;
            }
        }

        /// <summary>
        /// Loads categories into the filter ComboBox with proper display.
        /// Single responsibility: only loads and configures categories for filter.
        /// </summary>
        private void LoadCategoriesForFilter()
        {
            try
            {
                var categories = _categoryService.GetAllCategories().ToList();
                cmbCategoryFilter.Items.Clear();
                
                // Set display member to show CategoryName instead of object type
                cmbCategoryFilter.DisplayMember = "CategoryName";
                cmbCategoryFilter.ValueMember = "Id";
                
                cmbCategoryFilter.Items.Add("Tất cả danh mục");
                foreach (var cat in categories)
                {
                    cmbCategoryFilter.Items.Add(cat);
                }
                cmbCategoryFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex);
            }
        }

        private void LoadProducts()
        {
            try
            {
                if (grid == null)
                {
                    _errorHandler.ShowError("Grid chưa được khởi tạo!");
                    return;
                }

                var allProducts = _productService.GetAllProducts();
                if (allProducts == null)
                {
                    _errorHandler.ShowWarning("Không thể tải danh sách sản phẩm. Dịch vụ trả về null.");
                    grid.DataSource = null;
                    return;
                }

                var products = allProducts.ToList();
                
                // Filter by search text
                if (txtSearch != null && !string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    var searchText = txtSearch.Text.ToLower();
                    products = products.Where(p => 
                        p != null &&
                        (p.ProductCode?.ToLower().Contains(searchText) ?? false) ||
                        (p.Name?.ToLower().Contains(searchText) ?? false)
                    ).ToList();
                }

                // Filter by category
                if (cmbCategoryFilter != null && cmbCategoryFilter.SelectedIndex > 0 && cmbCategoryFilter.SelectedItem is CategoryDTO selectedCategory)
                {
                    products = products.Where(p => p != null && p.CategoryId == selectedCategory.Id).ToList();
                }

                // Setup grid columns if not already set
                SetupProductGridColumns();

                // Load data with images
                if (products == null)
                {
                    grid.DataSource = new List<object>();
                    return;
                }

                var productList = products.Where(p => p != null).Select(p => new
                {
                    p.Id,
                    p.ProductCode,
                    p.Name,
                    DanhMục = p.CategoryName ?? "",
                    p.UnitPrice,
                    p.Unit,
                    p.IsActive,
                    TrạngThái = p.IsActive ? "Hoạt động" : "Ngừng",
                    ImagePath = p.ImagePath
                }).ToList();

                grid.DataSource = productList;

                // Load images into grid
                LoadProductImagesToGrid(products.Where(p => p != null).ToList());
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError($"Lỗi khi tải danh sách sản phẩm: {ex.Message}");
                if (grid != null)
                {
                    grid.DataSource = null;
                }
            }
        }

        /// <summary>
        /// Sets up grid columns for product display with image column.
        /// Single responsibility: only configures grid columns.
        /// </summary>
        private void SetupProductGridColumns()
        {
            if (grid.Columns.Count > 0 && grid.Columns.Contains("Ảnh"))
                return; // Already set up

            // Add image column if not exists
            if (!grid.Columns.Contains("Ảnh"))
            {
                var imageColumn = new DataGridViewImageColumn
                {
                    Name = "Ảnh",
                    HeaderText = "Ảnh",
                    Width = 80,
                    ImageLayout = DataGridViewImageCellLayout.Zoom
                };
                grid.Columns.Insert(0, imageColumn);
            }

            // Reorder columns: Ảnh, Mã SP, Tên SP, Danh mục, Giá, Đơn vị, Trạng thái
            if (grid.Columns.Contains("Id"))
                grid.Columns["Id"].Visible = false;
        }

        /// <summary>
        /// Loads product images into the grid.
        /// Single responsibility: only loads images into grid cells.
        /// </summary>
        private void LoadProductImagesToGrid(List<ProductDTO> products)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells["Id"]?.Value == null) continue;

                var productId = (int)row.Cells["Id"].Value;
                var product = products.FirstOrDefault(p => p.Id == productId);

                if (product != null && !string.IsNullOrWhiteSpace(product.ImagePath))
                {
                    try
                    {
                        var image = ImageHelper.LoadProductImage(product.ImagePath);
                        if (image != null)
                        {
                            var thumbnail = ImageHelper.ResizeImage(image, 60, 60);
                            row.Cells["Ảnh"].Value = thumbnail;
                            // Store full image path in tag for preview
                            row.Cells["Ảnh"].Tag = product;
                        }
                    }
                    catch
                    {
                        // If image fails to load, leave cell empty
                        row.Cells["Ảnh"].Value = null;
                    }
                }
            }
        }

        /// <summary>
        /// Shows image preview dialog when clicking on image cell.
        /// Single responsibility: only displays image preview.
        /// </summary>
        private void ShowImagePreview(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= grid.Rows.Count) return;

            var row = grid.Rows[rowIndex];
            var product = row.Cells["Ảnh"]?.Tag as ProductDTO;
            
            if (product != null && !string.IsNullOrWhiteSpace(product.ImagePath))
            {
                using var previewDialog = new ImagePreviewDialog(product.ImagePath, product.Name);
                previewDialog.ShowDialog(this);
            }
            else
            {
                _errorHandler.ShowInfo("Sản phẩm này chưa có ảnh!");
            }
        }

        /// <summary>
        /// Opens dialog to add a new product.
        /// Single responsibility: only handles add product action.
        /// </summary>
        private void AddProduct()
        {
            // Kiểm tra quyền: Chỉ Admin mới được thêm sản phẩm
            if (_currentUser == null || _currentUser.Role != UserRole.Admin)
            {
                _errorHandler.ShowWarning("Chỉ quản trị viên mới có quyền thêm sản phẩm!");
                return;
            }

            using var dialog = new ProductEditDialog(null, _categoryService, _productService);
            if (dialog.ShowDialog(this) == DialogResult.OK && dialog.CreateProductDTO != null)
            {
                try
                {
                    // Save image first if provided (temporary file path)
                    string? savedImagePath = null;
                    if (!string.IsNullOrWhiteSpace(dialog.SelectedImagePath) && File.Exists(dialog.SelectedImagePath))
                    {
                        // We need to create product first to get ProductId, then save image
                        // So we'll create product without image, then update with image path
                        var tempDto = dialog.CreateProductDTO;
                        tempDto.ImagePath = null; // Clear temporary path
                        
                        _productService.CreateProduct(tempDto);
                        
                        // Get the created product ID (we need to query it)
                        var createdProduct = _productService.GetAllProducts()
                            .FirstOrDefault(p => p.ProductCode == tempDto.ProductCode);
                        
                        if (createdProduct != null)
                        {
                            savedImagePath = ImageHelper.SaveProductImage(dialog.SelectedImagePath, createdProduct.Id);
                            
                            // Update product with image path
                            var updateDto = new UpdateProductDTO
                            {
                                Id = createdProduct.Id,
                                ProductCode = createdProduct.ProductCode,
                                Name = createdProduct.Name,
                                CategoryId = createdProduct.CategoryId,
                                UnitPrice = createdProduct.UnitPrice,
                                Unit = createdProduct.Unit,
                                ImagePath = savedImagePath,
                                IsActive = createdProduct.IsActive
                            };
                            _productService.UpdateProduct(updateDto);
                        }
                    }
                    else
                    {
                        // No image, just create product
                        _productService.CreateProduct(dialog.CreateProductDTO);
                    }
                    
                    LoadProducts();
                    _errorHandler.ShowSuccess("Thêm sản phẩm thành công!");
                }
                catch (Exception ex)
                {
                    _errorHandler.ShowError(ex);
                }
            }
        }

        /// <summary>
        /// Opens dialog to edit selected product.
        /// Single responsibility: only handles edit product action.
        /// </summary>
        private void EditSelectedProduct()
        {
            // Kiểm tra quyền: Chỉ Admin mới được sửa sản phẩm
            if (_currentUser == null || _currentUser.Role != UserRole.Admin)
            {
                _errorHandler.ShowWarning("Chỉ quản trị viên mới có quyền sửa sản phẩm!");
                return;
            }

            if (grid.SelectedRows.Count == 0)
            {
                _errorHandler.ShowWarning("Vui lòng chọn sản phẩm cần sửa!");
                return;
            }

            var id = (int)grid.SelectedRows[0].Cells["Id"].Value;
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                ErrorHandler.ShowError("Không tìm thấy sản phẩm!");
                return;
            }

            using var dialog = new ProductEditDialog(product, _categoryService, _productService);
            if (dialog.ShowDialog(this) == DialogResult.OK && dialog.UpdateProductDTO != null)
            {
                try
                {
                    var updateDto = dialog.UpdateProductDTO;
                    var existingProduct = _productService.GetProductById(id);
                    
                    // Handle image upload/delete if needed
                    if (!string.IsNullOrWhiteSpace(dialog.SelectedImagePath) && File.Exists(dialog.SelectedImagePath))
                    {
                        // New image selected - save it and delete old one
                        if (!string.IsNullOrWhiteSpace(existingProduct?.ImagePath))
                        {
                            ImageHelper.DeleteProductImage(existingProduct.ImagePath);
                        }
                        updateDto.ImagePath = ImageHelper.SaveProductImage(dialog.SelectedImagePath, id);
                    }
                    else if (dialog.SelectedImagePath == string.Empty && existingProduct?.ImagePath != null)
                    {
                        // Image was removed - delete old image
                        ImageHelper.DeleteProductImage(existingProduct.ImagePath);
                        updateDto.ImagePath = null;
                    }
                    else
                    {
                        // Keep existing image
                        updateDto.ImagePath = existingProduct?.ImagePath;
                    }
                    
                    _productService.UpdateProduct(updateDto);
                    LoadProducts();
                    _errorHandler.ShowSuccess("Cập nhật sản phẩm thành công!");
                }
                catch (Exception ex)
                {
                    _errorHandler.ShowError(ex);
                }
            }
        }

        private void DeleteSelectedProduct()
        {
            // Kiểm tra quyền: Chỉ Admin mới được xóa sản phẩm
            if (_currentUser == null || _currentUser.Role != UserRole.Admin)
            {
                _errorHandler.ShowWarning("Chỉ quản trị viên mới có quyền xóa sản phẩm!");
                return;
            }

            if (grid.SelectedRows.Count == 0)
            {
                _errorHandler.ShowWarning("Vui lòng chọn sản phẩm cần xóa!");
                return;
            }

            var id = (int)grid.SelectedRows[0].Cells["Id"].Value;
            var productName = grid.SelectedRows[0].Cells["Name"].Value?.ToString() ?? "";

            if (_errorHandler.ShowConfirmation($"Bạn có chắc muốn xóa sản phẩm '{productName}'?\n\nLưu ý: Không thể xóa sản phẩm đã có trong đơn hàng."))
            {
                try
                {
                    _productService.DeleteProduct(id);
                    LoadProducts();
                    _errorHandler.ShowSuccess("Xóa sản phẩm thành công!");
                }
                catch (Exception ex)
                {
                    _errorHandler.ShowError(ex);
                }
            }
        }

        private void DeactivateSelectedProduct()
        {
            // Kiểm tra quyền: Chỉ Admin mới được ngừng kinh doanh sản phẩm
            if (_currentUser == null || _currentUser.Role != UserRole.Admin)
            {
                _errorHandler.ShowWarning("Chỉ quản trị viên mới có quyền ngừng kinh doanh sản phẩm!");
                return;
            }

            if (grid.SelectedRows.Count == 0)
            {
                _errorHandler.ShowWarning("Vui lòng chọn sản phẩm!");
                return;
            }

            var id = (int)grid.SelectedRows[0].Cells["Id"].Value;
            try
            {
                _productService.DeactivateProduct(id);
                LoadProducts();
                ErrorHandler.ShowSuccess("Ngừng kinh doanh sản phẩm thành công!");
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex);
            }
        }
    }

    // ProductEditDialog has been moved to separate file ProductEditDialog.cs
}
