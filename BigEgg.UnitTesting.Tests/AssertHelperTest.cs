namespace BigEgg.UnitTesting.Tests
{
    using System;
    using System.Threading.Tasks;
    using System.Threading;
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

        [TestClass]
        public class ExpectedExceptionAsyncTest
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public async Task Null()
            {
                await AssertHelper.ExpectedExceptionAsync<InvalidOperationException>(null);
            }

            [TestMethod]
            public async Task HandleException()
            {
                await AssertHelper.ExpectedExceptionAsync<ArgumentNullException>(ThrowsArgumentNullExceptionAsync);
            }

            [TestMethod]
            [ExpectedException(typeof(AssertException))]
            public async Task HandleWrongException()
            {
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(ThrowsArgumentNullExceptionAsync);
            }

            [TestMethod]
            [ExpectedException(typeof(AssertException))]
            public async Task NoException()
            {
                await AssertHelper.ExpectedExceptionAsync<ArgumentNullException>(DoNothingAsync);
            }


            private static async Task ThrowsArgumentNullExceptionAsync()
            {
                await Task.Run(() =>
                {
                    Thread.Sleep(1000);
                    throw new ArgumentNullException();
                });
            }

            private static async Task DoNothingAsync()
            {
                await Task.Run(() =>
                {
                    Thread.Sleep(1000);
                });
            }
        }
    }
}
