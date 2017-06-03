using System;
using NUnit.Framework;
using TeachersDiary.Services.Encrypting;

namespace TeachersDiary.Services.Tests
{
    [TestFixture]
    public class EnctyptingServiceTests
    {
        [Test]
        public void EncodeId_WithValidId_ReturnDecodedId()
        {
            var id = 1;
            IEncryptingService provider = new EncryptingService();

            var encodedId = provider.EncodeId(id);

            Assert.AreEqual("MWFua1NhbGx0", encodedId);
        }

        [Test]
        public void DecodeId_WithValidId_ReturnDecodedId()
        {
            var id = "MWFua1NhbGx0";
            IEncryptingService provider = new EncryptingService();

            var decodedId = provider.DecodeId(id);

            Assert.AreEqual(1, decodedId);
        }

        [Test]
        public void DecodeId_WithNullId_ThrowsArgumentNullException()
        {
            IEncryptingService provider = new EncryptingService();

            Assert.That(() => provider.DecodeId(null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Message.Contain("The argument is null."));
        }
    }
}
