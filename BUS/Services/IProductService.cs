using System.Collections.Generic;
using WinFormsFashionShop.DTO;

namespace WinFormsFashionShop.Business.Services
{
    public interface IProductService
    {
        /// <summary>
        /// Retrieves all products.
        /// </summary>
        IEnumerable<ProductDTO> GetAllProducts();

        /// <summary>
        /// Retrieves a product by identifier.
        /// </summary>
        ProductDTO? GetProductById(int id);

        /// <summary>
        /// Creates a new product.
        /// </summary>
        void CreateProduct(CreateProductDTO product);

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        void UpdateProduct(UpdateProductDTO product);

        /// <summary>
        /// Deletes a product by identifier.
        /// </summary>
        void DeleteProduct(int id);

        /// <summary>
        /// Updates only the price of the product.
        /// </summary>
        void UpdateProductPrice(int productId, decimal newPrice);

        /// <summary>
        /// Deactivates a product so it is not shown to customers.
        /// </summary>
        void DeactivateProduct(int productId);

        /// <summary>
        /// Generates a unique product code automatically.
        /// </summary>
        string GenerateProductCode();
    }
}