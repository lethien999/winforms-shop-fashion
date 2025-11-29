using System.Collections.Generic;
using WinFormsFashionShop.DTO;

namespace WinFormsFashionShop.Business.Services
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDTO> GetAllCustomers();
        CustomerDTO? GetCustomerById(int id);
        void CreateCustomer(CreateCustomerDTO customer);
        void UpdateCustomer(UpdateCustomerDTO customer);
        void DeleteCustomer(int id);
    }
}