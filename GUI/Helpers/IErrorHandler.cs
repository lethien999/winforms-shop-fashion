using System;

namespace WinFormsFashionShop.Presentation.Helpers
{
    /// <summary>
    /// Interface for error handling operations.
    /// Single responsibility: defines contract for displaying messages to users.
    /// </summary>
    public interface IErrorHandler
    {
        /// <summary>
        /// Shows an error message to the user.
        /// </summary>
        void ShowError(string message);

        /// <summary>
        /// Shows an error message from an exception.
        /// </summary>
        void ShowError(Exception ex);

        /// <summary>
        /// Shows an error message with a custom title.
        /// </summary>
        void ShowError(string message, string title);

        /// <summary>
        /// Shows a warning message to the user.
        /// </summary>
        void ShowWarning(string message);

        /// <summary>
        /// Shows a success message to the user.
        /// </summary>
        void ShowSuccess(string message);

        /// <summary>
        /// Shows an information message to the user.
        /// </summary>
        void ShowInfo(string message);

        /// <summary>
        /// Shows a confirmation dialog and returns the user's choice.
        /// </summary>
        bool ShowConfirmation(string message, string title = "Xác nhận");
    }
}
