using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using UIThemeConstants = WinFormsFashionShop.Presentation.Helpers.UIThemeConstants;

namespace WinFormsFashionShop.Presentation.Helpers
{
    /// <summary>
    /// Helper class for creating dashboard cards with statistics.
    /// Single responsibility: only creates dashboard card UI elements.
    /// </summary>
    public static class DashboardHelper
    {
        /// <summary>
        /// Creates a dashboard statistics card.
        /// </summary>
        /// <param name="title">Card title</param>
        /// <param name="value">Main value to display</param>
        /// <param name="icon">Icon emoji or text</param>
        /// <param name="cardColor">Card background color</param>
        /// <param name="width">Card width</param>
        /// <param name="height">Card height</param>
        public static Panel CreateStatCard(string title, string value, string icon, Color cardColor, int width = 240, int height = 140)
        {
            // Tạo gradient color từ cardColor
            var gradientColor = Color.FromArgb(
                Math.Min(255, cardColor.R + 30),
                Math.Min(255, cardColor.G + 30),
                Math.Min(255, cardColor.B + 30));

            var card = new DashboardCard
            {
                Width = width,
                Height = height,
                CardColor = cardColor,
                GradientColor = gradientColor,
                UseGradient = true,
                Padding = new Padding(20, 18, 20, 18)
            };

            // Icon label
            var lblIcon = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI Emoji", 28, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(15, 15),
                ForeColor = Color.White
            };

            // Title label
            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Arial", 9, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(15, 55),
                ForeColor = Color.FromArgb(200, 255, 255, 255)
            };

            // Value label
            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Arial", 20, FontStyle.Bold),
                AutoSize = false,
                Location = new Point(15, 75),
                Size = new Size(width - 30, 35),
                ForeColor = Color.White
            };

            card.Controls.AddRange(new Control[] { lblIcon, lblTitle, lblValue });
            return card;
        }

        /// <summary>
        /// Creates a dashboard action button card.
        /// </summary>
        public static Panel CreateActionCard(string title, string description, string icon, EventHandler clickHandler, Color? cardColor = null)
        {
            var card = new DashboardCard
            {
                Width = 180,
                Height = 140,
                CardColor = cardColor ?? UIThemeConstants.Colors.BackgroundWhite,
                Padding = new Padding(15),
                Cursor = Cursors.Hand
            };

            // Icon label
            var lblIcon = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI Emoji", 32, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(12, 12),
                ForeColor = UIThemeConstants.Colors.PrimaryBlue
            };

            // Title label
            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Arial", 11, FontStyle.Bold),
                AutoSize = false,
                Location = new Point(12, 58),
                Size = new Size(156, 22),
                ForeColor = UIThemeConstants.Colors.TextPrimary
            };

            // Description label
            var lblDescription = new Label
            {
                Text = description,
                Font = new Font("Arial", 9, FontStyle.Regular),
                AutoSize = false,
                Location = new Point(12, 83),
                Size = new Size(156, 45),
                ForeColor = UIThemeConstants.Colors.TextSecondary
            };

            card.Controls.AddRange(new Control[] { lblIcon, lblTitle, lblDescription });
            card.Click += clickHandler;
            
            // Make all child controls clickable
            foreach (Control ctrl in card.Controls)
            {
                ctrl.Cursor = Cursors.Hand;
                ctrl.Click += clickHandler;
            }

            return card;
        }

        /// <summary>
        /// Applies gradient background to a panel.
        /// </summary>
        public static void ApplyGradientBackground(Panel panel, Color color1, Color color2, LinearGradientMode mode = LinearGradientMode.Vertical)
        {
            panel.Paint += (s, e) =>
            {
                using (var brush = new LinearGradientBrush(panel.ClientRectangle, color1, color2, mode))
                {
                    e.Graphics.FillRectangle(brush, panel.ClientRectangle);
                }
            };
        }
    }
}
