using System;
using System.IO;
using Titanium.Security.Cryptography;

namespace BulkEncryption
{

    class Program
    {
        static void Main(string[] args)
        {
            const string PASSWORD = "Passw@rd2";
            const string sourceFilePath = @"..\..\..\Data.txt";
            const string targetFilePath = @"..\..\..\Data.target.txt";

            // Read data from File;
            byte[] data = File.ReadAllBytes(sourceFilePath);

            SymmetricCSP cryptoProvider = SymmetricCSP.CreateAesProvider();

            // Encrypt data
            byte[] cipherPackage = cryptoProvider.Encrypt(data, PASSWORD);
            // Replace file content with encrypted data
            File.WriteAllBytes(targetFilePath, cipherPackage);

            Console.WriteLine("Encrypted data writen to Target file.");
            Console.ReadLine();

            // Decrypt data
            byte[] plainText = cryptoProvider.Decrypt(cipherPackage, PASSWORD);
            // Write plain text to file
            File.WriteAllBytes(targetFilePath, plainText);

            Console.WriteLine("Decrypted data writen to Target file.");
            Console.ReadLine();
        }
    }

}
