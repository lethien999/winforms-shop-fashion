using System;
using System.Windows.Forms;

namespace WinFormsFashionShop.Presentation.Helpers
{
    /// <summary>
    /// Centralized error handling helper for displaying messages to users.
    /// Follows Single Responsibility Principle - only responsible for showing messages.
    /// </summary>
    public static class ErrorHandler
    {
        /// <summary>
        /// Shows an error message to the user.
        /// </summary>
        /// <param name="message">The error message to display</param>
        public static void ShowError(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                message = "Đã xảy ra lỗi không xác định.";

            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows an error message from an exception.
        /// </summary>
        /// <param name="ex">The exception to display</param>
        public static void ShowError(Exception ex)
        {
            if (ex == null)
            {
                ShowError("Đã xảy ra lỗi không xác định.");
                return;
            }

            var message = $"Lỗi: {ex.Message}";
            if (ex.InnerException != null)
            {
                message += $"\n\nChi tiết: {ex.InnerException.Message}";
            }

            ShowError(message);
        }

        /// <summary>
        /// Shows an error message with a custom title.
        /// </summary>
        /// <param name="message">The error message to display</param>
        /// <param name="title">The title of the message box</param>
        public static void ShowError(string message, string title)
        {
            if (string.IsNullOrWhiteSpace(message))
                message = "Đã xảy ra lỗi không xác định.";

            if (string.IsNullOrWhiteSpace(title))
                title = "Lỗi";

            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows a warning message to the user.
        /// </summary>
        /// <param name="message">The warning message to display</param>
        public static void ShowWarning(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                message = "Cảnh báo.";

            MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Shows a success message to the user.
        /// </summary>
        /// <param name="message">The success message to display</param>
        public static void ShowSuccess(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                message = "Thành công.";

            MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Shows an information message to the user.
        /// </summary>
        /// <param name="message">The information message to display</param>
        public static void ShowInfo(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                message = "Thông tin.";

            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Shows a confirmation dialog and returns the user's choice.
        /// </summary>
        /// <param name="message">The confirmation message to display</param>
        /// <param name="title">The title of the message box</param>
        /// <returns>True if user clicked Yes, False otherwise</returns>
        public static bool ShowConfirmation(string message, string title = "Xác nhận")
        {
            if (string.IsNullOrWhiteSpace(message))
                message = "Bạn có chắc chắn?";

            if (string.IsNullOrWhiteSpace(title))
                title = "Xác nhận";

            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
    }
}

