using System;
using Titanium.Security.Cryptography;

namespace PasswordHash
{
    class Program
    {
        static void Main(string[] args)
        {
            const string PASSWORD = "Passw@rd2";
            Console.WriteLine($"Password: {PASSWORD}");

            // Hash the password for storage.
            // If you do not provide a salt, one will be created and returned.
            (byte[] salt, byte[] hash) = CryptoPrimitives.DeriveHash(PASSWORD);
            
            // You would encrypt and store the salt with the hash.
            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");
            Console.WriteLine($"Hash: {Convert.ToBase64String(hash)}");

            bool isValid = false;
            do
            {
                Console.Write(Environment.NewLine + "Enter Password: ");
                string testPassword = Console.ReadLine();
                isValid = CryptoPrimitives.ValidatePassword(testPassword, hash, salt);
                Console.WriteLine($"Entered Password matches? {isValid}");
            } while (!isValid);


            Console.ReadLine();
        }
    }
}
