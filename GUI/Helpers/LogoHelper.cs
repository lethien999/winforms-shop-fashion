using System;
using System.Drawing;
using System.IO;
using WinFormsFashionShop.Business.Constants;

namespace WinFormsFashionShop.Presentation.Helpers
{
    /// <summary>
    /// Helper class for managing brand logo across the application.
    /// Single responsibility: only handles logo loading and management.
    /// </summary>
    public static class LogoHelper
    {
        /// <summary>
        /// Brand logo folder name.
        /// </summary>
        private const string BrandFolder = "Brand";

        /// <summary>
        /// Default logo filename.
        /// </summary>
        private const string DefaultLogoFileName = "logo.png";

        /// <summary>
        /// Alternative logo filenames to try.
        /// </summary>
        private static readonly string[] AlternativeLogoNames = 
        {
            "logo.png",
            "logo.jpg",
            "logo.jpeg",
            "brand.png",
            "brand.jpg",
            "TF_logo.png",
            "TF_logo.jpg"
        };

        /// <summary>
        /// Gets the full path to the brand logo directory.
        /// </summary>
        private static string GetBrandLogoDirectory()
        {
            return Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                ImageConstants.ImagesBaseFolder,
                BrandFolder
            );
        }

        /// <summary>
        /// Gets the full path to the logo file.
        /// </summary>
        private static string? GetLogoPath()
        {
            var brandDir = GetBrandLogoDirectory();
            
            if (!Directory.Exists(brandDir))
            {
                // Try to create directory if it doesn't exist
                try
                {
                    Directory.CreateDirectory(brandDir);
                }
                catch
                {
                    return null;
                }
            }

            // Try to find logo file with alternative names
            foreach (var logoName in AlternativeLogoNames)
            {
                var logoPath = Path.Combine(brandDir, logoName);
                if (File.Exists(logoPath))
                {
                    return logoPath;
                }
            }

            return null;
        }

        /// <summary>
        /// Loads the brand logo as an Image object.
        /// Returns null if logo file is not found.
        /// </summary>
        /// <returns>Logo image or null if not found</returns>
        public static Image? LoadLogo()
        {
            var logoPath = GetLogoPath();
            if (string.IsNullOrWhiteSpace(logoPath))
                return null;

            try
            {
                return Image.FromFile(logoPath);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Loads the brand logo and resizes it to specified dimensions.
        /// </summary>
        /// <param name="width">Target width</param>
        /// <param name="height">Target height</param>
        /// <returns>Resized logo image or null if not found</returns>
        public static Image? LoadLogo(int width, int height)
        {
            var logo = LoadLogo();
            if (logo == null)
                return null;

            return ImageHelper.ResizeImage(logo, width, height);
        }

        /// <summary>
        /// Loads the brand logo and resizes it maintaining aspect ratio.
        /// </summary>
        /// <param name="maxSize">Maximum size (width and height)</param>
        /// <returns>Resized logo image or null if not found</returns>
        public static Image? LoadLogo(int maxSize)
        {
            return LoadLogo(maxSize, maxSize);
        }

        /// <summary>
        /// Checks if logo file exists.
        /// </summary>
        /// <returns>True if logo exists, false otherwise</returns>
        public static bool LogoExists()
        {
            return !string.IsNullOrWhiteSpace(GetLogoPath());
        }

        /// <summary>
        /// Gets the logo directory path for reference.
        /// </summary>
        /// <returns>Logo directory path</returns>
        public static string GetLogoDirectory()
        {
            return GetBrandLogoDirectory();
        }
    }
}

