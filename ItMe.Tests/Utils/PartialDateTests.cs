using ItMe.Utils;
using NUnit.Framework;

namespace ItMe.Tests.Utils
{
    [TestFixture]
    public class PartialDateTests
    {
        [Test]
        public void Encode()
        {
            Assert.AreEqual("2018-2-3", new PartialDate(2018, 2, 3).Encode());
            Assert.AreEqual("?-2-3", new PartialDate(null, 2, 3).Encode());
            Assert.AreEqual("?-?-?", new PartialDate(null, null, null).Encode());
        }

        [Test]
        public void Decode()
        {
            Assert.AreEqual(new PartialDate(2018, 2, 3), PartialDate.Decode("2018-2-3"));
            Assert.AreEqual(new PartialDate(null, 2, 3), PartialDate.Decode("?-2-3"));
            Assert.AreEqual(new PartialDate(null, null, 3), PartialDate.Decode("?-?-3"));
            Assert.AreEqual(new PartialDate(null, null, null), PartialDate.Decode("?-?-?"));
        }
    }
}