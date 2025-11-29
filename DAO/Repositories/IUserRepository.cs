using WinFormsFashionShop.Data.Entities;
using System.Collections.Generic;

namespace WinFormsFashionShop.Data.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        IEnumerable<User> GetByRole(string role);
        IEnumerable<User> GetActiveUsers();
    }
}