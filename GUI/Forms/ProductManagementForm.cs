using System;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.DTO;

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

        private void LoadCategoriesForFilter()
        {
            try
            {
                var categories = _categoryService.GetAllCategories().ToList();
                cmbCategoryFilter.Items.Clear();
                cmbCategoryFilter.Items.Add("Tất cả danh mục");
                foreach (var cat in categories)
                {
                    cmbCategoryFilter.Items.Add(cat);
                }
                cmbCategoryFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh mục: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show($"Lỗi tải sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddProduct()
        {
            using var dialog = new ProductEditDialog(null, _categoryService);
            if (dialog.ShowDialog() == DialogResult.OK && dialog.CreateProductDTO != null)
            {
                try
                {
                    _productService.CreateProduct(dialog.CreateProductDTO);
                    LoadProducts();
                    MessageBox.Show("Thêm sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi thêm sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void EditSelectedProduct()
        {
            if (grid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var id = (int)grid.SelectedRows[0].Cells["Id"].Value;
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                MessageBox.Show("Không tìm thấy sản phẩm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using var dialog = new ProductEditDialog(product, _categoryService);
            if (dialog.ShowDialog() == DialogResult.OK && dialog.UpdateProductDTO != null)
            {
                try
                {
                    _productService.UpdateProduct(dialog.UpdateProductDTO);
                    LoadProducts();
                    MessageBox.Show("Cập nhật sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi cập nhật sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DeleteSelectedProduct()
        {
            if (grid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var id = (int)grid.SelectedRows[0].Cells["Id"].Value;
            var productName = grid.SelectedRows[0].Cells["Name"].Value?.ToString() ?? "";

            if (MessageBox.Show($"Bạn có chắc muốn xóa sản phẩm '{productName}'?\n\nLưu ý: Không thể xóa sản phẩm đã có trong đơn hàng.", 
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _productService.DeleteProduct(id);
                    LoadProducts();
                    MessageBox.Show("Xóa sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi xóa sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DeactivateSelectedProduct()
        {
            if (grid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var id = (int)grid.SelectedRows[0].Cells["Id"].Value;
            try
            {
                _productService.DeactivateProduct(id);
                LoadProducts();
                MessageBox.Show("Ngừng kinh doanh sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // Dialog for editing product
    public class ProductEditDialog : Form
    {
        private TextBox? _txtCode, _txtName, _txtPrice, _txtUnit;
        private ComboBox? _cmbCategory;
        private CheckBox? _chkIsActive;
        private Button? _btnOK, _btnCancel;
        private readonly ICategoryService _categoryService;
        public CreateProductDTO? CreateProductDTO { get; private set; }
        public UpdateProductDTO? UpdateProductDTO { get; private set; }
        public object? ProductDTO => (object?)CreateProductDTO ?? UpdateProductDTO;
        private ProductDTO? _existingProduct;

        public ProductEditDialog(ProductDTO? product, ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _existingProduct = product;
            Text = product == null ? "Thêm sản phẩm mới" : "Sửa sản phẩm";
            Width = 450;
            Height = 350;
            StartPosition = FormStartPosition.CenterParent;
            InitializeControls();
        }

        private void InitializeControls()
        {
            var lblCode = new Label { Text = "Mã SP:", Left = 10, Top = 20, Width = 100 };
            _txtCode = new TextBox { Left = 120, Top = 20, Width = 250, Text = _existingProduct?.ProductCode ?? "" };

            var lblName = new Label { Text = "Tên SP:", Left = 10, Top = 60, Width = 100 };
            _txtName = new TextBox { Left = 120, Top = 60, Width = 250, Text = _existingProduct?.Name ?? "" };

            var lblCategory = new Label { Text = "Danh mục:", Left = 10, Top = 100, Width = 100 };
            _cmbCategory = new ComboBox { Left = 120, Top = 100, Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };
            LoadCategories();

            var lblPrice = new Label { Text = "Giá bán:", Left = 10, Top = 140, Width = 100 };
            _txtPrice = new TextBox { Left = 120, Top = 140, Width = 250, Text = _existingProduct?.UnitPrice.ToString() ?? "0" };

            var lblUnit = new Label { Text = "Đơn vị:", Left = 10, Top = 180, Width = 100 };
            _txtUnit = new TextBox { Left = 120, Top = 180, Width = 250, Text = _existingProduct?.Unit ?? "cái" };

            _chkIsActive = new CheckBox { Text = "Hoạt động", Left = 120, Top = 220, Checked = _existingProduct?.IsActive ?? true };

            _btnOK = new Button { Text = "OK", Left = 120, Top = 260, Width = 100, DialogResult = DialogResult.OK };
            _btnCancel = new Button { Text = "Hủy", Left = 230, Top = 260, Width = 100, DialogResult = DialogResult.Cancel };

            _btnOK.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(_txtCode?.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã sản phẩm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                    return;
                }
                if (string.IsNullOrWhiteSpace(_txtName?.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên sản phẩm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                    return;
                }
                if (_cmbCategory?.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn danh mục!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                    return;
                }
                if (!decimal.TryParse(_txtPrice?.Text, out var price) || price < 0)
                {
                    MessageBox.Show("Giá bán không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        Unit = _txtUnit?.Text.Trim() ?? "cái"
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
                        IsActive = _chkIsActive?.Checked ?? true
                    };
                }
            };

            Controls.AddRange(new Control[] { 
                lblCode, _txtCode, lblName, _txtName, lblCategory, _cmbCategory,
                lblPrice, _txtPrice, lblUnit, _txtUnit, _chkIsActive, _btnOK, _btnCancel 
            });
        }

        private void LoadCategories()
        {
            try
            {
                var categories = _categoryService.GetAllCategories().ToList();
                _cmbCategory?.Items.Clear();
                if (_cmbCategory != null)
                {
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
                MessageBox.Show($"Lỗi tải danh mục: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
