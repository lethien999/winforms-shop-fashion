namespace WinFormsFashionShop.Business.Constants
{
    /// <summary>
    /// Constants for image storage paths and configurations.
    /// Single responsibility: only contains image-related constants.
    /// </summary>
    public static class ImageConstants
    {
        /// <summary>
        /// Base folder name for all application images.
        /// </summary>
        public const string ImagesBaseFolder = "Images";

        /// <summary>
        /// Subfolder names for different image types.
        /// </summary>
        public static class ImageFolders
        {
            public const string Products = "Products";
            public const string Categories = "Categories";
            public const string Users = "Users";
            public const string Customers = "Customers";
            public const string Orders = "Orders";
            public const string Invoices = "Invoices";
            public const string Brand = "Brand";
            public const string Temp = "Temp";
        }

        /// <summary>
        /// Maximum file size in bytes (5MB).
        /// </summary>
        public const long MaxFileSize = 5 * 1024 * 1024;

        /// <summary>
        /// Supported image file extensions.
        /// </summary>
        public static readonly string[] SupportedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };

        /// <summary>
        /// Default image dimensions for different purposes.
        /// </summary>
        public static class ImageDimensions
        {
            public static class Product
            {
                public const int ThumbnailWidth = 60;
                public const int ThumbnailHeight = 60;
                public const int PreviewWidth = 150;
                public const int PreviewHeight = 150;
                public const int FullWidth = 800;
                public const int FullHeight = 800;
            }

            public static class Category
            {
                public const int ThumbnailWidth = 80;
                public const int ThumbnailHeight = 80;
            }

            public static class User
            {
                public const int AvatarWidth = 100;
                public const int AvatarHeight = 100;
            }
        }
    }
}

