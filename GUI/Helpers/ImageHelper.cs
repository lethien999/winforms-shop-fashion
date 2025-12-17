using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using WinFormsFashionShop.Business.Constants;

namespace WinFormsFashionShop.Presentation.Helpers
{
    /// <summary>
    /// Helper class for handling image uploads and file operations for all image types.
    /// Follows Single Responsibility Principle - only handles image file operations.
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>
        /// Supported image file extensions.
        /// </summary>
        private static readonly string[] SupportedExtensions = ImageConstants.SupportedExtensions;

        /// <summary>
        /// Maximum file size in bytes (5MB).
        /// </summary>
        private const long MaxFileSize = ImageConstants.MaxFileSize;

        /// <summary>
        /// Base directory for storing all application images.
        /// </summary>
        private static readonly string BaseImageDirectory = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, 
            ImageConstants.ImagesBaseFolder);

        /// <summary>
        /// Gets the directory path for a specific image type.
        /// Single responsibility: only resolves directory path.
        /// </summary>
        private static string GetImageDirectory(string imageTypeFolder)
        {
            return Path.Combine(BaseImageDirectory, imageTypeFolder);
        }

        /// <summary>
        /// Validates if the file is a supported image format.
        /// Single responsibility: only validates file extension.
        /// </summary>
        public static bool IsValidImageFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return false;

            var extension = Path.GetExtension(filePath)?.ToLowerInvariant();
            return !string.IsNullOrEmpty(extension) && SupportedExtensions.Contains(extension);
        }

        /// <summary>
        /// Validates if the file size is within acceptable limits.
        /// Single responsibility: only validates file size.
        /// </summary>
        public static bool IsValidFileSize(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                return false;

            var fileInfo = new FileInfo(filePath);
            return fileInfo.Length <= MaxFileSize;
        }

        /// <summary>
        /// Saves product image to the Images/Products directory.
        /// Single responsibility: only saves product image file.
        /// </summary>
        /// <param name="sourceFilePath">Path to the source image file</param>
        /// <param name="productId">Product ID to use in filename</param>
        /// <returns>Relative path to the saved image, or null if save failed</returns>
        public static string? SaveProductImage(string sourceFilePath, int productId)
        {
            return SaveImage(sourceFilePath, ImageConstants.ImageFolders.Products, productId.ToString());
        }

        /// <summary>
        /// Saves category image to the Images/Categories directory.
        /// Single responsibility: only saves category image file.
        /// </summary>
        /// <param name="sourceFilePath">Path to the source image file</param>
        /// <param name="categoryId">Category ID to use in filename</param>
        /// <returns>Relative path to the saved image, or null if save failed</returns>
        public static string? SaveCategoryImage(string sourceFilePath, int categoryId)
        {
            return SaveImage(sourceFilePath, ImageConstants.ImageFolders.Categories, categoryId.ToString());
        }

        /// <summary>
        /// Saves user avatar to the Images/Users directory.
        /// Single responsibility: only saves user avatar file.
        /// </summary>
        /// <param name="sourceFilePath">Path to the source image file</param>
        /// <param name="userId">User ID to use in filename</param>
        /// <returns>Relative path to the saved image, or null if save failed</returns>
        public static string? SaveUserAvatar(string sourceFilePath, int userId)
        {
            return SaveImage(sourceFilePath, ImageConstants.ImageFolders.Users, userId.ToString());
        }

        /// <summary>
        /// Generic method to save image to specified folder.
        /// Single responsibility: only saves image file to specified location.
        /// </summary>
        /// <param name="sourceFilePath">Path to the source image file</param>
        /// <param name="imageTypeFolder">Subfolder name (Products, Categories, Users, etc.)</param>
        /// <param name="entityId">Entity ID to use in filename</param>
        /// <returns>Relative path to the saved image, or null if save failed</returns>
        public static string? SaveImage(string sourceFilePath, string imageTypeFolder, string entityId)
        {
            if (string.IsNullOrWhiteSpace(sourceFilePath) || !File.Exists(sourceFilePath))
                return null;

            if (!IsValidImageFile(sourceFilePath))
                throw new ArgumentException("File format không được hỗ trợ. Chỉ chấp nhận: JPG, JPEG, PNG, GIF, BMP, WEBP");

            if (!IsValidFileSize(sourceFilePath))
                throw new ArgumentException($"Kích thước file quá lớn. Tối đa: {MaxFileSize / 1024 / 1024}MB");

            try
            {
                var targetDirectory = GetImageDirectory(imageTypeFolder);
                
                // Ensure directory exists
                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }

                // Generate unique filename: EntityId_Timestamp.extension
                var extension = Path.GetExtension(sourceFilePath);
                var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                var fileName = $"{entityId}_{timestamp}{extension}";
                var destinationPath = Path.Combine(targetDirectory, fileName);

                // Copy file
                File.Copy(sourceFilePath, destinationPath, overwrite: true);

                // Return relative path for database storage
                return Path.Combine(ImageConstants.ImagesBaseFolder, imageTypeFolder, fileName);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Không thể lưu ảnh: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Deletes product image file.
        /// Single responsibility: only deletes product image file.
        /// </summary>
        public static void DeleteProductImage(string? imagePath)
        {
            DeleteImage(imagePath);
        }

        /// <summary>
        /// Deletes category image file.
        /// Single responsibility: only deletes category image file.
        /// </summary>
        public static void DeleteCategoryImage(string? imagePath)
        {
            DeleteImage(imagePath);
        }

        /// <summary>
        /// Deletes user avatar file.
        /// Single responsibility: only deletes user avatar file.
        /// </summary>
        public static void DeleteUserAvatar(string? imagePath)
        {
            DeleteImage(imagePath);
        }

        /// <summary>
        /// Generic method to delete image file.
        /// Single responsibility: only deletes image file.
        /// </summary>
        public static void DeleteImage(string? imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                return;

            try
            {
                var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imagePath);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
            catch
            {
                // Silently fail - image deletion is not critical
            }
        }

        /// <summary>
        /// Gets the full path to the product image file.
        /// Single responsibility: only resolves product image file path.
        /// </summary>
        public static string? GetProductImagePath(string? imagePath)
        {
            return GetImagePath(imagePath);
        }

        /// <summary>
        /// Gets the full path to any image file from relative path.
        /// Single responsibility: only resolves image file path.
        /// </summary>
        public static string? GetImagePath(string? imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                return null;

            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imagePath);
            return File.Exists(fullPath) ? fullPath : null;
        }

        /// <summary>
        /// Loads product image as Image object for display.
        /// Single responsibility: only loads product image for display.
        /// </summary>
        public static Image? LoadProductImage(string? imagePath)
        {
            return LoadImage(imagePath);
        }

        /// <summary>
        /// Generic method to load image as Image object for display.
        /// Single responsibility: only loads image for display.
        /// </summary>
        public static Image? LoadImage(string? imagePath)
        {
            var fullPath = GetImagePath(imagePath);
            if (string.IsNullOrWhiteSpace(fullPath))
                return null;

            try
            {
                return Image.FromFile(fullPath);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Resizes image to fit within specified dimensions while maintaining aspect ratio.
        /// Single responsibility: only resizes image.
        /// </summary>
        public static Image? ResizeImage(Image? image, int maxWidth, int maxHeight)
        {
            if (image == null)
                return null;

            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }
    }
}

