using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinFormsFashionShop.Presentation.Helpers
{
    /// <summary>
    /// UI Theme constants for consistent styling across all forms.
    /// Single responsibility: only contains UI theme constants.
    /// </summary>
    public static class UIThemeConstants
    {
        /// <summary>
        /// Brand colors for the application.
        /// </summary>
        public static class Colors
        {
            // Primary brand colors
            public static readonly Color PrimaryBlue = Color.FromArgb(70, 130, 180);      // Steel Blue
            public static readonly Color PrimaryBlueLight = Color.FromArgb(100, 150, 200); // Light Blue
            public static readonly Color PrimaryBlueDark = Color.FromArgb(50, 100, 150);   // Dark Blue
            public static readonly Color PrimaryGold = Color.FromArgb(255, 215, 0);       // Gold accent
            public static readonly Color PrimaryDark = Color.FromArgb(30, 30, 40);        // Dark background
            
            // Dashboard card colors
            public static readonly Color CardRevenue = Color.FromArgb(34, 193, 195);      // Teal/Cyan gradient
            public static readonly Color CardOrders = Color.FromArgb(251, 176, 64);       // Orange
            public static readonly Color CardProducts = Color.FromArgb(149, 117, 205);    // Purple
            public static readonly Color CardCustomers = Color.FromArgb(255, 94, 87);     // Coral/Red
            public static readonly Color CardInventory = Color.FromArgb(72, 219, 251);    // Light Blue
            public static readonly Color CardLowStock = Color.FromArgb(255, 159, 64);     // Orange Warning
            
            // Secondary colors
            public static readonly Color SuccessGreen = Color.FromArgb(34, 139, 34);       // Forest Green
            public static readonly Color WarningOrange = Color.FromArgb(255, 165, 0);     // Orange
            public static readonly Color DangerRed = Color.FromArgb(220, 53, 69);         // Red
            public static readonly Color InfoBlue = Color.FromArgb(0, 123, 255);          // Info Blue
            public static readonly Color SecondaryGray = Color.FromArgb(108, 117, 125);   // Gray
            
            // Background colors
            public static readonly Color BackgroundLight = Color.FromArgb(245, 245, 250); // Light gray background
            public static readonly Color BackgroundWhite = Color.White;
            public static readonly Color BackgroundPanel = Color.FromArgb(240, 240, 240);  // Panel background
            public static readonly Color BackgroundScroll = Color.FromArgb(230, 230, 235); // Scroll area
            
            // Text colors
            public static readonly Color TextPrimary = Color.FromArgb(30, 30, 40);
            public static readonly Color TextSecondary = Color.FromArgb(108, 117, 125);
            public static readonly Color TextLight = Color.White;
            public static readonly Color TextMuted = Color.Gray;
            
            // Border colors
            public static readonly Color BorderLight = Color.FromArgb(230, 230, 230);
            public static readonly Color BorderMedium = Color.FromArgb(200, 200, 200);
            
            // Grid colors
            public static readonly Color GridHeader = PrimaryBlue;
            public static readonly Color GridHeaderText = Color.White;
            public static readonly Color GridRowSelected = Color.FromArgb(200, 230, 255);
            public static readonly Color GridRowAlternate = Color.FromArgb(248, 248, 248);
        }

        /// <summary>
        /// Font definitions for consistent typography.
        /// </summary>
        public static class Fonts
        {
            // Title fonts
            public static readonly Font TitleLarge = new Font("Arial", 18, FontStyle.Bold);
            public static readonly Font TitleMedium = new Font("Arial", 16, FontStyle.Bold);
            public static readonly Font TitleSmall = new Font("Arial", 14, FontStyle.Bold);
            
            // Body fonts
            public static readonly Font BodyLarge = new Font("Arial", 12, FontStyle.Regular);
            public static readonly Font BodyMedium = new Font("Arial", 11, FontStyle.Regular);
            public static readonly Font BodySmall = new Font("Arial", 10, FontStyle.Regular);
            public static readonly Font BodyTiny = new Font("Arial", 9, FontStyle.Regular);
            
            // Special fonts
            public static readonly Font Button = new Font("Arial", 10, FontStyle.Bold);
            public static readonly Font Label = new Font("Arial", 10, FontStyle.Regular);
            public static readonly Font LabelBold = new Font("Arial", 10, FontStyle.Bold);
            public static readonly Font Info = new Font("Arial", 9, FontStyle.Italic);
        }

        /// <summary>
        /// Spacing and sizing constants for consistent layout.
        /// </summary>
        public static class Spacing
        {
            // Padding
            public const int PaddingTiny = 5;
            public const int PaddingSmall = 10;
            public const int PaddingMedium = 15;
            public const int PaddingLarge = 20;
            public const int PaddingXLarge = 30;
            
            // Margins
            public const int MarginTiny = 5;
            public const int MarginSmall = 10;
            public const int MarginMedium = 15;
            public const int MarginLarge = 20;
            public const int MarginXLarge = 30;
            
            // Control sizes - optimized for compact UI
            public const int ButtonHeight = 35;
            public const int ButtonHeightSmall = 28;
            public const int ButtonHeightLarge = 40;
            public const int TextBoxHeight = 28;
            public const int HeaderHeight = 60;
            public const int FooterHeight = 60;
            public const int SearchPanelHeight = 70;
            public const int GridRowHeight = 32;
            
            // Logo sizes
            public const int LogoSizeSmall = 32;
            public const int LogoSizeMedium = 48;
            public const int LogoSizeLarge = 64;
            public const int LogoSizeXLarge = 96;
        }

        /// <summary>
        /// Form size constants - optimized for compact windowed mode.
        /// </summary>
        public static class FormSizes
        {
            // Standard form sizes - compact và vừa phải
            public static readonly Size StandardForm = new Size(1024, 720);
            public static readonly Size StandardFormSmall = new Size(900, 650);
            public static readonly Size StandardFormMedium = new Size(1100, 750);
            public static readonly Size StandardFormMinimum = new Size(800, 600);
            
            // Management forms (Product, Customer, Order Management, etc.)
            public static readonly Size ManagementForm = new Size(1000, 700);
            public static readonly Size ManagementFormMinimum = new Size(900, 600);
            
            // Order form - có thể lớn hơn một chút vì cần nhiều thông tin
            public static readonly Size OrderForm = new Size(1100, 750);
            public static readonly Size OrderFormMinimum = new Size(950, 650);
            
            // Dialog sizes - nhỏ gọn
            public static readonly Size DialogSmall = new Size(450, 350);
            public static readonly Size DialogMedium = new Size(600, 500);
            public static readonly Size DialogLarge = new Size(800, 650);
            public static readonly Size DialogXLarge = new Size(950, 750);
            
            // Login form (centered, smaller)
            public static readonly Size LoginForm = new Size(450, 400);
            
            // Selection dialogs
            public static readonly Size SelectionDialog = new Size(800, 600);
            public static readonly Size SelectionDialogSmall = new Size(650, 500);
        }

        /// <summary>
        /// Border radius for rounded corners (for future use with custom controls).
        /// </summary>
        public static class BorderRadius
        {
            public const int Small = 4;
            public const int Medium = 8;
            public const int Large = 12;
        }

        /// <summary>
        /// Helper methods for applying theme to controls.
        /// </summary>
        public static class ThemeHelper
        {
            /// <summary>
            /// Applies primary button style.
            /// </summary>
            public static void ApplyPrimaryButtonStyle(Button button)
            {
                button.BackColor = Colors.PrimaryBlue;
                button.ForeColor = Colors.TextLight;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.Font = Fonts.Button;
                button.Height = Spacing.ButtonHeight;
                button.Cursor = Cursors.Hand;
            }

            /// <summary>
            /// Applies success button style.
            /// </summary>
            public static void ApplySuccessButtonStyle(Button button)
            {
                button.BackColor = Colors.SuccessGreen;
                button.ForeColor = Colors.TextLight;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.Font = Fonts.Button;
                button.Height = Spacing.ButtonHeight;
                button.Cursor = Cursors.Hand;
            }

            /// <summary>
            /// Applies danger button style.
            /// </summary>
            public static void ApplyDangerButtonStyle(Button button)
            {
                button.BackColor = Colors.DangerRed;
                button.ForeColor = Colors.TextLight;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.Font = Fonts.Button;
                button.Height = Spacing.ButtonHeight;
                button.Cursor = Cursors.Hand;
            }

            /// <summary>
            /// Applies secondary button style.
            /// </summary>
            public static void ApplySecondaryButtonStyle(Button button)
            {
                button.BackColor = Colors.SecondaryGray;
                button.ForeColor = Colors.TextLight;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.Font = Fonts.Button;
                button.Height = Spacing.ButtonHeight;
                button.Cursor = Cursors.Hand;
            }

            /// <summary>
            /// Applies info button style.
            /// </summary>
            public static void ApplyInfoButtonStyle(Button button)
            {
                button.BackColor = Colors.InfoBlue;
                button.ForeColor = Colors.TextLight;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.Font = Fonts.Button;
                button.Height = Spacing.ButtonHeight;
                button.Cursor = Cursors.Hand;
            }

            /// <summary>
            /// Applies grid styling.
            /// </summary>
            public static void ApplyGridStyle(DataGridView grid)
            {
                grid.BackgroundColor = Colors.BackgroundWhite;
                grid.BorderStyle = BorderStyle.None;
                grid.EnableHeadersVisualStyles = false;
                grid.ColumnHeadersDefaultCellStyle.BackColor = Colors.GridHeader;
                grid.ColumnHeadersDefaultCellStyle.ForeColor = Colors.GridHeaderText;
                grid.ColumnHeadersDefaultCellStyle.Font = Fonts.LabelBold;
                grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Colors.GridHeader;
                grid.DefaultCellStyle.SelectionBackColor = Colors.GridRowSelected;
                grid.DefaultCellStyle.SelectionForeColor = Colors.TextPrimary;
                grid.AlternatingRowsDefaultCellStyle.BackColor = Colors.GridRowAlternate;
                grid.ColumnHeadersHeight = 35;
                grid.RowTemplate.Height = Spacing.GridRowHeight;
            }

            /// <summary>
            /// Applies standard form styling - compact và professional.
            /// </summary>
            public static void ApplyFormStyle(Form form, Size formSize, Size? minimumSize = null)
            {
                form.BackColor = Colors.BackgroundLight;
                form.Font = Fonts.BodyMedium;
                form.ClientSize = formSize;
                if (minimumSize.HasValue)
                {
                    form.MinimumSize = minimumSize.Value;
                }
                form.StartPosition = FormStartPosition.CenterParent;
            }

            /// <summary>
            /// Applies header panel styling with gradient.
            /// </summary>
            public static void ApplyHeaderPanelStyle(Panel panel)
            {
                panel.BackColor = Colors.PrimaryBlue;
                panel.Height = Spacing.HeaderHeight;
                panel.Dock = DockStyle.Top;
                
                // Apply gradient effect
                panel.Paint += (s, e) =>
                {
                    using (var brush = new LinearGradientBrush(
                        panel.ClientRectangle,
                        Colors.PrimaryBlue,
                        Colors.PrimaryBlueDark,
                        LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(brush, panel.ClientRectangle);
                    }
                };
            }

            /// <summary>
            /// Applies footer/button panel styling.
            /// </summary>
            public static void ApplyFooterPanelStyle(Panel panel)
            {
                panel.BackColor = Colors.BackgroundPanel;
                panel.Height = Spacing.FooterHeight;
                panel.Dock = DockStyle.Bottom;
                panel.Padding = new Padding(Spacing.PaddingMedium);
            }

            /// <summary>
            /// Applies search panel styling.
            /// </summary>
            public static void ApplySearchPanelStyle(Panel panel)
            {
                panel.BackColor = Colors.BackgroundWhite;
                panel.BorderStyle = BorderStyle.FixedSingle;
                panel.Height = Spacing.SearchPanelHeight;
                panel.Dock = DockStyle.Top;
                panel.Padding = new Padding(Spacing.PaddingSmall);
            }

            /// <summary>
            /// Applies standard textbox styling.
            /// </summary>
            public static void ApplyTextBoxStyle(TextBox textBox)
            {
                textBox.Font = Fonts.BodyMedium;
                textBox.Height = Spacing.TextBoxHeight;
                textBox.BorderStyle = BorderStyle.FixedSingle;
            }

            /// <summary>
            /// Applies standard button panel styling for management forms.
            /// </summary>
            public static void ApplyButtonPanelStyle(Panel panel)
            {
                panel.BackColor = Colors.BackgroundPanel;
                panel.Height = Spacing.FooterHeight;
                panel.Dock = DockStyle.Bottom;
                panel.Padding = new Padding(Spacing.PaddingSmall, Spacing.PaddingTiny, Spacing.PaddingSmall, Spacing.PaddingTiny);
            }

            /// <summary>
            /// Applies beautiful button style with hover effect.
            /// </summary>
            public static void ApplyButtonStyle(Button button, Color backColor, Color hoverColor)
            {
                button.BackColor = backColor;
                button.ForeColor = Colors.TextLight;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.MouseOverBackColor = hoverColor;
                button.Font = Fonts.Button;
                button.Height = Spacing.ButtonHeight;
                button.Cursor = Cursors.Hand;
            }
        }
    }
}

