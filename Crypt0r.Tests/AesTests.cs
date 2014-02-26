using System;
using System.Security.Cryptography;
using NUnit.Framework;

namespace Crypt0r.Tests
{
    [TestFixture]
    public class AesTests
    {
        public class Encrypt
        {
            [Test]
            public void ResultIsDifferentFromPlainText()
            {
                const string plainText = "password";
                const string key = "randomkey";

                var result = Aes.Encrypt(plainText, key);
                
                Assert.That(result, Is.Not.EqualTo(plainText));
            }

            [Test]
            public void ReturnsString_WhenArgsAreValid()
            {
                const string value = "password";
                const string key = "randomkey";

                var result = Aes.Encrypt(value, key);
                
                Assert.That(result, Is.TypeOf<string>());
            }            

            [Test]
            public void ThrowsArgumentNullException_WhenKeyIsNull()
            {                
                const string plainText = "password";

                Assert.Throws(Is.TypeOf<ArgumentNullException>()
                    .And.Message.StringContaining("Parameter name: key"),
                    () => Aes.Encrypt(plainText, null));
            }

            [Test]
            public void ThrowsArgumentNullException_WhenPlainTextIsNull()
            {
                const string key = "key";

                Assert.Throws(Is.TypeOf<ArgumentNullException>()
                    .And.Message.StringContaining("Parameter name: plainText"),
                    () => Aes.Encrypt(null, key));
            }
        }

        public class Decrypt
        {
            [Test]
            public void ResultIsDifferentFromCipherText()
            {
                const string cipherText = "awDXEkj0uRKq2mjBVTttzhRwBhfk+DYmoq5B1+jvyIF9ens/mCqSjJ8gC55WQqA4nxZgT5yEb0HMnZ9RkiWZYg==";
                const string key = "this is a random key";

                var result = Aes.Decrypt(cipherText, key);

                Assert.That(result, Is.Not.EqualTo(cipherText));
            }

            [Test]
            public void Roundtrip()
            {
                const string plainText = "a plain text string";
                const string key = "this is a random key";

                var cipherText = Aes.Encrypt(plainText, key);
                var result = Aes.Decrypt(cipherText, key);

                Assert.That(result, Is.EqualTo(plainText));
            }

            [Test]
            public void ReturnsString_WhenArgsAreValid()
            {
                const string cipherText = "awDXEkj0uRKq2mjBVTttzhRwBhfk+DYmoq5B1+jvyIF9ens/mCqSjJ8gC55WQqA4nxZgT5yEb0HMnZ9RkiWZYg==";
                const string key = "this is a random key";

                var result = Aes.Decrypt(cipherText, key);

                Assert.That(result, Is.TypeOf<string>());
            }

            [Test]
            public void ThrowsArgumentNullException_WhenCipherTextIsNull()
            {
                const string key = "key";

                Assert.Throws(Is.TypeOf<ArgumentNullException>()
                    .And.Message.StringContaining("Parameter name: cipherText"),
                    () => Aes.Decrypt(null, key));
            }

            [Test]
            public void ThrowsArgumentNullException_WhenKeyIsNull()
            {
                const string value = "password";

                Assert.Throws(Is.TypeOf<ArgumentNullException>()
                    .And.Message.StringContaining("Parameter name: key"),
                    () => Aes.Encrypt(value, null));
            }

            [Test]
            public void ThrowsCryptographicException_WhenKeyIsDifferentFromOriginal()
            {
                const string plainText = "a plain text string";
                const string encryptKey = "this is a random key";
                const string decryptKey = "completelly different key";

                var cipherText = Aes.Encrypt(plainText, encryptKey);

                Assert.Throws(Is.TypeOf<CryptographicException>(),
                    () => Aes.Decrypt(cipherText, decryptKey));
            }
        }
    }
}
