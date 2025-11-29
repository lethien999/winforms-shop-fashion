using System;
using System.Collections.Generic;
using System.Linq;
using WinFormsFashionShop.Business.Mappers;
using WinFormsFashionShop.Data.Repositories;
using WinFormsFashionShop.DTO;

namespace WinFormsFashionShop.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
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

        public void CreateProduct(CreateProductDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(dto.Name)) throw new ArgumentException("Product name is required", nameof(dto.Name));
            if (dto.UnitPrice < 0) throw new ArgumentException("Price cannot be negative", nameof(dto.UnitPrice));
            
            var product = ProductMapper.ToEntity(dto);
            _productRepository.Insert(product);
        }

        public void UpdateProduct(UpdateProductDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (dto.Id <= 0) throw new ArgumentException("Product Id is invalid", nameof(dto.Id));
            
            var existingProduct = _productRepository.GetById(dto.Id) 
                ?? throw new InvalidOperationException("Product not found");
            
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
    }
}