using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.DTO;
using WinFormsFashionShop.Presentation.Helpers;

namespace WinFormsFashionShop.Presentation.Forms
{
    public partial class ProductManagementForm : Form
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductManagementForm(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            InitializeComponent();
            InitializeControls();
            LoadProducts();
        }

        private void InitializeControls()
        {
            // Load categories for filter
            LoadCategoriesForFilter();

            // Wire up event handlers
            cmbCategoryFilter.SelectedIndexChanged += (s, e) => LoadProducts();
            btnSearch.Click += (s, e) => LoadProducts();
            grid.CellDoubleClick += (s, e) => EditSelectedProduct();
            btnAdd.Click += (s, e) => AddProduct();
            btnEdit.Click += (s, e) => EditSelectedProduct();
            btnDelete.Click += (s, e) => DeleteSelectedProduct();
            btnDeactivate.Click += (s, e) => DeactivateSelectedProduct();
            btnRefresh.Click += (s, e) => LoadProducts();
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
                var products = _productService.GetAllProducts().ToList();
                
                // Filter by search text
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    var searchText = txtSearch.Text.ToLower();
                    products = products.Where(p => 
                        p.ProductCode.ToLower().Contains(searchText) ||
                        p.Name.ToLower().Contains(searchText)
                    ).ToList();
                }

                // Filter by category
                if (cmbCategoryFilter.SelectedIndex > 0 && cmbCategoryFilter.SelectedItem is CategoryDTO selectedCategory)
                {
                    products = products.Where(p => p.CategoryId == selectedCategory.Id).ToList();
                }

                grid.DataSource = products.Select(p => new
                {
                    p.Id,
                    p.ProductCode,
                    p.Name,
                    DanhMục = p.CategoryName ?? "",
                    p.UnitPrice,
                    p.Unit,
                    p.IsActive,
                    TrạngThái = p.IsActive ? "Hoạt động" : "Ngừng"
                }).ToList();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex);
            }
        }

        /// <summary>
        /// Opens dialog to add a new product.
        /// Single responsibility: only handles add product action.
        /// </summary>
        private void AddProduct()
        {
            using var dialog = new ProductEditDialog(null, _categoryService, _productService);
            if (dialog.ShowDialog() == DialogResult.OK && dialog.CreateProductDTO != null)
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
                    ErrorHandler.ShowSuccess("Thêm sản phẩm thành công!");
                }
                catch (Exception ex)
                {
                    ErrorHandler.ShowError(ex);
                }
            }
        }

        /// <summary>
        /// Opens dialog to edit selected product.
        /// Single responsibility: only handles edit product action.
        /// </summary>
        private void EditSelectedProduct()
        {
            if (grid.SelectedRows.Count == 0)
            {
                ErrorHandler.ShowWarning("Vui lòng chọn sản phẩm cần sửa!");
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
            if (dialog.ShowDialog() == DialogResult.OK && dialog.UpdateProductDTO != null)
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
                    ErrorHandler.ShowSuccess("Cập nhật sản phẩm thành công!");
                }
                catch (Exception ex)
                {
                    ErrorHandler.ShowError(ex);
                }
            }
        }

        private void DeleteSelectedProduct()
        {
            if (grid.SelectedRows.Count == 0)
            {
                ErrorHandler.ShowWarning("Vui lòng chọn sản phẩm cần xóa!");
                return;
            }

            var id = (int)grid.SelectedRows[0].Cells["Id"].Value;
            var productName = grid.SelectedRows[0].Cells["Name"].Value?.ToString() ?? "";

            if (ErrorHandler.ShowConfirmation($"Bạn có chắc muốn xóa sản phẩm '{productName}'?\n\nLưu ý: Không thể xóa sản phẩm đã có trong đơn hàng."))
            {
                try
                {
                    _productService.DeleteProduct(id);
                    LoadProducts();
                    ErrorHandler.ShowSuccess("Xóa sản phẩm thành công!");
                }
                catch (Exception ex)
                {
                    ErrorHandler.ShowError(ex);
                }
            }
        }

        private void DeactivateSelectedProduct()
        {
            if (grid.SelectedRows.Count == 0)
            {
                ErrorHandler.ShowWarning("Vui lòng chọn sản phẩm!");
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

    // Dialog for editing product
    public class ProductEditDialog : Form
    {
        private TextBox? _txtCode, _txtName, _txtPrice, _txtUnit;
        private ComboBox? _cmbCategory;
        private CheckBox? _chkIsActive;
        private Button? _btnOK, _btnCancel, _btnSelectImage, _btnRemoveImage;
        private PictureBox? _picProductImage;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        public CreateProductDTO? CreateProductDTO { get; private set; }
        public UpdateProductDTO? UpdateProductDTO { get; private set; }
        public string? SelectedImagePath { get; private set; } // Đường dẫn file ảnh được chọn (tạm thời)
        public object? ProductDTO => (object?)CreateProductDTO ?? UpdateProductDTO;
        private ProductDTO? _existingProduct;

        public ProductEditDialog(ProductDTO? product, ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _existingProduct = product;
            Text = product == null ? "Thêm sản phẩm mới" : "Sửa sản phẩm";
            Width = 600;
            Height = 500;
            StartPosition = FormStartPosition.CenterParent;
            InitializeControls();
        }

        private void InitializeControls()
        {
            var lblCode = new Label { Text = "Mã SP:", Left = 10, Top = 20, Width = 100 };
            _txtCode = new TextBox { Left = 120, Top = 20, Width = 250, Text = _existingProduct?.ProductCode ?? "" };
            
            // Auto-generate product code for new products
            if (_existingProduct == null)
            {
                _txtCode.ReadOnly = true;
                _txtCode.BackColor = System.Drawing.SystemColors.Control;
                _txtCode.Text = _productService.GenerateProductCode();
            }

            var lblName = new Label { Text = "Tên SP:", Left = 10, Top = 60, Width = 100 };
            _txtName = new TextBox { Left = 120, Top = 60, Width = 250, Text = _existingProduct?.Name ?? "" };

            var lblCategory = new Label { Text = "Danh mục:", Left = 10, Top = 100, Width = 100 };
            _cmbCategory = new ComboBox { Left = 120, Top = 100, Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };
            LoadCategories();

            var lblPrice = new Label { Text = "Giá bán:", Left = 10, Top = 140, Width = 100 };
            _txtPrice = new TextBox { Left = 120, Top = 140, Width = 250, Text = _existingProduct?.UnitPrice.ToString() ?? "0" };

            var lblUnit = new Label { Text = "Đơn vị:", Left = 10, Top = 180, Width = 100 };
            _txtUnit = new TextBox { Left = 120, Top = 180, Width = 250, Text = _existingProduct?.Unit ?? "cái" };

            // Image section
            var lblImage = new Label { Text = "Ảnh SP:", Left = 10, Top = 220, Width = 100 };
            _picProductImage = new PictureBox 
            { 
                Left = 120, 
                Top = 220, 
                Width = 150, 
                Height = 150, 
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            LoadExistingImage();

            _btnSelectImage = new Button { Text = "Chọn ảnh", Left = 280, Top = 220, Width = 90, Height = 30 };
            _btnSelectImage.Click += (s, e) => SelectImage();

            _btnRemoveImage = new Button { Text = "Xóa ảnh", Left = 280, Top = 260, Width = 90, Height = 30 };
            _btnRemoveImage.Click += (s, e) => RemoveImage();

            _chkIsActive = new CheckBox { Text = "Hoạt động", Left = 120, Top = 380, Checked = _existingProduct?.IsActive ?? true };

            _btnOK = new Button { Text = "OK", Left = 120, Top = 420, Width = 100, DialogResult = DialogResult.OK };
            _btnCancel = new Button { Text = "Hủy", Left = 230, Top = 420, Width = 100, DialogResult = DialogResult.Cancel };

            _btnOK.Click += (s, e) =>
            {
                // Product code is auto-generated for new products, so no need to validate
                if (string.IsNullOrWhiteSpace(_txtName?.Text))
                {
                    ErrorHandler.ShowWarning("Vui lòng nhập tên sản phẩm!");
                    DialogResult = DialogResult.None;
                    return;
                }
                if (_cmbCategory?.SelectedItem == null)
                {
                    ErrorHandler.ShowWarning("Vui lòng chọn danh mục!");
                    DialogResult = DialogResult.None;
                    return;
                }
                if (!decimal.TryParse(_txtPrice?.Text, out var price) || price < 0)
                {
                    ErrorHandler.ShowWarning("Giá bán không hợp lệ!");
                    DialogResult = DialogResult.None;
                    return;
                }

                if (_existingProduct == null)
                {
                    // Create new product
                    CreateProductDTO = new CreateProductDTO
                    {
                        ProductCode = _txtCode.Text.Trim(),
                        Name = _txtName.Text.Trim(),
                        CategoryId = ((CategoryDTO)_cmbCategory.SelectedItem).Id,
                        UnitPrice = price,
                        Unit = _txtUnit?.Text.Trim() ?? "cái",
                        ImagePath = SelectedImagePath // Lưu đường dẫn tạm thời, sẽ được xử lý sau khi tạo product
                    };
                }
                else
                {
                    // Update existing product
                    UpdateProductDTO = new UpdateProductDTO
                    {
                        Id = _existingProduct.Id,
                        ProductCode = _txtCode.Text.Trim(),
                        Name = _txtName.Text.Trim(),
                        CategoryId = ((CategoryDTO)_cmbCategory.SelectedItem).Id,
                        UnitPrice = price,
                        Unit = _txtUnit?.Text.Trim() ?? "cái",
                        ImagePath = SelectedImagePath ?? _existingProduct.ImagePath, // Giữ ảnh cũ nếu không chọn ảnh mới
                        IsActive = _chkIsActive?.Checked ?? true
                    };
                }
            };

            Controls.AddRange(new Control[] { 
                lblCode, _txtCode, lblName, _txtName, lblCategory, _cmbCategory,
                lblPrice, _txtPrice, lblUnit, _txtUnit, 
                lblImage, _picProductImage, _btnSelectImage, _btnRemoveImage,
                _chkIsActive, _btnOK, _btnCancel 
            });
        }

        /// <summary>
        /// Opens file dialog to select product image.
        /// Single responsibility: only handles image selection.
        /// </summary>
        private void SelectImage()
        {
            using var dialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.webp|All Files|*.*",
                Title = "Chọn ảnh sản phẩm"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (!ImageHelper.IsValidImageFile(dialog.FileName))
                    {
                        ErrorHandler.ShowWarning("Định dạng file không được hỗ trợ. Chỉ chấp nhận: JPG, JPEG, PNG, GIF, BMP, WEBP");
                        return;
                    }

                    if (!ImageHelper.IsValidFileSize(dialog.FileName))
                    {
                        ErrorHandler.ShowWarning("Kích thước file quá lớn. Tối đa: 5MB");
                        return;
                    }

                    SelectedImagePath = dialog.FileName;
                    DisplayImage(dialog.FileName);
                }
                catch (Exception ex)
                {
                    ErrorHandler.ShowError(ex);
                }
            }
        }

        /// <summary>
        /// Removes selected image.
        /// Single responsibility: only removes image.
        /// </summary>
        private void RemoveImage()
        {
            SelectedImagePath = null;
            _picProductImage?.Image?.Dispose();
            _picProductImage!.Image = null;
        }

        /// <summary>
        /// Displays image in PictureBox.
        /// Single responsibility: only displays image.
        /// </summary>
        private void DisplayImage(string imagePath)
        {
            try
            {
                Image? image = null;
                
                // If it's a full path (file exists), load directly
                if (File.Exists(imagePath))
                {
                    image = Image.FromFile(imagePath);
                }
                else
                {
                    // Otherwise, treat as relative path from database
                    image = ImageHelper.LoadProductImage(imagePath);
                }
                
                if (image != null)
                {
                    _picProductImage?.Image?.Dispose();
                    _picProductImage!.Image = ImageHelper.ResizeImage(image, 150, 150);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Không thể tải ảnh: {ex.Message}");
            }
        }

        /// <summary>
        /// Loads existing product image if available.
        /// Single responsibility: only loads existing image.
        /// </summary>
        private void LoadExistingImage()
        {
            if (_existingProduct?.ImagePath != null)
            {
                DisplayImage(_existingProduct.ImagePath);
            }
        }

        /// <summary>
        /// Loads categories into the ComboBox with proper display.
        /// Single responsibility: only loads and configures categories.
        /// </summary>
        private void LoadCategories()
        {
            try
            {
                var categories = _categoryService.GetAllCategories().Where(c => c.IsActive).ToList();
                _cmbCategory?.Items.Clear();
                
                if (_cmbCategory != null)
                {
                    // Set display member to show CategoryName instead of object type
                    _cmbCategory.DisplayMember = "CategoryName";
                    _cmbCategory.ValueMember = "Id";
                    
                    foreach (var cat in categories)
                    {
                        _cmbCategory.Items.Add(cat);
                        if (_existingProduct != null && _existingProduct.CategoryId == cat.Id)
                            _cmbCategory.SelectedItem = cat;
                    }
                    
                    if (_cmbCategory.SelectedItem == null && _cmbCategory.Items.Count > 0)
                        _cmbCategory.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex);
            }
        }
    }
}
