using WinFormsFashionShop.Presentation.Helpers;

namespace WinFormsFashionShop.Presentation.Composition
{
    /// <summary>
    /// Composition root for Presentation layer dependencies.
    /// Single responsibility: wiring presentation layer dependencies.
    /// </summary>
    public static class PresentationComposition
    {
        /// <summary>
        /// Creates and returns presentation services.
        /// </summary>
        public static PresentationServices Create()
        {
            return new PresentationServices
            {
                ErrorHandler = new ErrorHandlerService(),
                DashboardCardFactory = new DashboardCardFactory()
            };
        }
    }

    /// <summary>
    /// Registry for presentation services.
    /// </summary>
    public class PresentationServices
    {
        public IErrorHandler ErrorHandler { get; init; } = null!;
        public IDashboardCardFactory DashboardCardFactory { get; init; } = null!;
    }
}
