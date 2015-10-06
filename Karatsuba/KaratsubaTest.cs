using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Karatsuba
{
    [TestFixture]
    public class KaratsubaTest
    {
        [Test]
        public void MultiplySingleDigitsCorrectly()
        {
            Assert.AreEqual(new BigInteger(0), Karatsuba.Multiply(0, 9));
            Assert.AreEqual(new BigInteger(15), Karatsuba.Multiply(5, 3));
        }

        [Test]
        public void GetBasePowerReturnsCorrectly()
        {
            Assert.AreEqual(2, Karatsuba.GetBasePower(10));
            Assert.AreEqual(8, Karatsuba.GetBasePower(10000000));

            Assert.AreEqual(10, Karatsuba.GetBasePower(1257896541));
        }

        [Test]
        public void SplitsCorrectly()
        {
            Assert.AreEqual(new BigInteger[] { 12, 34}, Karatsuba.Split(1234, 2));
            Assert.AreEqual(new BigInteger[] { 12, 0 }, Karatsuba.Split(1200, 2));
            Assert.AreEqual(new BigInteger[] { 100, 0 }, Karatsuba.Split(100000, 3));
            Assert.AreEqual(new BigInteger[] { 12578, 96541 }, Karatsuba.Split(1257896541, 5));

            Assert.AreEqual(new BigInteger[] { 12, 345 }, Karatsuba.Split(12345, 3));
            Assert.AreEqual(new BigInteger[] { 10, 0 }, Karatsuba.Split(10000, 3));

            Assert.AreEqual(new BigInteger[] { 12345, 6789}, Karatsuba.Split(123456789, 4));
        }


        [Test]
        public void MultiplesCorrectly()
        {
            Assert.AreEqual(new BigInteger(2500), Karatsuba.Multiply(50, 50));
            Assert.AreEqual(new BigInteger(7006652), Karatsuba.Multiply(1234, 5678));
            Assert.AreEqual(new BigInteger(35936459), Karatsuba.Multiply(4589, 7831));
            Assert.AreEqual(new BigInteger(83810205), Karatsuba.Multiply(12345, 6789));
            Assert.AreEqual(new BigInteger(250000000000), Karatsuba.Multiply(500000, 500000));
        }
    }
}
