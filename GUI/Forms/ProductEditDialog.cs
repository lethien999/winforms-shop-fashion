using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.DTO;
using WinFormsFashionShop.Presentation.Helpers;

namespace WinFormsFashionShop.Presentation.Forms
{
    /// <summary>
    /// Dialog for editing product (add/edit).
    /// </summary>
    public partial class ProductEditDialog : Form
    {
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
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            Text = _existingProduct == null ? "Thêm sản phẩm mới" : "Sửa sản phẩm";

            // Auto-generate product code for new products
            if (_existingProduct == null)
            {
                txtCode.ReadOnly = true;
                txtCode.BackColor = SystemColors.Control;
                txtCode.Text = _productService.GenerateProductCode();
            }
            else
            {
                txtCode.Text = _existingProduct.ProductCode ?? "";
            }

            // Set initial values
            if (_existingProduct != null)
            {
                txtName.Text = _existingProduct.Name ?? "";
                txtPrice.Text = _existingProduct.UnitPrice.ToString("N0");
                txtUnit.Text = _existingProduct.Unit ?? "cái";
                chkIsActive.Checked = _existingProduct.IsActive;
                LoadExistingImage();
            }
            else
            {
                txtPrice.Text = "0";
                txtUnit.Text = "cái";
                chkIsActive.Checked = true;
            }

            // Load categories
            LoadCategories();

            // Setup price input validation
            txtPrice.KeyPress += TxtPrice_KeyPress;

            // Wire up event handlers
            btnSelectImage.Click += (s, e) => SelectImage();
            btnRemoveImage.Click += (s, e) => RemoveImage();
            btnOK.Click += BtnOK_Click;
        }

        private void TxtPrice_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Cho phép: số, Backspace, Delete, Tab, Enter, dấu phẩy, dấu chấm
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && 
                e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            // Chỉ cho phép một dấu phẩy hoặc chấm
            if ((e.KeyChar == ',' || e.KeyChar == '.') && 
                (txtPrice.Text.Contains(',') || txtPrice.Text.Contains('.')))
            {
                e.Handled = true;
            }
        }

        private void BtnOK_Click(object? sender, EventArgs e)
        {
            // Product code is auto-generated for new products, so no need to validate
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ErrorHandler.ShowWarning("Vui lòng nhập tên sản phẩm!");
                DialogResult = DialogResult.None;
                return;
            }
            if (cmbCategory.SelectedItem == null)
            {
                ErrorHandler.ShowWarning("Vui lòng chọn danh mục!");
                DialogResult = DialogResult.None;
                return;
            }
            // Parse giá bán, hỗ trợ cả dấu phẩy và dấu chấm làm phân cách phần nghìn
            var priceText = txtPrice.Text?.Trim() ?? "0";
            // Loại bỏ dấu phẩy và dấu chấm (dùng làm phân cách phần nghìn)
            priceText = priceText.Replace(",", "").Replace(".", "");
            
            if (!decimal.TryParse(priceText, System.Globalization.NumberStyles.Number, 
                System.Globalization.CultureInfo.InvariantCulture, out var price) || price < 0)
            {
                ErrorHandler.ShowWarning("Giá bán không hợp lệ! Vui lòng nhập số dương.");
                DialogResult = DialogResult.None;
                txtPrice.Focus();
                return;
            }
            
            // Kiểm tra giá quá lớn (tránh overflow)
            if (price > 999999999999) // Giới hạn 999 tỷ
            {
                ErrorHandler.ShowWarning("Giá bán quá lớn! Vui lòng nhập giá nhỏ hơn 999,999,999,999 VNĐ.");
                DialogResult = DialogResult.None;
                txtPrice.Focus();
                return;
            }

            if (_existingProduct == null)
            {
                // Create new product
                CreateProductDTO = new CreateProductDTO
                {
                    ProductCode = txtCode.Text.Trim(),
                    Name = txtName.Text.Trim(),
                    CategoryId = ((CategoryDTO)cmbCategory.SelectedItem).Id,
                    UnitPrice = price,
                    Unit = txtUnit?.Text.Trim() ?? "cái",
                    ImagePath = SelectedImagePath // Lưu đường dẫn tạm thời, sẽ được xử lý sau khi tạo product
                };
            }
            else
            {
                // Update existing product
                UpdateProductDTO = new UpdateProductDTO
                {
                    Id = _existingProduct.Id,
                    ProductCode = txtCode.Text.Trim(),
                    Name = txtName.Text.Trim(),
                    CategoryId = ((CategoryDTO)cmbCategory.SelectedItem).Id,
                    UnitPrice = price,
                    Unit = txtUnit?.Text.Trim() ?? "cái",
                    ImagePath = SelectedImagePath ?? _existingProduct.ImagePath, // Giữ ảnh cũ nếu không chọn ảnh mới
                    IsActive = chkIsActive.Checked
                };
            }
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

            if (dialog.ShowDialog(this) == DialogResult.OK)
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
            picProductImage.Image?.Dispose();
            picProductImage.Image = null;
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
                    picProductImage.Image?.Dispose();
                    picProductImage.Image = ImageHelper.ResizeImage(image, 150, 150);
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
                cmbCategory.Items.Clear();
                
                // Set display member to show CategoryName instead of object type
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember = "Id";
                
                foreach (var cat in categories)
                {
                    cmbCategory.Items.Add(cat);
                    if (_existingProduct != null && _existingProduct.CategoryId == cat.Id)
                        cmbCategory.SelectedItem = cat;
                }
                
                if (cmbCategory.SelectedItem == null && cmbCategory.Items.Count > 0)
                    cmbCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Không thể tải danh mục: {ex.Message}");
            }
        }
    }
}
