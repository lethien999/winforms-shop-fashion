using System;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Constants;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.DTO;
using WinFormsFashionShop.Presentation.Helpers;
using UIThemeConstants = WinFormsFashionShop.Presentation.Helpers.UIThemeConstants;

namespace WinFormsFashionShop.Presentation.Forms
{
    public partial class CategoryForm : Form
    {
        private readonly ICategoryService _categoryService;
        private readonly IErrorHandler _errorHandler;
        private readonly UserDTO? _currentUser;

        public CategoryForm(ICategoryService categoryService, IErrorHandler errorHandler, UserDTO? currentUser = null)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
            _currentUser = currentUser;
            InitializeComponent();
            InitializeControls();
            LoadCategories();
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
            UIThemeConstants.ThemeHelper.ApplyGridStyle(grid);

            // Wire up event handlers for controls created in Designer
            btnSearch.Click += (s, e) => LoadCategories();
            grid.CellDoubleClick += (s, e) => EditSelectedCategory();
            btnAdd.Click += (s, e) => AddCategory();
            btnEdit.Click += (s, e) => EditSelectedCategory();
            btnDelete.Click += (s, e) => DeleteSelectedCategory();
            btnRefresh.Click += (s, e) => LoadCategories();

            // Kiểm tra quyền: Chỉ Admin mới được thêm/sửa/xóa danh mục
            if (_currentUser == null || _currentUser.Role != UserRole.Admin)
            {
                btnAdd.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
            }
        }

        private void LoadCategories()
        {
            try
            {
                if (grid == null)
                {
                    _errorHandler.ShowError("Grid chưa được khởi tạo!");
                    return;
                }

                var allCategories = _categoryService.GetAllCategories();
                if (allCategories == null)
                {
                    _errorHandler.ShowWarning("Không thể tải danh sách danh mục. Dịch vụ trả về null.");
                    grid.DataSource = null;
                    return;
                }

                var categories = allCategories.ToList();
                
                if (txtSearch != null && !string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    var searchText = txtSearch.Text?.ToLower() ?? string.Empty;
                    categories = categories.Where(c => 
                        c != null &&
                        ((c.CategoryName?.ToLower().Contains(searchText) ?? false) ||
                        (c.Description?.ToLower().Contains(searchText) ?? false))
                    ).ToList();
                }

                if (categories == null)
                {
                    grid.DataSource = new List<object>();
                    return;
                }

                var categoryList = categories.Where(c => c != null).Select(c => new
                {
                    c.Id,
                    c.CategoryName,
                    c.Description,
                    c.IsActive,
                    TrạngThái = c.IsActive ? "Hoạt động" : "Ngừng"
                }).ToList();
                grid.DataSource = categoryList;
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError($"Lỗi khi tải danh sách danh mục: {ex.Message}");
                if (grid != null)
                {
                    grid.DataSource = null;
                }
            }
        }

        private void AddCategory()
        {
            // Kiểm tra quyền: Chỉ Admin mới được thêm danh mục
            if (_currentUser == null || _currentUser.Role != UserRole.Admin)
            {
                _errorHandler.ShowWarning("Chỉ quản trị viên mới có quyền thêm danh mục!");
                return;
            }

            using var dialog = new CategoryEditDialog(null);
            if (dialog.ShowDialog(this) == DialogResult.OK && dialog.CreateCategoryDTO != null)
            {
                try
                {
                    _categoryService.CreateCategory(dialog.CreateCategoryDTO);
                    LoadCategories();
                    _errorHandler.ShowSuccess("Thêm danh mục thành công!");
                }
                catch (Exception ex)
                {
                    _errorHandler.ShowError(ex);
                }
            }
        }

        private void EditSelectedCategory()
        {
            // Kiểm tra quyền: Chỉ Admin mới được sửa danh mục
            if (_currentUser == null || _currentUser.Role != UserRole.Admin)
            {
                _errorHandler.ShowWarning("Chỉ quản trị viên mới có quyền sửa danh mục!");
                return;
            }

            if (grid.SelectedRows.Count == 0)
            {
                _errorHandler.ShowWarning("Vui lòng chọn danh mục cần sửa!");
                return;
            }

            var id = (int)grid.SelectedRows[0].Cells["Id"].Value;
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                ErrorHandler.ShowError("Không tìm thấy danh mục!");
                return;
            }

            using var dialog = new CategoryEditDialog(category);
            if (dialog.ShowDialog(this) == DialogResult.OK && dialog.UpdateCategoryDTO != null)
            {
                try
                {
                    _categoryService.UpdateCategory(dialog.UpdateCategoryDTO);
                    LoadCategories();
                    _errorHandler.ShowSuccess("Cập nhật danh mục thành công!");
                }
                catch (Exception ex)
                {
                    _errorHandler.ShowError(ex);
                }
            }
        }

        private void DeleteSelectedCategory()
        {
            // Kiểm tra quyền: Chỉ Admin mới được xóa danh mục
            if (_currentUser == null || _currentUser.Role != UserRole.Admin)
            {
                _errorHandler.ShowWarning("Chỉ quản trị viên mới có quyền xóa danh mục!");
                return;
            }

            if (grid.SelectedRows.Count == 0)
            {
                _errorHandler.ShowWarning("Vui lòng chọn danh mục cần xóa!");
                return;
            }

            var id = (int)grid.SelectedRows[0].Cells["Id"].Value;
            var categoryName = grid.SelectedRows[0].Cells["CategoryName"].Value?.ToString() ?? "";

            if (_errorHandler.ShowConfirmation($"Bạn có chắc muốn xóa danh mục '{categoryName}'?"))
            {
                try
                {
                    _categoryService.DeleteCategory(id);
                    LoadCategories();
                    _errorHandler.ShowSuccess("Xóa danh mục thành công!");
                }
                catch (Exception ex)
                {
                    _errorHandler.ShowError(ex);
                }
            }
        }
    }

}

