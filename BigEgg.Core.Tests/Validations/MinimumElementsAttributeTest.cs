namespace BigEgg.Tests.Validations
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.UnitTesting;

    using BigEgg.Validations;

    public class MinimumElementsAttributeTest
    {
        [TestClass]
        public class GeneralTest
        {
            [TestMethod]
            public void ConstructorTest()
            {
                AssertHelper.ExpectedException<ArgumentException>(() => new MinimumElementsAttribute(-1));
            }
        }

        [TestClass]
        public class AllowNullTest
        {
            [TestMethod]
            public void AllowNull()
            {
                var data = new AllowNullData() { Data = null };
                Assert.IsFalse(data.Validate().Any());

                data.Data = new List<string>();
                Assert.IsFalse(data.Validate().Any());
            }

            [TestMethod]
            public void NotAllowNull()
            {
                var data = new NotAllowNullData() { Data = null };
                Assert.IsTrue(data.Validate().Any());

                data.Data = new List<string>();
                Assert.IsFalse(data.Validate().Any());
            }


            public class NotAllowNullData : ValidatableObject
            {
                [MinimumElements(0, AllowNull = false)]
                public object Data { get; set; }
            }

            public class AllowNullData : ValidatableObject
            {
                [MinimumElements(0, AllowNull = true)]
                public object Data { get; set; }
            }
        }

        [TestClass]
        public class ValidationTest
        {
            [TestMethod]
            public void List()
            {
                var data = new DataInfo() { Data = new List<string>() };
                Assert.IsTrue(data.Validate().Any());

                data = new DataInfo() { Data = new List<string>() { "test" } };
                Assert.IsFalse(data.Validate().Any());
            }

            [TestMethod]
            public void Array()
            {
                var data = new DataInfo() { Data = new string[0] };
                Assert.IsTrue(data.Validate().Any());

                data = new DataInfo() { Data = new string[1] };
                Assert.IsFalse(data.Validate().Any());
            }

            [TestMethod]
            public void Dictionary()
            {
                var data = new DataInfo() { Data = new Dictionary<string, string>() };
                Assert.IsTrue(data.Validate().Any());

                data = new DataInfo() { Data = new Dictionary<string, string>() { { "key", "value" } } };
                Assert.IsFalse(data.Validate().Any());
            }
        }

        public class DataInfo : ValidatableObject
        {
            [MinimumElements(1)]
            public IEnumerable Data { get; set; }
        }
    }
}
