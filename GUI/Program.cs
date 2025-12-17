using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using WinFormsFashionShop.Presentation.Forms;
using WinFormsFashionShop.Business.Composition;
using WinFormsFashionShop.Presentation.Helpers;
using WinFormsFashionShop.Presentation.Composition;

namespace WinFormsFashionShop.Presentation
{
    internal static class Program
    {
        private static readonly string LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Setup global exception handlers
            SetupGlobalExceptionHandlers();

            // Ensure log directory exists
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }

            // Obtain services from Business composition root. Presentation does not reference Data.
            var services = ServicesComposition.Create();
            var presentationServices = PresentationComposition.Create();

            using (var login = new LoginForm(services.AuthService))
            {
                var result = login.ShowDialog();
                if (result == DialogResult.OK && login.LoggedInUser != null)
                {
                    try
                    {
                        Application.Run(new MainForm(
                            services.DashboardService,
                            presentationServices.DashboardCardFactory,
                            presentationServices.ErrorHandler,
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
                        LogException(ex, "Application startup error");
                        presentationServices.ErrorHandler.ShowError("Lỗi khởi động ứng dụng: " + ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Sets up global exception handlers for unhandled exceptions.
        /// Single responsibility: only configures exception handlers.
        /// </summary>
        private static void SetupGlobalExceptionHandlers()
        {
            // Create error handler for global exception handlers
            var errorHandler = new ErrorHandlerService();

            // Handle UI thread exceptions
            Application.ThreadException += (sender, e) =>
            {
                LogException(e.Exception, "UI Thread Exception");
                errorHandler.ShowError("Đã xảy ra lỗi không mong muốn:\n" + e.Exception.Message);
            };

            // Handle non-UI thread exceptions
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                {
                    LogException(ex, "Unhandled Exception");
                    errorHandler.ShowError("Đã xảy ra lỗi nghiêm trọng:\n" + ex.Message);
                }
            };
        }

        /// <summary>
        /// Logs an exception to a file.
        /// Single responsibility: only logs exceptions.
        /// </summary>
        private static void LogException(Exception ex, string context)
        {
            try
            {
                var logFile = Path.Combine(LogDirectory, $"error-{DateTime.Now:yyyyMMdd}.log");
                var logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{context}]\n" +
                                $"Message: {ex.Message}\n" +
                                $"Stack Trace: {ex.StackTrace}\n";
                
                if (ex.InnerException != null)
                {
                    logMessage += $"Inner Exception: {ex.InnerException.Message}\n";
                }
                
                logMessage += new string('-', 80) + "\n\n";
                
                File.AppendAllText(logFile, logMessage);
            }
            catch
            {
                // Silently fail if logging fails
            }
        }
    }
}