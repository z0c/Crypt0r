using System;
using System.IO;
using System.Security.Cryptography;

namespace Crypt0r
{
    public static class Aes
    {
        public static string Encrypt(string plainText, string key, int saltSize = 32)
        {
            if (plainText == null) throw new ArgumentNullException("plainText");
            if (key == null) throw new ArgumentNullException("key");

            var derivationKey = new Rfc2898DeriveBytes(key, 32);
            var salt = derivationKey.Salt;

            using (var aesManaged = new AesManaged())
            {
                var encryptor = aesManaged.CreateEncryptor(aesManaged.Key, aesManaged.IV);
               
                using (var msEncrypt = new MemoryStream())
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (var swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                    var bytes = msEncrypt.ToArray();

                    Array.Resize(ref salt, salt.Length + bytes.Length);
                    Array.Copy(bytes, 0, salt, saltSize, bytes.Length);

                    return Convert.ToBase64String(salt);
                }
            }            
        }

        public static string Decrypt(string cypherText, string key, int saltSize = 32)
        {
            if (cypherText == null) throw new ArgumentNullException("cypherText");
            if (key == null) throw new ArgumentNullException("key");

            return cypherText;
        }
    }
}
