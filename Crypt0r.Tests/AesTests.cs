using System;
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
            public void ThrowArgumentNullException_WhenKeyIsNull()
            {                
                const string plainText = "password";

                Assert.Throws(Is.TypeOf<ArgumentNullException>()
                    .And.Message.StringContaining("Parameter name: key"),
                    () => Aes.Encrypt(plainText, null));
            }

            [Test]
            public void ThrowArgumentNullException_WhenPlainTextIsNull()
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
            public void ReturnsString_WhenArgsAreValid()
            {
                const string cypherText = "password";
                const string key = "randomkey";

                var result = Aes.Decrypt(cypherText, key);

                Assert.That(result, Is.TypeOf<string>());
            }

            [Test]
            public void ThrowArgumentNullException_WhenCypherTextIsNull()
            {
                const string key = "key";

                Assert.Throws(Is.TypeOf<ArgumentNullException>()
                    .And.Message.StringContaining("Parameter name: cypherText"),
                    () => Aes.Decrypt(null, key));
            }

            [Test]
            public void ThrowArgumentNullException_WhenKeyIsNull()
            {
                const string value = "password";

                Assert.Throws(Is.TypeOf<ArgumentNullException>()
                    .And.Message.StringContaining("Parameter name: key"),
                    () => Aes.Encrypt(value, null));
            }

        }
    }
}
