using System;
using System.Collections.Generic;
using System.Linq;
using WinFormsFashionShop.Business.Mappers;
using WinFormsFashionShop.Data.Repositories;
using WinFormsFashionShop.DTO;

namespace WinFormsFashionShop.Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<CustomerDTO> GetAllCustomers()
        {
            return _customerRepository.GetAll().Select(CustomerMapper.ToDTO);
        }

        public CustomerDTO? GetCustomerById(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            var customer = _customerRepository.GetById(id);
            return customer != null ? CustomerMapper.ToDTO(customer) : null;
        }

        public void CreateCustomer(CreateCustomerDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(dto.CustomerName)) 
                throw new ArgumentException("Customer name is required", nameof(dto.CustomerName));
            
            var customer = CustomerMapper.ToEntity(dto);
            _customerRepository.Insert(customer);
        }

        public void UpdateCustomer(UpdateCustomerDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (dto.Id <= 0) throw new ArgumentException("Customer Id invalid", nameof(dto.Id));
            
            var existingCustomer = _customerRepository.GetById(dto.Id)
                ?? throw new InvalidOperationException("Customer not found");
            
            var updatedCustomer = CustomerMapper.ToEntity(dto, existingCustomer);
            _customerRepository.Update(updatedCustomer);
        }

        public void DeleteCustomer(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            _customerRepository.Delete(id);
        }
    }
}