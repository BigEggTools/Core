﻿namespace BigEgg.Tests.Utils
{
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class XmlSerializeExtensionsTest
    {
        [TestMethod]
        public void ObjectToXElementTest()
        {
            var person = new Person()
            {
                Name = "BigEgg",
                Age = 20,
                Email = "abc@test.com"
            };

            var root = person.ObjectToXElement();
            Assert.AreEqual("Person", root.Name);
            Assert.AreEqual("BigEgg", root.Attribute("FullName").Value);
            Assert.IsTrue(root.HasElements);
            Assert.AreEqual(20, int.Parse(root.Element("Age").Value));
            Assert.IsFalse(root.Elements().Any(x => x.Name == "Email"));
        }

        [TestMethod]
        public void XElementToObjectTest()
        {
            XElement root = new XElement("Person",
                new XAttribute("FullName", "BigEgg"),
                new XElement("Age", 20),
                new XElement("Email", "abc@test.com")
                );

            var person = root.XElementToObject<Person>();
            Assert.IsNotNull(person);
            Assert.AreEqual("BigEgg", person.Name);
            Assert.AreEqual(20, person.Age);
            Assert.IsTrue(string.IsNullOrWhiteSpace(person.Email));
        }

        [XmlRoot]
        public class Person
        {
            [XmlAttribute("FullName")]
            public string Name { get; set; }

            [XmlElement]
            public int Age { get; set; }

            [XmlIgnore]
            public string Email { get; set; }
        }
    }
}
