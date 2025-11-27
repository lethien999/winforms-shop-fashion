using System;
using System.Windows.Forms;
using WinFormsFashionShop.Presentation.Forms;
using WinFormsFashionShop.Business.Composition;

namespace WinFormsFashionShop.Presentation
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Obtain services from Business composition root. Presentation does not reference Data.
            var services = ServicesComposition.Create();

            using (var login = new LoginForm(services.AuthService))
            {
                var result = login.ShowDialog();
                if (result == DialogResult.OK)
                {
                    try
                    {
                        Application.Run(new MainForm(services.ProductService, services.CustomerService, services.OrderService, services.InventoryService));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"?ng d?ng l?i: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}