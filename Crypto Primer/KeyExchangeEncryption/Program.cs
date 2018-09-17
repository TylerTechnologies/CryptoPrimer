using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Titanium.Security.Cryptography;

namespace KeyExchangeEncryption
{

    class Program
    {
        static void Main(string[] args)
        {
            //Note: Data length cannot exceed key length
            byte[] symmetricEncryptionKey = CryptoPrimitives.GenerateKey(64);

            /*** Encryption using public key***/
            byte[] encryptedKey = null;
            using (var rsaCryptoProvider = new RsaCSP(new X509Certificate2(@"..\..\..\Certs\RSA_2048_PublicKey.cer")))
            {
                // Only the public key is needed for encryption
                // The symmetric encryption key can be safely shared after encryption.
                encryptedKey = rsaCryptoProvider.Encrypt(symmetricEncryptionKey);
            }

            /*** Decryption using private key***/
            Console.Write("Enter certificate password: ");
            string certPassword = Console.ReadLine();
            byte[] decryptedKey = null;
            using (var rsaCryptoProvider = new RsaCSP(new X509Certificate2(@"..\..\..\Certs\RSA_2048_KeyPair.pfx", certPassword)))
            {
                decryptedKey = rsaCryptoProvider.Decrypt(encryptedKey);
            }

            Console.WriteLine(Environment.NewLine + $"Decrypted key matches the original key? {symmetricEncryptionKey.SequenceEqual(decryptedKey)}");
            Console.ReadLine();
        }
    }

}
