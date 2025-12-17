using System;
using System.Windows.Forms;
using WinFormsFashionShop.DTO;
using WinFormsFashionShop.Presentation.Helpers;

namespace WinFormsFashionShop.Presentation.Forms
{
    /// <summary>
    /// Dialog for editing category (add/edit).
    /// </summary>
    public partial class CategoryEditDialog : Form
    {
        public CreateCategoryDTO? CreateCategoryDTO { get; private set; }
        public UpdateCategoryDTO? UpdateCategoryDTO { get; private set; }
        public object? CategoryDTO => (object?)CreateCategoryDTO ?? UpdateCategoryDTO;
        private CategoryDTO? _existingCategory;

        public CategoryEditDialog(CategoryDTO? category)
        {
            _existingCategory = category;
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            Text = _existingCategory == null ? "Thêm danh mục mới" : "Sửa danh mục";
            
            // Set initial values
            if (_existingCategory != null)
            {
                txtName.Text = _existingCategory.CategoryName ?? "";
                txtDescription.Text = _existingCategory.Description ?? "";
                chkIsActive.Checked = _existingCategory.IsActive;
            }
            else
            {
                chkIsActive.Checked = true;
            }

            // Wire up event handlers
            btnOK.Click += BtnOK_Click;
        }

        private void BtnOK_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
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
                    CategoryName = txtName.Text.Trim(),
                    Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim()
                };
            }
            else
            {
                // Update existing category
                UpdateCategoryDTO = new UpdateCategoryDTO
                {
                    Id = _existingCategory.Id,
                    CategoryName = txtName.Text.Trim(),
                    Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim(),
                    IsActive = chkIsActive.Checked
                };
            }
        }
    }
}
