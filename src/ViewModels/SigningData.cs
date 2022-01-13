using System.Security.Cryptography;
using System.Text;

namespace src.Controllers
{
    public static class SigningData
    {
        public static string encryptData(string message)
        {
            byte[] RSAkey;

            try
            {
                RSAkey = Convert.FromBase64String(System.IO.File.ReadAllText(@"C:\Users\Public\HHSKeys\WriteText.txt"));
            }
            catch (FileNotFoundException ex)
            {
                throw new Exception("Can't find private key", ex);
            }

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
                    rsa.ImportRSAPrivateKey(RSAkey, out _);

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