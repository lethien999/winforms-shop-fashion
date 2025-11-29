namespace WinFormsFashionShop.DTO
{
    /// <summary>
    /// Data Transfer Object for Customer
    /// </summary>
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;
    }

    /// <summary>
    /// DTO for creating a new customer
    /// </summary>
    public class CreateCustomerDTO
    {
        public string CustomerName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }

    /// <summary>
    /// DTO for updating an existing customer
    /// </summary>
    public class UpdateCustomerDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

