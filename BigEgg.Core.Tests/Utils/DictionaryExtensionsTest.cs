namespace BigEgg.Tests.Utils
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class DictionaryExtensionTest
    {
        [TestClass]
        public class AddOrUpdateTest_CommonValue
        {
            [TestMethod]
            public void NotExist()
            {
                IDictionary<string, string> dictionary = new Dictionary<string, string>();
                Assert.AreEqual(0, dictionary.Count);

                dictionary.AddOrUpdate("key", "value");
                Assert.AreEqual(1, dictionary.Count);
                Assert.IsTrue(dictionary.ContainsKey("key"));
                Assert.AreEqual("value", dictionary["key"]);
            }

            [TestMethod]
            public void Exist()
            {
                IDictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.AddOrUpdate("key", "value");
                Assert.AreEqual(1, dictionary.Count);

                dictionary.AddOrUpdate("key", "newValue");
                Assert.AreEqual(1, dictionary.Count);
                Assert.IsTrue(dictionary.ContainsKey("key"));
                Assert.AreEqual("newValue", dictionary["key"]);
            }
        }

        [TestClass]
        public class AddOrUpdateTest_ListValue
        {
            [TestMethod]
            public void NotExist()
            {
                IDictionary<string, IList<string>> dictionary = new Dictionary<string, IList<string>>();
                Assert.AreEqual(0, dictionary.Count);

                dictionary.AddOrUpdate("key", "value");
                Assert.AreEqual(1, dictionary.Count);
                Assert.IsTrue(dictionary.ContainsKey("key"));
                Assert.IsNotNull(dictionary["key"]);
                Assert.AreEqual(1, dictionary["key"].Count);
                Assert.AreEqual("value", dictionary["key"].Single());
            }

            [TestMethod]
            public void Exist()
            {
                IDictionary<string, IList<string>> dictionary = new Dictionary<string, IList<string>>();
                dictionary.AddOrUpdate("key", "value");
                Assert.AreEqual(1, dictionary.Count);

                dictionary.AddOrUpdate("key", "newValue");
                Assert.AreEqual(1, dictionary.Count);
                Assert.IsTrue(dictionary.ContainsKey("key"));
                Assert.IsNotNull(dictionary["key"]);
                Assert.AreEqual(2, dictionary["key"].Count);
                Assert.AreEqual("value", dictionary["key"].First());
                Assert.AreEqual("newValue", dictionary["key"].Last());
            }
        }
    }
}
