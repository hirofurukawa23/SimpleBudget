using Microsoft.VisualStudio.TestTools.UnitTesting;
using SB.Domain.ValueObjects;

namespace UnitTests.DomainTests
{
    [TestClass]
    public class MemoTest
    {
        [TestMethod]
        public void CtorTest()
        {
            var memo = new Memo("");
            Assert.IsNotNull(memo);
        }

        [TestMethod]
        public void ValueTest_1()
        {
            var memo = new Memo("");
            Assert.AreEqual(memo.Value, "");
        }

        [TestMethod]
        public void ValueTest_2()
        {
            var text = @"ABCDEFG";
            var memo = new Memo(text);
            Assert.AreEqual(memo.Value, "ABCDEFG");
        }

        [TestMethod]
        public void ValueTest_3()
        {
            var text = @"ABCDEFG
HIJKLMN";
            var memo = new Memo(text);
            Assert.AreEqual(memo.Value, @"ABCDEFG
HIJKLMN");
        }

        [TestMethod]
        public void ValueTest_4()
        {
            var text = "ABCDEFG\nHIJKLMN";
            var memo = new Memo(text);
            Assert.AreEqual(memo.Value, "ABCDEFG\nHIJKLMN");
        }

        [TestMethod]
        public void LengthTest_1()
        {
            var memo = new Memo("");
            Assert.AreEqual(memo.Length, 0);
        }

        [TestMethod]
        public void LengthTest_2()
        {
            var memo = new Memo("ABC");
            Assert.AreEqual(memo.Length, 3);
        }

        [TestMethod]
        public void LengthTest_3()
        {
            var memo = new Memo("ABC\nDE");
            Assert.AreEqual(memo.Length, 5);
        }

        [TestMethod]
        public void LengthTest_4()
        {
            var memo = new Memo(@"ABC
DE");
            Assert.AreEqual(memo.Length, 5);
        }

        [TestMethod]
        public void EqualTest_1()
        { //自己等価の比較
            var memo1 = new Memo("");
            var isEqual = memo1.Equals(memo1);
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void EqualTest_2()
        {
            var memo1 = new Memo("");
            var memo2 = new Memo("");
            var isEqual = memo1.Equals(memo2);
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void EqualTest_3()
        {
            var memo1 = new Memo("ABC");
            var memo2 = new Memo("ABC");
            var isEqual = memo1.Equals(memo2);
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void EqualTest_4()
        {
            var memo1 = new Memo("ABC\nDE");
            var memo2 = new Memo("ABC\nDE");
            var isEqual = memo1.Equals(memo2);
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void EqualTest_5()
        {
            var memo1 = new Memo(@"ABC
DE");
            var memo2 = new Memo(@"ABC
DE");
            var isEqual = memo1.Equals(memo2);
            Assert.IsTrue(isEqual);
        }


        [TestMethod]
        public void NotEqualTest_1()
        {
            var memo1 = new Memo("");
            var memo2 = new Memo("");
            var isEqual = memo1.Equals(memo2);
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void NotEqualTest_2()
        {
            var memo1 = new Memo("ABC");
            var memo2 = new Memo("ABD");
            var isEqual = memo1.Equals(memo2);
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void NotEqualTest3()
        {
            var memo1 = new Memo("ABC\nDE");
            var memo2 = new Memo("ABC\nDF");
            var isEqual = memo1.Equals(memo2);
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void NotEqualTest_4()
        {
            var memo1 = new Memo(@"ABC
DE");
            var memo2 = new Memo(@"ABC
DF");
            var isEqual = memo1.Equals(memo2);
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void EqualOpeTest_1()
        { //自己等価の比較
            var memo1 = new Memo("");
            var isEqual = memo1 == memo1;
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void EqualOpeTest_2()
        {
            var memo1 = new Memo("");
            var memo2 = new Memo("");
            var isEqual = memo1 == memo2;
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void EqualOpeTest_3()
        {
            var memo1 = new Memo("ABC");
            var memo2 = new Memo("ABC");
            var isEqual = memo1 == memo2;
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void EqualOpeTest_4()
        {
            var memo1 = new Memo("ABC\nDE");
            var memo2 = new Memo("ABC\nDE");
            var isEqual = memo1 == memo2;
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void EqualOpeTest_5()
        {
            var memo1 = new Memo(@"ABC
DE");
            var memo2 = new Memo(@"ABC
DE");
            var isEqual = memo1 == memo2;
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void EqualOpeTest_6()
        {
            var memo1 = new Memo("ABC");
            var memo2 = new Memo("ABD");
            var isEqual = memo1 == memo2;
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void EqualOpeTest_7()
        {
            var memo1 = new Memo("ABC\nDE");
            var memo2 = new Memo("ABC\nDF");
            var isEqual = memo1 == memo2;
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void EqualOpeTest_8()
        {
            var memo1 = new Memo(@"ABC
DE");
            var memo2 = new Memo(@"ABC
DF");
            var isEqual = memo1 == memo2;
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void NotEqualOpeTest_1()
        { //自己等価の比較
            var memo1 = new Memo("");
            var isEqual = memo1 != memo1;
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void NotEqualOpeTest_2()
        {
            var memo1 = new Memo("");
            var memo2 = new Memo("");
            var isEqual = memo1 != memo2;
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void NotEqualOpeTest_3()
        {
            var memo1 = new Memo("ABC");
            var memo2 = new Memo("ABC");
            var isEqual = memo1 != memo2;
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void NotEqualOpeTest_4()
        {
            var memo1 = new Memo("ABC\nDE");
            var memo2 = new Memo("ABC\nDE");
            var isEqual = memo1 != memo2;
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void NotEqualOpeTest_5()
        {
            var memo1 = new Memo(@"ABC
DE");
            var memo2 = new Memo(@"ABC
DE");
            var isEqual = memo1 != memo2;
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void NotEqualOpeTest_6()
        {
            var memo1 = new Memo("ABC");
            var memo2 = new Memo("ABD");
            var isEqual = memo1 != memo2;
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void NotEqualOpeTest_7()
        {
            var memo1 = new Memo("ABC\nDE");
            var memo2 = new Memo("ABC\nDF");
            var isEqual = memo1 != memo2;
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void NotEqualOpeTest_8()
        {
            var memo1 = new Memo(@"ABC
DE");
            var memo2 = new Memo(@"ABC
DF");
            var isEqual = memo1 != memo2;
            Assert.IsTrue(isEqual);
        }
    }
}
