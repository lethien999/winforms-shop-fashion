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
                if (result == DialogResult.OK && login.LoggedInUser != null)
                {
                    try
                    {
                        Application.Run(new MainForm(
                            services.ProductService, 
                            services.CustomerService, 
                            services.OrderService, 
                            services.InventoryService,
                            services.CategoryService,
                            services.UserService,
                            services.ReportService,
                            login.LoggedInUser));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ứng dụng lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}