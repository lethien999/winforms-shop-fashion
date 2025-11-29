// Quick script to generate BCrypt hash
// Compile and run: csc GenerateHash.cs /r:BCrypt.Net-Next.dll
// Or use in a C# project with BCrypt.Net-Next package

using System;
using BCrypt.Net;

class GenerateHash
{
    static void Main()
    {
        string password = "admin123";
        int workFactor = 10;
        
        string hash = BCrypt.Net.BCrypt.HashPassword(password, workFactor);
        
        Console.WriteLine("Generated BCrypt Hash for 'admin123':");
        Console.WriteLine(hash);
        Console.WriteLine();
        Console.WriteLine("SQL Update Statement:");
        Console.WriteLine($"UPDATE Users SET PasswordHash = '{hash}' WHERE Username = 'admin';");
    }
}

