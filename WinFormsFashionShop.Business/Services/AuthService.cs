using System;
using WinFormsFashionShop.Data.Entities;
using WinFormsFashionShop.Data.Repositories;

namespace WinFormsFashionShop.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Validates credentials by comparing username and password hash.
        /// </summary>
        public bool ValidateCredentials(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("username is required", nameof(username));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("password is required", nameof(password));

            var user = GetUserByUsername(username);
            if (user == null) return false;

            // TODO: Replace with secure hashing and salt verification.
            return user.PasswordHash == password; // placeholder comparison
        }

        /// <summary>
        /// Returns user by username or null when not found.
        /// </summary>
        public User? GetUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("username is required", nameof(username));
            foreach (var u in _userRepository.GetAll())
            {
                if (string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase)) return u;
            }
            return null;
        }
    }
}