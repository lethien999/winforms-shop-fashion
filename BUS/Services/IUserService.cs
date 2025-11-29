using System.Collections.Generic;
using WinFormsFashionShop.Data.Entities;

namespace WinFormsFashionShop.Business.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User? GetUserById(int id);
        void CreateUser(User user, string password);
        void UpdateUser(User user);
        void UpdateUserPassword(int userId, string newPassword);
        void DeleteUser(int id);
        IEnumerable<User> GetUsersByRole(string role);
        IEnumerable<User> GetActiveUsers();
        void DeactivateUser(int userId);
        void ActivateUser(int userId);
    }
}

