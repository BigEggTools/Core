namespace BigEgg.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class PreconditionsTest
    {
        [TestClass]
        public class NotNullTest
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void Null()
            {
                Preconditions.NotNull(null);
            }

            [TestMethod]
            public void NotNull()
            {
                Preconditions.NotNull("");
            }

            [TestMethod]
            public void Message()
            {
                try
                {
                    Preconditions.NotNull(null, "param");
                }
                catch (ArgumentNullException ex)
                {
                    Assert.IsTrue(ex.Message.Contains("'param'"));
                    Assert.IsTrue(ex.Message.Contains("'Message'"));
                }
            }
        }

        [TestClass]
        public class NotNullOrWhiteSpaceTest
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void Null()
            {
                Preconditions.NotNullOrWhiteSpace(null);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void Empty()
            {
                Preconditions.NotNullOrWhiteSpace("");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void WhiteSpace()
            {
                Preconditions.NotNullOrWhiteSpace("  ");
            }

            [TestMethod]
            public void ValidValue()
            {
                Preconditions.NotNullOrWhiteSpace("abc");
            }

            [TestMethod]
            public void Message()
            {
                try
                {
                    Preconditions.NotNullOrWhiteSpace(null, "param");
                }
                catch (ArgumentException ex)
                {
                    Assert.IsTrue(ex.Message.Contains("'param'"));
                    Assert.IsTrue(ex.Message.Contains("'Message'"));
                }
            }
        }

        [TestClass]
        public class CheckTest
        {
            [TestMethod]
            public void Match()
            {
                Preconditions.Check(string.IsNullOrWhiteSpace(null));
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void NotMath()
            {
                Preconditions.Check(!string.IsNullOrWhiteSpace(null));
            }

            [TestMethod]
            public void Message()
            {
                try
                {
                    Preconditions.Check(!string.IsNullOrWhiteSpace(null), "Parameter '{0}' in '{1}' should not be empty string.", "param");
                }
                catch (ArgumentException ex)
                {
                    Assert.IsTrue(ex.Message.Contains("'param'"));
                    Assert.IsTrue(ex.Message.Contains("'Message'"));
                }
            }
        }

        [TestClass]
        public class GenericCheckTest
        {
            [TestMethod]
            public void Match()
            {
                Preconditions.Check<NotSupportedException>(string.IsNullOrWhiteSpace(null));
            }

            [TestMethod]
            [ExpectedException(typeof(NotSupportedException))]
            public void NotMath()
            {
                Preconditions.Check<NotSupportedException>(!string.IsNullOrWhiteSpace(null));
            }

            [TestMethod]
            public void Message()
            {
                try
                {
                    Preconditions.Check<NotSupportedException>(!string.IsNullOrWhiteSpace(null), "Parameter '{0}' in '{1}' should not be empty string.", "param");
                }
                catch (NotSupportedException ex)
                {
                    Assert.IsTrue(ex.Message.Contains("'param'"));
                    Assert.IsTrue(ex.Message.Contains("'Message'"));
                }
            }
        }
    }
}
