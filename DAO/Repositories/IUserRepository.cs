using WinFormsFashionShop.Data.Entities;
using System.Collections.Generic;

namespace WinFormsFashionShop.Data.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        /// <summary>
        /// Gets a user by username (case-insensitive).
        /// </summary>
        User? GetByUsername(string username);
        IEnumerable<User> GetByRole(string role);
        IEnumerable<User> GetActiveUsers();
    }
}