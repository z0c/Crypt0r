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
            public void ReturnsString_WhenArgsAreValid()
            {
                const string value = "password";
                const string key = "randomkey";

                var result = Aes.Encrypt(value, key);

                Assert.That(result, Is.TypeOf(typeof(string)));
            }

            [Test]
            public void ResultIsDifferentFromValue()
            {
                const string value = "password";
                const string key = "randomkey";

                var result = Aes.Encrypt(value, key);

                Assert.That(result, Is.Not.EqualTo(value));
            }

            [Test]
            public void ThrowArgumentNullException_WhenKeyIsEmpty()
            {                
                const string value = "password";

                Assert.Throws(Is.TypeOf<ArgumentNullException>()
                    .And.Message.StringContaining("Parameter name: key"),
                    () => Aes.Encrypt(value, string.Empty));
            }

            [Test]
            public void ThrowArgumentNullException_WhenKeyIsNull()
            {                
                const string value = "password";

                Assert.Throws(Is.TypeOf<ArgumentNullException>()
                    .And.Message.StringContaining("Parameter name: key"),
                    () => Aes.Encrypt(value, null));
            }

            [Test]
            public void ThrowArgumentNullException_WhenValueIsEmpty()
            {
                const string key = "key";

                Assert.Throws(Is.TypeOf<ArgumentNullException>()
                    .And.Message.StringContaining("Parameter name: value"),
                    () => Aes.Encrypt(string.Empty, key));
            }

            [Test]
            public void ThrowArgumentNullException_WhenValueIsNull()
            {
                const string key = "key";

                Assert.Throws(Is.TypeOf<ArgumentNullException>()
                    .And.Message.StringContaining("Parameter name: value"),
                    () => Aes.Encrypt(null, key));
            }
        }
    }
}
