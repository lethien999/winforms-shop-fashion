using System.Drawing;
using System.Windows.Forms;

namespace WinFormsFashionShop.Presentation.Helpers
{
    /// <summary>
    /// Custom color table for MenuStrip to make it more beautiful.
    /// Single responsibility: only defines menu colors.
    /// </summary>
    public class MenuColorTable : ProfessionalColorTable
    {
        public override Color MenuItemSelected => Color.FromArgb(100, 150, 200);
        public override Color MenuItemSelectedGradientBegin => Color.FromArgb(100, 150, 200);
        public override Color MenuItemSelectedGradientEnd => Color.FromArgb(80, 130, 180);
        public override Color MenuItemPressedGradientBegin => Color.FromArgb(70, 130, 180);
        public override Color MenuItemPressedGradientEnd => Color.FromArgb(50, 100, 150);
        public override Color MenuItemBorder => Color.FromArgb(70, 130, 180);
        public override Color MenuBorder => Color.FromArgb(50, 100, 150);
        public override Color ImageMarginGradientBegin => Color.FromArgb(70, 130, 180);
        public override Color ImageMarginGradientMiddle => Color.FromArgb(80, 140, 190);
        public override Color ImageMarginGradientEnd => Color.FromArgb(70, 130, 180);
    }
}
