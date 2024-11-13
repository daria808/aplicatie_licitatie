using System;
using System.Collections.Generic; // For database simulation
using System.Security.Cryptography;
using System.Text;

public class AuthenticationHandler
{
    private Dictionary<string, string> userDatabase = new Dictionary<string, string>(); // Simulated database

    public bool RegisterUser(string username, string password)
    {
        if (userDatabase.ContainsKey(username))
        {
            Console.WriteLine("User already exists.");
            return false; // Username is taken
        }

        string hashedPassword = HashPassword(password);
        userDatabase[username] = hashedPassword;
        Console.WriteLine("User registered successfully.");
        return true;
    }

    public bool AuthenticateUser(string username, string password)
    {
        if (!userDatabase.ContainsKey(username))
        {
            Console.WriteLine("User not found.");
            return false;
        }

        string storedHashedPassword = userDatabase[username];
        if (VerifyPassword(password, storedHashedPassword))
        {
            Console.WriteLine("Authentication successful.");
            return true;
        }
        else
        {
            Console.WriteLine("Invalid password.");
            return false;
        }
    }

    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }

    private bool VerifyPassword(string password, string hashedPassword)
    {
        string hashedInputPassword = HashPassword(password);
        return hashedInputPassword == hashedPassword;
    }
}
