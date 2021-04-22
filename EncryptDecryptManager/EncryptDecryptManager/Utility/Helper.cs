using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EncryptDecryptManager.Utility
{
    static class Helper
    {
        private const string IV = "9234122723415343";
        public static string EncryptString(string key, string plainText)
        {
            byte[] encrypted;
            // Create an Aes object
            // with the specified key and IV.
            using (var aesAlg = Aes.Create())
            {
                //aesAlg.Key = key;
                //aesAlg.IV = iv;
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(IV);

                // Create a decrytor to perform the stream transform.
                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return Convert.ToBase64String(encrypted);

        }

        public static string DecryptString(string key, string cipherText)
        {
            // Declare the string used to hold
            // the decrypted text.
            string plaintext;

            // Create an Aes object
            // with the specified key and IV.
            using (var aesAlg = Aes.Create())
            {
                //aesAlg.Key = key;
                //aesAlg.IV = iv;
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(IV);

                // Create a decrytor to perform the stream transform.
                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }
    }
}
