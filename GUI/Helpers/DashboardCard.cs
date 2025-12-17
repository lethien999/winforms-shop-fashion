using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using UIThemeConstants = WinFormsFashionShop.Presentation.Helpers.UIThemeConstants;

namespace WinFormsFashionShop.Presentation.Helpers
{
    /// <summary>
    /// Custom Panel với rounded corners và shadow effect cho dashboard cards.
    /// Single responsibility: chỉ render card đẹp với shadow và rounded corners.
    /// </summary>
    public class DashboardCard : Panel
    {
        private Color _cardColor = Color.White;
        private Color _gradientColor = Color.Empty;
        private Color _shadowColor = Color.FromArgb(100, 0, 0, 0);
        private int _cornerRadius = 12;
        private int _shadowOffset = 4;
        private bool _useGradient = false;

        public Color CardColor
        {
            get => _cardColor;
            set { _cardColor = value; Invalidate(); }
        }

        public Color GradientColor
        {
            get => _gradientColor;
            set { _gradientColor = value; Invalidate(); }
        }

        public bool UseGradient
        {
            get => _useGradient;
            set { _useGradient = value; Invalidate(); }
        }

        public Color ShadowColor
        {
            get => _shadowColor;
            set { _shadowColor = value; Invalidate(); }
        }

        public int CornerRadius
        {
            get => _cornerRadius;
            set { _cornerRadius = value; Invalidate(); }
        }

        public DashboardCard()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | 
                     ControlStyles.UserPaint | 
                     ControlStyles.DoubleBuffer | 
                     ControlStyles.ResizeRedraw, true);
            BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Vẽ shadow
            using (var shadowBrush = new SolidBrush(Color.FromArgb(30, 0, 0, 0)))
            {
                var shadowRect = new Rectangle(
                    _shadowOffset, 
                    _shadowOffset, 
                    Width - _shadowOffset * 2, 
                    Height - _shadowOffset * 2);
                DrawRoundedRectangle(g, shadowBrush, null, shadowRect, _cornerRadius);
            }

            // Vẽ card với gradient hoặc solid color
            var cardRect = new Rectangle(0, 0, Width - _shadowOffset, Height - _shadowOffset);
            
            if (_useGradient && !_gradientColor.IsEmpty)
            {
                using (var gradientBrush = new LinearGradientBrush(
                    cardRect, 
                    _cardColor, 
                    _gradientColor, 
                    LinearGradientMode.Vertical))
                {
                    DrawRoundedRectangle(g, gradientBrush, null, cardRect, _cornerRadius);
                }
            }
            else
            {
                using (var cardBrush = new SolidBrush(_cardColor))
                {
                    DrawRoundedRectangle(g, cardBrush, null, cardRect, _cornerRadius);
                }
            }

            // Vẽ border nhẹ
            using (var borderPen = new Pen(UIThemeConstants.Colors.BorderLight, 1))
            {
                var borderRect = new Rectangle(0, 0, Width - _shadowOffset - 1, Height - _shadowOffset - 1);
                DrawRoundedRectangle(g, null, borderPen, borderRect, _cornerRadius);
            }
        }

        private void DrawRoundedRectangle(Graphics g, Brush? brush, Pen? pen, Rectangle rect, int radius)
        {
            using (var path = new GraphicsPath())
            {
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                path.CloseAllFigures();

                if (brush != null)
                    g.FillPath(brush, path);
                if (pen != null)
                    g.DrawPath(pen, path);
            }
        }
    }
}
