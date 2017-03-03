using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BigEgg.Tests
{
    [TestClass]
    public class ValidatableObjectTest
    {
        [TestMethod]
        public void ValidateTest()
        {
            Person person = new Person();

            var validateResult = person.Validate();
            Assert.AreEqual(1, validateResult.Count());
            Assert.AreEqual(Person.NameMadatoryErrorMessage, validateResult.Single().ErrorMessage);

            person.Name = "BigEgg";
            validateResult = person.Validate();
            Assert.IsFalse(validateResult.Any());

            person.Email = new string('A', 65);
            validateResult = person.Validate();
            Assert.AreEqual(2, validateResult.Count());
            Assert.IsTrue(validateResult.Any(x => x.ErrorMessage == Person.EmailInvalidErrorMessage));
            Assert.IsTrue(validateResult.Any(x => x.ErrorMessage == Person.EmailLengthErrorMessage));

            person.Email = "BigEgg.BigEgg.com";
            validateResult = person.Validate();
            Assert.AreEqual(1, validateResult.Count());
            Assert.AreEqual(Person.EmailInvalidErrorMessage, validateResult.Single().ErrorMessage);

            person.Email = new string('A', 65) + "@BigEgg.com";
            validateResult = person.Validate();
            Assert.AreEqual(1, validateResult.Count());
            Assert.AreEqual(Person.EmailLengthErrorMessage, validateResult.Single().ErrorMessage);

            person.Name = "";
            validateResult = person.Validate();
            Assert.AreEqual(2, validateResult.Count());

            person.Name = "BigEgg";
            person.Email = "BigEgg@BigEgg.com";
            validateResult = person.Validate();
            Assert.IsFalse(validateResult.Any());
        }

        [TestMethod]
        public void ValidateByInterface()
        {
            IValidatableObject person = new Person();

            var validateResult = person.Validate(null);
            Assert.AreEqual(1, validateResult.Count());
            Assert.AreEqual(Person.NameMadatoryErrorMessage, validateResult.Single().ErrorMessage);
        }

        [TestMethod]
        public void SerializationTest()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Person person = new Person() { Name = "BigEgg" };

                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, person);

                stream.Position = 0;
                Person newPerson = (Person)formatter.Deserialize(stream);
                Assert.AreEqual(person.Name, newPerson.Name);
            }
        }

        [TestMethod]
        public void SerializationWithDCSTest()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Person person = new Person() { Name = "BigEgg" };

                var serializer = new DataContractSerializer(typeof(Person));
                serializer.WriteObject(stream, person);

                stream.Position = 0;
                Person newPerson = (Person)serializer.ReadObject(stream);
                Assert.AreEqual(person.Name, newPerson.Name);
            }
        }

        [Serializable]
        private class Person : ValidatableObject
        {
            public const string NameMadatoryErrorMessage = "The Name field is mandatory.";
            public const string EmailInvalidErrorMessage = "The Email field is not a valid e-mail address.";
            public const string EmailLengthErrorMessage = "The field Email must be a string with a maximum length of 64.";

            [Required(ErrorMessage = NameMadatoryErrorMessage)]
            public string Name { get; set; }

            [EmailAddress(ErrorMessage = EmailInvalidErrorMessage)]
            [StringLength(64, ErrorMessage = EmailLengthErrorMessage)]
            public string Email { get; set; }
        }
    }
}
