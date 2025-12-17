using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WinFormsFashionShop.Presentation.Helpers;

namespace WinFormsFashionShop.Presentation.Forms
{
    /// <summary>
    /// Dialog for previewing product images in full size.
    /// Single responsibility: only displays image preview.
    /// </summary>
    public partial class ImagePreviewDialog : Form
    {
        private string? _imagePath;
        private string? _productName;

        public ImagePreviewDialog(string imagePath, string? productName = null)
        {
            _imagePath = imagePath ?? throw new ArgumentNullException(nameof(imagePath));
            _productName = productName;
            InitializeComponent();
            InitializeControls();
            LoadImage();
        }

        /// <summary>
        /// Initializes event handlers and sets initial values.
        /// Single responsibility: only wires up event handlers and sets initial data.
        /// </summary>
        private void InitializeControls()
        {
            // Set title
            Text = _productName != null ? $"Ảnh sản phẩm - {_productName}" : "Xem ảnh sản phẩm";
            
            // Set product name label
            if (!string.IsNullOrWhiteSpace(_productName))
            {
                _lblProductName!.Text = _productName;
            }
            else
            {
                _lblProductName!.Text = "XEM ẢNH SẢN PHẨM";
            }

            // Double-click to close
            _picImage!.DoubleClick += (s, e) => Close();

            // Handle ESC key
            KeyPreview = true;
            KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                    Close();
            };
        }


        /// <summary>
        /// Loads and displays the image.
        /// Single responsibility: only loads and displays image.
        /// </summary>
        private void LoadImage()
        {
            try
            {
                Image? image = null;

                // Try to load from file path
                if (File.Exists(_imagePath))
                {
                    image = Image.FromFile(_imagePath);
                }
                else
                {
                    // Try to load using ImageHelper
                    image = ImageHelper.LoadProductImage(_imagePath);
                }

                if (image == null)
                {
                    ErrorHandler.ShowWarning("Không thể tải ảnh!");
                    Close();
                    return;
                }

                // Display image
                _picImage!.Image = image;

                // Adjust form size to fit image (with max limits)
                var primaryScreen = Screen.PrimaryScreen;
                var maxWidth = (primaryScreen?.WorkingArea.Width ?? 1920) * 0.9f;
                var maxHeight = (primaryScreen?.WorkingArea.Height ?? 1080) * 0.9f;

                var imageWidth = Math.Min(image.Width + 40, maxWidth);
                var imageHeight = Math.Min(image.Height + 100, maxHeight);

                Width = (int)imageWidth;
                Height = (int)imageHeight;
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Không thể tải ảnh: {ex.Message}");
                Close();
            }
        }

    }
}

