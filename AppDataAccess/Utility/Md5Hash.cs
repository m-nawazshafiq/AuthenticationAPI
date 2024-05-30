using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDataAccess.Utility
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class Md5Hash
    {
        // Method to generate a random salt
        public static string GenerateSalt(int length = 16)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var salt = new byte[length];
                rng.GetBytes(salt);
                return Convert.ToBase64String(salt);
            }
        }

        // Method to compute MD5 hash with salt
        public static string ComputeHash(string password, string salt)
        {
            using (var md5 = MD5.Create())
            {
                // Combine the password and salt
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var combinedBytes = new byte[salt.Length + passwordBytes.Length];
                var saltBytes = Encoding.UTF8.GetBytes(salt);
                Buffer.BlockCopy(saltBytes, 0, combinedBytes, 0, saltBytes.Length);
                Buffer.BlockCopy(passwordBytes, 0, combinedBytes, saltBytes.Length, passwordBytes.Length);

                // Compute the hash
                var hashBytes = md5.ComputeHash(combinedBytes);

                // Convert hash bytes to a hexadecimal string
                var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                return hashString;
            }
        }

        // Example usage
        //public static void Main()
        //{
        //    string password = "mypassword";
        //    byte[] salt = GenerateSalt();

        //    string hash = ComputeHash(password, salt);

        //    Console.WriteLine("Password: " + password);
        //    Console.WriteLine("Salt: " + BitConverter.ToString(salt).Replace("-", "").ToLower());
        //    Console.WriteLine("MD5 Hash: " + hash);
        //}
    }

}
