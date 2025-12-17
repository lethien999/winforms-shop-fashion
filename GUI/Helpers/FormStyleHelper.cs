using System.Drawing;
using System.Windows.Forms;
using UIThemeConstants = WinFormsFashionShop.Presentation.Helpers.UIThemeConstants;

namespace WinFormsFashionShop.Presentation.Helpers
{
    /// <summary>
    /// Helper class for applying consistent form styling.
    /// Single responsibility: only handles form styling.
    /// </summary>
    public static class FormStyleHelper
    {
        /// <summary>
        /// Applies compact window style to a management form.
        /// </summary>
        public static void ApplyManagementFormStyle(Form form)
        {
            UIThemeConstants.ThemeHelper.ApplyFormStyle(
                form, 
                UIThemeConstants.FormSizes.ManagementForm,
                UIThemeConstants.FormSizes.ManagementFormMinimum
            );
        }

        /// <summary>
        /// Applies compact window style to an order form.
        /// </summary>
        public static void ApplyOrderFormStyle(Form form)
        {
            UIThemeConstants.ThemeHelper.ApplyFormStyle(
                form,
                UIThemeConstants.FormSizes.OrderForm,
                UIThemeConstants.FormSizes.OrderFormMinimum
            );
        }

        /// <summary>
        /// Applies standard dialog style.
        /// </summary>
        public static void ApplyDialogStyle(Form form, Size dialogSize)
        {
            form.BackColor = UIThemeConstants.Colors.BackgroundLight;
            form.Font = UIThemeConstants.Fonts.BodyMedium;
            form.ClientSize = dialogSize;
            form.StartPosition = FormStartPosition.CenterParent;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
        }

        /// <summary>
        /// Applies header panel style with logo and title.
        /// </summary>
        public static void ApplyHeaderPanelStyle(Panel panel, Label titleLabel, PictureBox? logoBox = null)
        {
            UIThemeConstants.ThemeHelper.ApplyHeaderPanelStyle(panel);
            
            if (titleLabel != null)
            {
                titleLabel.Font = UIThemeConstants.Fonts.TitleSmall;
                titleLabel.ForeColor = UIThemeConstants.Colors.TextLight;
                titleLabel.AutoSize = true;
            }

            if (logoBox != null)
            {
                logoBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        /// <summary>
        /// Applies search panel style.
        /// </summary>
        public static void ApplySearchPanelStyle(Panel panel)
        {
            UIThemeConstants.ThemeHelper.ApplySearchPanelStyle(panel);
        }

        /// <summary>
        /// Applies button panel style.
        /// </summary>
        public static void ApplyButtonPanelStyle(Panel panel)
        {
            UIThemeConstants.ThemeHelper.ApplyButtonPanelStyle(panel);
        }

        /// <summary>
        /// Applies standard button styles based on button type.
        /// </summary>
        public static void ApplyButtonStyle(Button button, ButtonType buttonType)
        {
            switch (buttonType)
            {
                case ButtonType.Primary:
                    UIThemeConstants.ThemeHelper.ApplyPrimaryButtonStyle(button);
                    break;
                case ButtonType.Success:
                    UIThemeConstants.ThemeHelper.ApplySuccessButtonStyle(button);
                    break;
                case ButtonType.Danger:
                    UIThemeConstants.ThemeHelper.ApplyDangerButtonStyle(button);
                    break;
                case ButtonType.Secondary:
                    UIThemeConstants.ThemeHelper.ApplySecondaryButtonStyle(button);
                    break;
                case ButtonType.Info:
                    UIThemeConstants.ThemeHelper.ApplyInfoButtonStyle(button);
                    break;
                default:
                    button.Height = UIThemeConstants.Spacing.ButtonHeight;
                    button.Font = UIThemeConstants.Fonts.Button;
                    break;
            }
        }

        /// <summary>
        /// Applies grid style to DataGridView.
        /// </summary>
        public static void ApplyGridStyle(DataGridView grid)
        {
            UIThemeConstants.ThemeHelper.ApplyGridStyle(grid);
        }
    }

    /// <summary>
    /// Button type enumeration for styling.
    /// </summary>
    public enum ButtonType
    {
        Primary,
        Success,
        Danger,
        Secondary,
        Info,
        Default
    }
}
