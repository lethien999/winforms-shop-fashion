// Script để generate BCrypt hash cho password "admin123"
// Chạy script này trong một C# project có BCrypt.Net-Next package

using System;
using BCrypt.Net;

namespace Database
{
    class GenerateBCryptHash
    {
        static void Main(string[] args)
        {
            string password = "admin123";
            int workFactor = 10;
            
            // Generate BCrypt hash
            string hash = BCrypt.Net.BCrypt.HashPassword(password, workFactor);
            
            Console.WriteLine("==========================================");
            Console.WriteLine("BCrypt Hash Generator");
            Console.WriteLine("==========================================");
            Console.WriteLine($"Password: {password}");
            Console.WriteLine($"Work Factor: {workFactor}");
            Console.WriteLine();
            Console.WriteLine("Generated Hash:");
            Console.WriteLine(hash);
            Console.WriteLine();
            Console.WriteLine("SQL Update Statement:");
            Console.WriteLine($"UPDATE Users SET PasswordHash = '{hash}' WHERE Username = 'admin';");
            Console.WriteLine();
            Console.WriteLine("Verify hash (should be True):");
            bool isValid = BCrypt.Net.BCrypt.Verify(password, hash);
            Console.WriteLine($"BCrypt.Verify(\"{password}\", hash) = {isValid}");
            Console.WriteLine("==========================================");
        }
    }
}
