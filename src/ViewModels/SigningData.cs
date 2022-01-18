using System.Security.Cryptography;
using System.Text;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

namespace src.Controllers
{
    public static class SigningData
    {
        private static byte[] _RSAkey; 

        public static async Task SetPrivateKey(string vaultUrl, string keyName)
        {
            var client = new SecretClient(new Uri(vaultUrl), new DefaultAzureCredential());
            var secret = await client.GetSecretAsync(keyName);
            _RSAkey = Convert.FromBase64String(secret.Value.Value);
        }

        public static async Task<string> EncryptData(string message)
        {
            /// The array to store the signed message in bytes
            byte[] signedBytes;
            using (var rsa = new RSACryptoServiceProvider())
            {
                /// Write the message to a byte array using UTF8 as the encoding.
                var encoder = new UTF8Encoding();
                byte[] originalData = encoder.GetBytes(message);

                try
                {
                    /// Import the private key used for signing the message
                    rsa.ImportRSAPrivateKey(_RSAkey, out _);

                    /// Sign the data, using SHA512 as the hashing algorithm 
                    signedBytes = rsa.SignData(originalData, CryptoConfig.MapNameToOID("SHA256"));
                }
                catch (CryptographicException e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
                finally
                {
                    /// Set the keycontainer to be cleared when rsa is garbage collected.
                    rsa.PersistKeyInCsp = false;
                }
            }
            /// Convert the a base64 to string before returning
            return Convert.ToBase64String(signedBytes);
        }
    }
}