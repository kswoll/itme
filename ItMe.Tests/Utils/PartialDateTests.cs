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

        [Test]
        public void FullDate()
        {
            Assert.AreEqual("February 3, 2018", new PartialDate(2018, 2, 3).ToString());
        }

        [Test]
        public void MonthAndYear()
        {
            Assert.AreEqual("February 2018", new PartialDate(2018, 2).ToString());
        }

        [Test]
        public void Year()
        {
            Assert.AreEqual("2018", new PartialDate(2018).ToString());
        }

        [Test]
        public void ParseYear()
        {
            var date = PartialDate.Parse("2018");
            Assert.AreEqual(2018, date.Year);
            Assert.IsNull(date.Month);
            Assert.IsNull(date.Day);
        }

        [Test]
        public void ParseMonthAndYear()
        {
            var date = PartialDate.Parse("July 2018");
            Assert.AreEqual(2018, date.Year);
            Assert.AreEqual(7, date.Month);
            Assert.IsNull(date.Day);
        }

        [Test]
        public void ParseFullDate()
        {
            var date = PartialDate.Parse("July 14, 2018");
            Assert.AreEqual(2018, date.Year);
            Assert.AreEqual(7, date.Month);
            Assert.AreEqual(14, date.Day);
        }
    }
}