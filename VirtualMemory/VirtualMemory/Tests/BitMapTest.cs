using NUnit.Framework;
namespace VirtualMemory.Tests
{
    [TestFixture()]
    public class BitMapTest
    {
        BitMap map;

        [SetUp]
        public void TestSetup()
        {
            map = new BitMap(64);
        }

        [Test]
        public void SetBit_Success()
        {
            map.SetBit(0);
            map.SetBit(1);

            Assert.AreEqual(1, map.GetBitAt(0));
        }

        [Test]
        public void GetNextOpenBit_ReturnsValue()
        {
            Assert.AreEqual(0, map.GetNextFreeBit());
            map.SetBit(0);
            Assert.AreEqual(1, map.GetNextFreeBit());
            map.SetBit(1);
            Assert.AreEqual(2, map.GetNextFreeBit());

            for (int i = 0; i < 10; i++)
                map.SetBit(i);

            Assert.AreEqual(10, map.GetNextFreeBit());
        }

        [Test]
        public void GetBitAt_ReturnsTrueValue()
        {
            Assert.AreEqual(0, map.GetBitAt(0));
            Assert.AreEqual(0, map.GetBitAt(1));
            map.SetBit(10);
            Assert.AreEqual(1, map.GetBitAt(10));
            map.SetBit(63);
            Assert.AreEqual(1, map.GetBitAt(63));
        }

    }
}