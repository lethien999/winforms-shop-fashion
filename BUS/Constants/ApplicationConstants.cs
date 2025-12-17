namespace WinFormsFashionShop.Business.Constants
{
    /// <summary>
    /// Application-wide constants to avoid magic values in code.
    /// Follows Single Responsibility Principle - only contains constant definitions.
    /// </summary>
    public static class OrderStatus
    {
        public const string Pending = "Pending";
        public const string Paid = "Paid";
        public const string Cancelled = "Cancelled";
    }

    /// <summary>
    /// User role constants.
    /// </summary>
    public static class UserRole
    {
        public const string Admin = "Admin";
        public const string Staff = "Staff";
    }

    /// <summary>
    /// Inventory-related constants.
    /// </summary>
    public static class InventoryConstants
    {
        /// <summary>
        /// Default threshold for low stock warning.
        /// </summary>
        public const int DefaultLowStockThreshold = 10;
    }

    /// <summary>
    /// Payment method constants.
    /// </summary>
    public static class PaymentMethod
    {
        public const string Cash = "Tiền mặt";
        public const string Card = "Thẻ";
        public const string Transfer = "Chuyển khoản";
        public const string VietQR = "VietQR (PayOS)";
        public const string Other = "Khác";
    }
}

