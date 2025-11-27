using System;
using System.Collections.Generic;
using WinFormsFashionShop.Data.Entities;
using WinFormsFashionShop.Data.Repositories;

namespace WinFormsFashionShop.Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAll();
        }

        public Customer? GetCustomerById(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            return _customerRepository.GetById(id);
        }

        public void CreateCustomer(Customer customer)
        {
            if (customer == null) throw new ArgumentNullException(nameof(customer));
            if (string.IsNullOrWhiteSpace(customer.FullName)) throw new ArgumentException("Customer name is required", nameof(customer.FullName));
            _customerRepository.Insert(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            if (customer == null) throw new ArgumentNullException(nameof(customer));
            if (customer.Id <= 0) throw new ArgumentException("Customer Id invalid", nameof(customer.Id));
            _customerRepository.Update(customer);
        }

        public void DeleteCustomer(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            _customerRepository.Delete(id);
        }
    }
}