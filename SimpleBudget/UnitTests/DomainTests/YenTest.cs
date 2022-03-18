using Microsoft.VisualStudio.TestTools.UnitTesting;
using SB.Domain;
using SB.Domain.ValueObjects;
using System;

namespace UnitTests.DomainTests
{
    [TestClass]
    public class YenTest
    {
        #region ctor test

        /// <summary>
        /// OK：0円
        /// </summary>
        [TestMethod]
        public void InitTest_OK_1()
        {
            var yen = new Yen(0);
            Assert.AreEqual(yen.Value, 0);
        }

        /// <summary>
        /// OK：1円
        /// </summary>
        [TestMethod]
        public void InitTest_OK_2()
        {
            var yen = new Yen(1);
            Assert.AreEqual(yen.Value, 1);
        }

        /// <summary>
        /// NG：負の値は許されない
        /// </summary>
        [TestMethod]
        public void InitTest_NG_1()
        {
            try
            {
                var yen = new Yen(-1);
            }
            catch(Exception ex)
            {
                Assert.AreEqual(ex.Message, DomainMessages.M_001);
            }
        }

        #endregion

        #region WithComma property test

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

        #endregion
    }
}
