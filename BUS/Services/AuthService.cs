using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;
using WinFormsFashionShop.Business.Mappers;
using WinFormsFashionShop.Data.Entities;
using WinFormsFashionShop.Data.Repositories;
using WinFormsFashionShop.DTO;

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
        /// Validates credentials using BCrypt password verification.
        /// </summary>
        public bool ValidateCredentials(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("username is required", nameof(username));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("password is required", nameof(password));

            var user = _userRepository.GetAll().FirstOrDefault(u => 
                string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase));
            if (user == null) return false;

            // Check if user is active
            if (!user.IsActive) return false;

            // Verify password using BCrypt
            // BCrypt automatically handles salt and verification
            try
            {
                // Check if password hash is BCrypt format (starts with $2a$ or $2b$)
                if (user.PasswordHash.StartsWith("$2a$") || user.PasswordHash.StartsWith("$2b$"))
                {
                    return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
                }
                else
                {
                    // Legacy SHA256 hash - verify and migrate to BCrypt
                    // This allows backward compatibility during migration
                    var sha256Hash = HashPasswordSHA256(password);
                    if (user.PasswordHash == sha256Hash)
                    {
                        // Password is correct, migrate to BCrypt
                        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, workFactor: 10);
                        user.UpdatedAt = DateTime.Now;
                        _userRepository.Update(user);
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Returns user by username or null when not found.
        /// </summary>
        public UserDTO? GetUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("username is required", nameof(username));
            var user = _userRepository.GetAll().FirstOrDefault(u => 
                string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase));
            return user == null ? null : UserMapper.ToDTO(user);
        }

        /// <summary>
        /// Legacy SHA256 hashing for backward compatibility during migration.
        /// This method is only used to verify old password hashes before migrating to BCrypt.
        /// </summary>
        private string HashPasswordSHA256(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}