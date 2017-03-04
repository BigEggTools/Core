namespace BigEgg.UnitTesting.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class AssertHelperTest
    {
        [TestClass]
        public class ExpectedExceptionTest
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void Null()
            {
                AssertHelper.ExpectedException<InvalidOperationException>(null);
            }

            [TestMethod]
            public void HandleException()
            {
                AssertHelper.ExpectedException<ArgumentNullException>(ThrowsArgumentNullException);
            }

            [TestMethod]
            [ExpectedException(typeof(AssertException))]
            public void HandleWrongException()
            {
                AssertHelper.ExpectedException<ArgumentException>(ThrowsArgumentNullException);
            }

            [TestMethod]
            [ExpectedException(typeof(AssertException))]
            public void NoException()
            {
                AssertHelper.ExpectedException<ArgumentNullException>(DoNothing);
            }


            private static void ThrowsArgumentNullException()
            {
                throw new ArgumentNullException();
            }

            private static void DoNothing() { }
        }
    }
}
