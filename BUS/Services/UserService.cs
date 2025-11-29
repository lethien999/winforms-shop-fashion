using System;
using System.Collections.Generic;
using BCrypt.Net;
using WinFormsFashionShop.Data.Entities;
using WinFormsFashionShop.Data.Repositories;

namespace WinFormsFashionShop.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public User? GetUserById(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            return _userRepository.GetById(id);
        }

        public void CreateUser(User user, string password)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrWhiteSpace(user.Username))
                throw new ArgumentException("Username is required", nameof(user));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required", nameof(password));
            if (string.IsNullOrWhiteSpace(user.Role))
                throw new ArgumentException("Role is required", nameof(user));

            // Hash password
            user.PasswordHash = HashPassword(password);
            user.CreatedAt = DateTime.Now;
            user.IsActive = true;

            _userRepository.Insert(user);
        }

        public void UpdateUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (user.Id <= 0) throw new ArgumentException("User Id invalid", nameof(user));
            if (string.IsNullOrWhiteSpace(user.Username))
                throw new ArgumentException("Username is required", nameof(user));

            user.UpdatedAt = DateTime.Now;
            _userRepository.Update(user);
        }

        public void UpdateUserPassword(int userId, string newPassword)
        {
            if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
            if (string.IsNullOrWhiteSpace(newPassword))
                throw new ArgumentException("Password is required", nameof(newPassword));

            var user = _userRepository.GetById(userId);
            if (user == null) throw new InvalidOperationException($"User with Id {userId} not found");

            user.PasswordHash = HashPassword(newPassword);
            user.UpdatedAt = DateTime.Now;
            _userRepository.Update(user);
        }

        public void DeleteUser(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            _userRepository.Delete(id);
        }

        public IEnumerable<User> GetUsersByRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentException("Role is required", nameof(role));
            return _userRepository.GetByRole(role);
        }

        public IEnumerable<User> GetActiveUsers()
        {
            return _userRepository.GetActiveUsers();
        }

        public void DeactivateUser(int userId)
        {
            if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
            var user = _userRepository.GetById(userId);
            if (user == null) throw new InvalidOperationException($"User with Id {userId} not found");
            
            user.IsActive = false;
            user.UpdatedAt = DateTime.Now;
            _userRepository.Update(user);
        }

        public void ActivateUser(int userId)
        {
            if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
            var user = _userRepository.GetById(userId);
            if (user == null) throw new InvalidOperationException($"User with Id {userId} not found");
            
            user.IsActive = true;
            user.UpdatedAt = DateTime.Now;
            _userRepository.Update(user);
        }

        /// <summary>
        /// Hashes password using BCrypt with automatic salt generation.
        /// BCrypt automatically generates a unique salt for each password.
        /// </summary>
        private string HashPassword(string password)
        {
            // BCrypt automatically generates a salt and includes it in the hash
            // WorkFactor (10) determines the computational cost (2^10 = 1024 iterations)
            // Higher work factor = more secure but slower
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 10);
        }
    }
}

