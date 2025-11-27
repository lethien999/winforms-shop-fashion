using System;
using System.Collections.Generic;
using WinFormsFashionShop.Data.Entities;
using WinFormsFashionShop.Data.Repositories;

namespace WinFormsFashionShop.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAll();
        }

        public Product? GetProductById(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            return _productRepository.GetById(id);
        }

        public void CreateProduct(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (string.IsNullOrWhiteSpace(product.Name)) throw new ArgumentException("Product name is required", nameof(product.Name));
            if (product.Price < 0) throw new ArgumentException("Price cannot be negative", nameof(product.Price));
            _productRepository.Insert(product);
        }

        public void UpdateProduct(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (product.Id <= 0) throw new ArgumentException("Product Id is invalid", nameof(product.Id));
            _productRepository.Update(product);
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
            var product = _productRepository.GetById(productId) ?? throw new InvalidOperationException("Product not found");
            product.Price = newPrice;
            _productRepository.Update(product);
        }

        public void DeactivateProduct(int productId)
        {
            if (productId <= 0) throw new ArgumentOutOfRangeException(nameof(productId));
            var product = _productRepository.GetById(productId) ?? throw new InvalidOperationException("Product not found");
            product.IsActive = false;
            _productRepository.Update(product);
        }
    }
}