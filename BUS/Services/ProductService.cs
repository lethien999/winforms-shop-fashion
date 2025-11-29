using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WinFormsFashionShop.Business.Mappers;
using WinFormsFashionShop.Data.Entities;
using WinFormsFashionShop.Data.Repositories;
using WinFormsFashionShop.DTO;

namespace WinFormsFashionShop.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IInventoryRepository _inventoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IInventoryRepository inventoryRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _inventoryRepository = inventoryRepository ?? throw new ArgumentNullException(nameof(inventoryRepository));
        }

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            var products = _productRepository.GetAll();
            return products.Select(p =>
            {
                var category = _categoryRepository.GetById(p.CategoryId);
                return ProductMapper.ToDTO(p, category?.CategoryName);
            });
        }

        public ProductDTO? GetProductById(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            var product = _productRepository.GetById(id);
            if (product == null) return null;
            
            var category = _categoryRepository.GetById(product.CategoryId);
            return ProductMapper.ToDTO(product, category?.CategoryName);
        }

        /// <summary>
        /// Creates a new product and automatically creates an inventory record with QuantityInStock = 0.
        /// Note: ImagePath should be the relative path (e.g., "Images/Products/...") if image was already saved.
        /// </summary>
        public void CreateProduct(CreateProductDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(dto.Name)) throw new ArgumentException("Product name is required", nameof(dto.Name));
            if (dto.UnitPrice < 0) throw new ArgumentException("Price cannot be negative", nameof(dto.UnitPrice));
            
            // Create product
            var product = ProductMapper.ToEntity(dto);
            _productRepository.Insert(product);
            
            // If ImagePath is a temporary file path, it should be handled by Presentation layer before calling this method
            // Here we only store the ImagePath if it's already a relative path
            
            // Automatically create inventory record with QuantityInStock = 0
            var inventory = new Inventory
            {
                ProductId = product.Id,
                QuantityInStock = 0,
                LastUpdated = DateTime.Now
            };
            _inventoryRepository.Insert(inventory);
        }

        public void UpdateProduct(UpdateProductDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (dto.Id <= 0) throw new ArgumentException("Product Id is invalid", nameof(dto.Id));
            
            var existingProduct = _productRepository.GetById(dto.Id) 
                ?? throw new InvalidOperationException("Product not found");
            
            // Note: Image handling (save/delete) should be done by Presentation layer before calling this method
            // Here we just update the ImagePath if provided
            
            var updatedProduct = ProductMapper.ToEntity(dto, existingProduct);
            _productRepository.Update(updatedProduct);
        }

        public void DeleteProduct(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            _productRepository.Delete(id);
        }

        public void UpdateProductPrice(int productId, decimal newPrice)
        {
            if (productId <= 0) throw new ArgumentOutOfRangeException(nameof(productId));
            if (newPrice < 0) throw new ArgumentException("Price cannot be negative", nameof(newPrice));
            
            var product = _productRepository.GetById(productId) 
                ?? throw new InvalidOperationException("Product not found");
            product.UnitPrice = newPrice;
            _productRepository.Update(product);
        }

        public void DeactivateProduct(int productId)
        {
            if (productId <= 0) throw new ArgumentOutOfRangeException(nameof(productId));
            var product = _productRepository.GetById(productId) 
                ?? throw new InvalidOperationException("Product not found");
            product.IsActive = false;
            _productRepository.Update(product);
        }

        /// <summary>
        /// Generates a unique product code automatically.
        /// Format: SP + sequential number (e.g., SP0001, SP0002, ...)
        /// Single responsibility: only generates product code.
        /// </summary>
        public string GenerateProductCode()
        {
            var allProducts = _productRepository.GetAll().ToList();
            var maxNumber = 0;

            foreach (var product in allProducts)
            {
                // Try to extract number from existing codes like SP0001, SP123, etc.
                if (product.ProductCode.StartsWith("SP", StringComparison.OrdinalIgnoreCase))
                {
                    var numberPart = product.ProductCode.Substring(2);
                    if (int.TryParse(numberPart, out var number))
                    {
                        if (number > maxNumber)
                            maxNumber = number;
                    }
                }
            }

            // Generate next code
            var nextNumber = maxNumber + 1;
            return $"SP{nextNumber:D4}"; // Format: SP0001, SP0002, etc.
        }
    }
}