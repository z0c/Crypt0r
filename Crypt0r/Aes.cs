using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Crypt0r
{
    public static class Aes
    {
        public static string Encrypt(string plainText, string key, int saltSize = 32)
        {
            if (plainText == null) throw new ArgumentNullException("plainText");
            if (key == null) throw new ArgumentNullException("key");

            using(var derivationKey = new Rfc2898DeriveBytes(key, saltSize))
            {
                var saltBytes = derivationKey.Salt;
                var keyBytes = derivationKey.GetBytes(32);
                var ivBytes = derivationKey.GetBytes(16);

                using (var aesManaged = new AesManaged())
                using (var encryptor = aesManaged.CreateEncryptor(keyBytes, ivBytes))
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    var cipherBytes = msEncrypt.ToArray();

                    Array.Resize(ref saltBytes, saltBytes.Length + cipherBytes.Length);
                    Array.Copy(cipherBytes, 0, saltBytes, saltSize, cipherBytes.Length);

                    return Convert.ToBase64String(saltBytes);
                }
            }
        }

        public static string Decrypt(string cipherText, string key, int saltSize = 32)
        {
            if (cipherText == null) throw new ArgumentNullException("cipherText");
            if (key == null) throw new ArgumentNullException("key");

            var cipherBytes = Convert.FromBase64String(cipherText);
            var saltBytes = cipherBytes.Take(saltSize).ToArray();
            var cipherTextBytes = cipherBytes.Skip(saltSize).Take(cipherBytes.Length - saltSize).ToArray();
            var derivationKey = new Rfc2898DeriveBytes(key, saltBytes);
            var keyBytes = derivationKey.GetBytes(32);
            var ivBytes = derivationKey.GetBytes(16);

            using (var aesManaged = new AesManaged())
            using (var decryptor = aesManaged.CreateDecryptor(keyBytes, ivBytes))
            using (var memoryStream = new MemoryStream(cipherTextBytes))
            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
            using (var streamReader = new StreamReader(cryptoStream))
                return streamReader.ReadToEnd();
        }
    }
}
