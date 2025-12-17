using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsFashionShop.Presentation.Helpers
{
    /// <summary>
    /// Interface for creating dashboard cards.
    /// Single responsibility: defines contract for creating dashboard card UI elements.
    /// </summary>
    public interface IDashboardCardFactory
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
        Panel CreateStatCard(string title, string value, string icon, Color cardColor, int width = 240, int height = 140);

        /// <summary>
        /// Creates a dashboard action button card.
        /// </summary>
        Panel CreateActionCard(string title, string description, string icon, EventHandler clickHandler, Color? cardColor = null);
    }
}
