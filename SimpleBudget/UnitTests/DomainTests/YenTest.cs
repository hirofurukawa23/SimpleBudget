using Microsoft.VisualStudio.TestTools.UnitTesting;
using SB.Domain.ValueObjects;

namespace UnitTests.DomainTests
{
    [TestClass]
    public class YenTest
    {
        [TestMethod]
        public void InitTest_OK_1()
        {
            var yen = new Yen(0);
            Assert.AreEqual(yen.AbsValue, 0);
        }

        [TestMethod]
        public void InitTest_OK_2()
        {
            var yen = new Yen(1);
            Assert.AreEqual(yen.AbsValue, 1);
        }

        [TestMethod]
        public void InitTest_OK_3()
        {
            var yen = new Yen(-1);
            Assert.IsTrue(yen.IsNegative);
            Assert.AreEqual(yen.AbsValue, 1);
        }

        [TestMethod]
        public void WithCommaTest_1()
        {
            var yen = new Yen(100);
            Assert.AreEqual(yen.WithComma, "100");
        }

        [TestMethod]
        public void WithCommaTest_2()
        {
            var yen = new Yen(1000);
            Assert.AreEqual(yen.WithComma, "1,000");
        }

        [TestMethod]
        public void WithCommaTest_3()
        {
            var yen = new Yen(1000000);
            Assert.AreEqual(yen.WithComma, "1,000,000");
        }

        [TestMethod]
        public void EqualTest_OK()
        {
            var yen1 = new Yen(1);
            var yen2 = new Yen(1);
            var isEqual = yen1.Equals(yen2);
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void EqualTest_OK_2()
        {
            var yen1 = new Yen(1);
            var isEqual = yen1.Equals(yen1);
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void EqualTest_NG()
        {
            var yen1 = new Yen(1);
            var yen2 = new Yen(2);
            var isEqual = yen1.Equals(yen2);
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void EqualTest_NG_2()
        {
            var yen1 = new Yen(1);
            var yen2 = new Yen(-1);
            var isEqual = yen1.Equals(yen2);
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void AddTest_1()
        {
            var yen1 = new Yen(1);
            var yen2 = new Yen(2);
            var added = yen1.Add(yen2);
            Assert.AreEqual(added.AbsValue, 3);
            Assert.AreEqual(added.RelativeValue, 3);
        }

        [TestMethod]
        public void AddTest_2()
        {
            var yen1 = new Yen(-1);
            var yen2 = new Yen(1);
            var added = yen1.Add(yen2);
            Assert.AreEqual(added.AbsValue, 0);
            Assert.AreEqual(added.RelativeValue, 0);
        }

        [TestMethod]
        public void AddTest_3()
        {
            var yen1 = new Yen(-2);
            var yen2 = new Yen(1);
            var added = yen1.Add(yen2);
            Assert.AreEqual(added.AbsValue, 1);
            Assert.IsTrue(added.IsNegative);
            Assert.AreEqual(added.RelativeValue, -1);
        }

        [TestMethod]
        public void SubtractTest_1()
        {
            var yen1 = new Yen(2);
            var yen2 = new Yen(1);
            var subed = yen1.Subtract(yen2);
            Assert.AreEqual(subed.AbsValue, 1);
            Assert.IsFalse(subed.IsNegative);
        }

        [TestMethod]
        public void SubtractTest_2()
        {
            var yen1 = new Yen(1);
            var yen2 = new Yen(1);
            var subed = yen1.Subtract(yen2);
            Assert.AreEqual(subed.AbsValue, 0);
            Assert.AreEqual(subed.RelativeValue, 0);
            Assert.IsFalse(subed.IsNegative);
        }

        [TestMethod]
        public void SubtractTest_3()
        {
            var yen1 = new Yen(-2);
            var yen2 = new Yen(-1);
            var subed = yen1.Subtract(yen2);
            Assert.AreEqual(subed.AbsValue, 1);
            Assert.AreEqual(subed.RelativeValue, -1);
            Assert.IsTrue(subed.IsNegative);
        }

        [TestMethod]
        public void SubtractTest_4()
        {
            var yen1 = new Yen(-2);
            var yen2 = new Yen(1);
            var subed = yen1.Subtract(yen2);
            Assert.AreEqual(subed.AbsValue, 3);
            Assert.AreEqual(subed.RelativeValue, -3);
            Assert.IsTrue(subed.IsNegative);
        }

        [TestMethod]
        public void EqualOperatorTest_1()
        {
            var yen1 = new Yen(1);
            var yen2 = new Yen(1);
            var isEqual = yen1 == yen2;
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void EqualOperatorTest_2()
        {
            var yen1 = new Yen(1);
            var yen2 = new Yen(2);
            var isEqual = yen1 == yen2;
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void EqualOperatorTest_3()
        {
            var yen1 = new Yen(-1);
            var yen2 = new Yen(-1);
            var isEqual = yen1 == yen2;
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void EqualOperatorTest_4()
        {
            var yen1 = new Yen(-1);
            var yen2 = new Yen(-2);
            var isEqual = yen1 == yen2;
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void EqualOperatorTest_5()
        {
            var yen1 = new Yen(-1);
            var yen2 = new Yen(1);
            var isEqual = yen1 == yen2;
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void NotEqualOperatorTest_1()
        {
            var yen1 = new Yen(1);
            var yen2 = new Yen(1);
            var isEqual = yen1 != yen2;
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void NotEqualOperatorTest_2()
        {
            var yen1 = new Yen(1);
            var yen2 = new Yen(2);
            var isEqual = yen1 != yen2;
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void NotEqualOperatorTest_3()
        {
            var yen1 = new Yen(-1);
            var yen2 = new Yen(-1);
            var isEqual = yen1 != yen2;
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void NotEqualOperatorTest_4()
        {
            var yen1 = new Yen(-1);
            var yen2 = new Yen(-2);
            var isEqual = yen1 != yen2;
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void NotEqualOperatorTest_5()
        {
            var yen1 = new Yen(-1);
            var yen2 = new Yen(1);
            var isEqual = yen1 != yen2;
            Assert.IsTrue(isEqual);
        }
    }
}
