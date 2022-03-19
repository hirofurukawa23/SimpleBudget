using Microsoft.VisualStudio.TestTools.UnitTesting;
using SB.Domain.ValueObjects;
using System;

namespace UnitTests.DomainTests
{
    [TestClass]
    public class DateTest
    {
        [TestMethod]
        public void CtorTest()
        {
            var date = new Date(2022, 3, 22);
            Assert.IsNotNull(date);
        }

        [TestMethod]
        public void DateTest_1()
        {
            var dateTime = new Date(2022, 3, 22);
            Assert.AreEqual(dateTime.Year, 2022);
            Assert.AreEqual(dateTime.Month, 3);
            Assert.AreEqual(dateTime.Day, 22);
        }

        [TestMethod]
        public void DayOfWeekTest()
        {
            var dateTime = new Date(2022, 3, 19);
            Assert.AreEqual(dateTime.DayOfWeek, DayOfWeek.Saturday);
        }

        [TestMethod]
        public void DayOfWeekInJpTest()
        {
            var dateTime = new Date(2022, 3, 19);
            Assert.AreEqual(dateTime.DayOfWeekInJapanese, "土曜日");
        }

        [TestMethod]
        public void EqualsTest()
        {
            var dateTime1 = new Date(2022, 3, 19);
            var isEqual = dateTime1.Equals(dateTime1);
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void EqualsTest2()
        {
            var dateTime1 = new Date(2022, 3, 19);
            var dateTime2 = new Date(2022, 3, 19);
            var isEqual = dateTime1.Equals(dateTime2);
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void EqualsTest3()
        {
            var dateTime1 = new Date(2022, 3, 19);
            var dateTime2 = new Date(2022, 3, 20);
            var isEqual = dateTime1.Equals(dateTime2);
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void EqualsTest4()
        {
            var dateTime1 = new Date(2022, 3, 19);
            var dateTime2 = new Date(2022, 3, 19);
            var isEqual = dateTime1 == dateTime2;
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void EqualsTest5()
        {
            var dateTime1 = new Date(2022, 3, 19);
            var dateTime2 = new Date(2022, 3, 20);
            var isEqual = dateTime1 == dateTime2;
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void EqualsTest6()
        {
            var dateTime1 = new Date(2022, 3, 19);
            var dateTime2 = new Date(2022, 3, 19);
            var isNoEqual = dateTime1 != dateTime2;
            Assert.IsFalse(isNoEqual);
        }

        [TestMethod]
        public void EqualsTest7()
        {
            var dateTime1 = new Date(2022, 3, 19);
            var dateTime2 = new Date(2022, 3, 20);
            var isNoEqual = dateTime1 != dateTime2;
            Assert.IsTrue(isNoEqual);
        }
    }
}
