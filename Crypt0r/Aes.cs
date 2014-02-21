using System;
using System.Security.Cryptography;

namespace Crypt0r
{
    public static class Aes
    {
        public static string Encrypt(string value, string key)
        {            
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException("value");
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException("key");

            var encrypted = value;
            var derivationKey = new Rfc2898DeriveBytes(key, 32);

            return encrypted;
        }
    }
}
