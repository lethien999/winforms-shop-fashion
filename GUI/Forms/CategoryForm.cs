using System;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.DTO;
using WinFormsFashionShop.Presentation.Helpers;

namespace WinFormsFashionShop.Presentation.Forms
{
    public partial class CategoryForm : Form
    {
        private readonly ICategoryService _categoryService;

        public CategoryForm(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            InitializeComponent();
            InitializeControls();
            LoadCategories();
        }

        private void InitializeControls()
        {
            // Wire up event handlers for controls created in Designer
            btnSearch.Click += (s, e) => LoadCategories();
            grid.CellDoubleClick += (s, e) => EditSelectedCategory();
            btnAdd.Click += (s, e) => AddCategory();
            btnEdit.Click += (s, e) => EditSelectedCategory();
            btnDelete.Click += (s, e) => DeleteSelectedCategory();
            btnRefresh.Click += (s, e) => LoadCategories();
        }

        private void LoadCategories()
        {
            try
            {
                var categories = _categoryService.GetAllCategories().ToList();
                
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    var searchText = txtSearch.Text.ToLower();
                    categories = categories.Where(c => 
                        c.CategoryName.ToLower().Contains(searchText) ||
                        (c.Description != null && c.Description.ToLower().Contains(searchText))
                    ).ToList();
                }

                grid.DataSource = categories.Select(c => new
                {
                    c.Id,
                    c.CategoryName,
                    c.Description,
                    c.IsActive,
                    TrạngThái = c.IsActive ? "Hoạt động" : "Ngừng"
                }).ToList();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex);
            }
        }

        private void AddCategory()
        {
            using var dialog = new CategoryEditDialog(null);
            if (dialog.ShowDialog() == DialogResult.OK && dialog.CreateCategoryDTO != null)
            {
                try
                {
                    _categoryService.CreateCategory(dialog.CreateCategoryDTO);
                    LoadCategories();
                    ErrorHandler.ShowSuccess("Thêm danh mục thành công!");
                }
                catch (Exception ex)
                {
                    ErrorHandler.ShowError(ex);
                }
            }
        }

        private void EditSelectedCategory()
        {
            if (grid.SelectedRows.Count == 0)
            {
                ErrorHandler.ShowWarning("Vui lòng chọn danh mục cần sửa!");
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
            if (dialog.ShowDialog() == DialogResult.OK && dialog.UpdateCategoryDTO != null)
            {
                try
                {
                    _categoryService.UpdateCategory(dialog.UpdateCategoryDTO);
                    LoadCategories();
                    ErrorHandler.ShowSuccess("Cập nhật danh mục thành công!");
                }
                catch (Exception ex)
                {
                    ErrorHandler.ShowError(ex);
                }
            }
        }

        private void DeleteSelectedCategory()
        {
            if (grid.SelectedRows.Count == 0)
            {
                ErrorHandler.ShowWarning("Vui lòng chọn danh mục cần xóa!");
                return;
            }

            var id = (int)grid.SelectedRows[0].Cells["Id"].Value;
            var categoryName = grid.SelectedRows[0].Cells["CategoryName"].Value?.ToString() ?? "";

            if (ErrorHandler.ShowConfirmation($"Bạn có chắc muốn xóa danh mục '{categoryName}'?"))
            {
                try
                {
                    _categoryService.DeleteCategory(id);
                    LoadCategories();
                    ErrorHandler.ShowSuccess("Xóa danh mục thành công!");
                }
                catch (Exception ex)
                {
                    ErrorHandler.ShowError(ex);
                }
            }
        }
    }

    // Dialog for editing category
    public class CategoryEditDialog : Form
    {
        private TextBox? _txtName, _txtDescription;
        private CheckBox? _chkIsActive;
        private Button? _btnOK, _btnCancel;
        public CreateCategoryDTO? CreateCategoryDTO { get; private set; }
        public UpdateCategoryDTO? UpdateCategoryDTO { get; private set; }
        public object? CategoryDTO => (object?)CreateCategoryDTO ?? UpdateCategoryDTO;
        private CategoryDTO? _existingCategory;

        public CategoryEditDialog(CategoryDTO? category)
        {
            _existingCategory = category;
            Text = category == null ? "Thêm danh mục mới" : "Sửa danh mục";
            Width = 400;
            Height = 250;
            StartPosition = FormStartPosition.CenterParent;
            InitializeControls();
        }

        private void InitializeControls()
        {
            var lblName = new Label { Text = "Tên danh mục:", Left = 10, Top = 20, Width = 100 };
            _txtName = new TextBox { Left = 120, Top = 20, Width = 250, Text = _existingCategory?.CategoryName ?? "" };

            var lblDescription = new Label { Text = "Mô tả:", Left = 10, Top = 60, Width = 100 };
            _txtDescription = new TextBox { Left = 120, Top = 60, Width = 250, Height = 60, Multiline = true, Text = _existingCategory?.Description ?? "" };

            _chkIsActive = new CheckBox { Text = "Hoạt động", Left = 120, Top = 130, Checked = _existingCategory?.IsActive ?? true };

            _btnOK = new Button { Text = "OK", Left = 120, Top = 160, Width = 100, DialogResult = DialogResult.OK };
            _btnCancel = new Button { Text = "Hủy", Left = 230, Top = 160, Width = 100, DialogResult = DialogResult.Cancel };

            _btnOK.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(_txtName.Text))
                {
                    ErrorHandler.ShowWarning("Vui lòng nhập tên danh mục!");
                    DialogResult = DialogResult.None;
                    return;
                }

                if (_existingCategory == null)
                {
                    // Create new category
                    CreateCategoryDTO = new CreateCategoryDTO
                    {
                        CategoryName = _txtName?.Text.Trim() ?? "",
                        Description = string.IsNullOrWhiteSpace(_txtDescription?.Text) ? null : _txtDescription.Text.Trim()
                    };
                }
                else
                {
                    // Update existing category
                    UpdateCategoryDTO = new UpdateCategoryDTO
                    {
                        Id = _existingCategory.Id,
                        CategoryName = _txtName?.Text.Trim() ?? "",
                        Description = string.IsNullOrWhiteSpace(_txtDescription?.Text) ? null : _txtDescription.Text.Trim(),
                        IsActive = _chkIsActive?.Checked ?? true
                    };
                }
            };

            Controls.AddRange(new Control[] { lblName, _txtName, lblDescription, _txtDescription, _chkIsActive, _btnOK, _btnCancel });
        }
    }
}

