using System;
using System.IO;
using System.Security.Cryptography;

namespace Crypt0r
{
    public static class Aes
    {
        public static string Encrypt(string value, string key, int saltSize = 32)
        {            
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException("value");
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException("key");

            var derivationKey = new Rfc2898DeriveBytes(key, 32);
            var salt = derivationKey.Salt;

            using (var aesManaged = new AesManaged())
            {
                var encryptor = aesManaged.CreateEncryptor(aesManaged.Key, aesManaged.IV);
               
                using (var msEncrypt = new MemoryStream())
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (var swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(value);
                    var bytes = msEncrypt.ToArray();

                    Array.Resize(ref salt, salt.Length + bytes.Length);
                    Array.Copy(bytes, 0, salt, saltSize, bytes.Length);

                    return Convert.ToBase64String(salt);
                }
            }            
        }
    }
}
