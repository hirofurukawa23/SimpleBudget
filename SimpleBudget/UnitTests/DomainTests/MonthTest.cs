using Microsoft.VisualStudio.TestTools.UnitTesting;
using SB.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.DomainTests
{
    [TestClass]
    public class MonthTest
    {
        [TestMethod]
        public void CtorTest()
        {
            var month = new Month(2022, 3);
            Assert.IsNotNull(month);
        }

        [TestMethod]
        public void PropertiesTest()
        {
            var month = new Month(2022, 3);
            Assert.AreEqual(month.Year, 2022);
            Assert.AreEqual(month.Months, 3);
            Assert.IsTrue(month.StartDate.Equals(new Date(2022, 3, 1)));
            Assert.IsTrue(month.EndDate.Equals(new Date(2022, 3, 31)));
            Assert.AreEqual(month.DaysCount, 31);
        }

        [TestMethod]
        public void EqualsTest_1()
        {
            var month = new Month(2022, 3);
            var isEqual = month.Equals(month);
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void EqualsTest_2()
        {
            var month1 = new Month(2022, 3);
            var month2 = new Month(2022, 3);
            var isEqual = month1.Equals(month2);
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void EqualsTest_3()
        {
            var month1 = new Month(2022, 3);
            var month2 = new Month(2022, 4);
            var isEqual = month1.Equals(month2);
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void EqualsOpeTest_1()
        {
            var month = new Month(2022, 3);
            var isEqual = month == month;
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void EqualsOpeTest_2()
        {
            var month1 = new Month(2022, 3);
            var month2 = new Month(2022, 3);
            var isEqual = month1 == month2;
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void EqualsOpeTest_3()
        {
            var month1 = new Month(2022, 3);
            var month2 = new Month(2022, 4);
            var isEqual = month1 == month2;
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void NotEqualsOpeTest_1()
        {
            var month = new Month(2022, 3);
            var isEqual = month != month;
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void NotEqualsOpeTest_2()
        {
            var month1 = new Month(2022, 3);
            var month2 = new Month(2022, 3);
            var isEqual = month1 != month2;
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void NotEqualsOpeTest_3()
        {
            var month1 = new Month(2022, 3);
            var month2 = new Month(2022, 4);
            var isEqual = month1 != month2;
            Assert.IsTrue(isEqual);
        }
    }
}
