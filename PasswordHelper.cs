using System;
using System.Security.Cryptography;
using System.Text;

public class PasswordHelper
{
    public static string HashPassword(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // Compute hash from the password.
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Convert byte array to a string representation (hexadecimal format).
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public static bool VerifyPassword(string enteredPassword, string storedHash)
    {
        // Hash the entered password and compare it to the stored hash.
        string hashOfEnteredPassword = HashPassword(enteredPassword);
        return hashOfEnteredPassword.Equals(storedHash, StringComparison.OrdinalIgnoreCase);
    }
}
