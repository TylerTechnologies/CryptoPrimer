using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Titanium.Security.Cryptography;

namespace DigitalSignature
{

    class Program
    {
        static void Main(string[] args)
        {
            const string sourceFilePath = @"..\..\..\Data.txt";

            // Load data to be signed;
            byte[] data = File.ReadAllBytes(sourceFilePath);

            /***Digital Signing using private key***/
            Console.Write("Enter certificate password: ");
            string certPassword = Console.ReadLine();
            byte[] signature = null;
            using (var rsaCryptoProvider = new RsaCSP(new X509Certificate2(@"..\..\..\Certs\RSA_2048_KeyPair.pfx", certPassword)))
            {
                // The private key is required for signing
                signature = rsaCryptoProvider.Sign(data);
            }

            /***Signature Validation using public key***/
            bool isSignatureValid = false;
            using (var rsaCryptoProvider = new RsaCSP(new X509Certificate2(@"..\..\..\Certs\RSA_2048_PublicKey.cer")))
            {
                // Only the public key is required for signature validation
                isSignatureValid = rsaCryptoProvider.VerifySignature(data, signature);
            }

            Console.WriteLine(Environment.NewLine + $"The digital signature is valid? {isSignatureValid}");
            Console.ReadLine();
        }
    }

}
