using BigEgg.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigEgg.Tests.Validations
{
    public class RequiredIfAttributeTest
    {
        [TestClass]
        public class GeneralValidationTest
        {
            [TestMethod]
            public void ValidationTest_NotMatch()
            {
                Person person = new Person();

                var validateResult = person.Validate();
                Assert.IsFalse(validateResult.Any());
            }

            [TestMethod]
            public void ValidationTest_Match()
            {
                Person person = new Person()
                {
                    IsCouple = true
                };

                var validateResult = person.Validate();
                Assert.AreEqual(1, validateResult.Count());
                Assert.AreEqual(Person.CoupleNameErrorMessage, validateResult.Single().ErrorMessage);

                person.CoupleName = "";
                validateResult = person.Validate();
                Assert.AreEqual(1, validateResult.Count());
                Assert.AreEqual(Person.CoupleNameErrorMessage, validateResult.Single().ErrorMessage);

                person.CoupleName = "   ";
                validateResult = person.Validate();
                Assert.AreEqual(1, validateResult.Count());
                Assert.AreEqual(Person.CoupleNameErrorMessage, validateResult.Single().ErrorMessage);

                person.CoupleName = "BigEgg";
                validateResult = person.Validate();
                Assert.IsFalse(validateResult.Any());
                Assert.AreEqual(Person.CoupleNameErrorMessage, validateResult.Single().ErrorMessage);
            }

            [TestMethod]
            public void ValidationTest_NotMatch_OtherType()
            {
                Person2 person = new Person2()
                {
                    IsCouple = "No"
                };

                var validateResult = person.Validate();
                Assert.IsFalse(validateResult.Any());
            }

            [TestMethod]
            public void ValidationTest_Match_OtherType()
            {
                Person2 person = new Person2()
                {
                    IsCouple = "Yes"
                };

                var validateResult = person.Validate();
                Assert.AreEqual(1, validateResult.Count());
                Assert.AreEqual(Person.CoupleNameErrorMessage, validateResult.Single().ErrorMessage);
            }

            [TestMethod]
            [ExpectedException(typeof(ValidationException))]
            public void ValidationTest_DependentPropertyNotExist()
            {
                WrongPerson person = new WrongPerson()
                {
                    IsCouple = true
                };

                person.Validate();
            }

            [TestMethod]
            [ExpectedException(typeof(ValidationException))]
            public void ValidationTest_DependentPropertyTypeNotMatch()
            {
                WrongPerson2 person = new WrongPerson2()
                {
                    IsCouple = true
                };

                person.Validate();
            }
        }

        [TestClass]
        public class IsInvertedTest
        {
            [TestMethod]
            public void ValidationTest_NotMatch()
            {
                PersonWithInverted person = new PersonWithInverted()
                {
                    IsNotCouple = true
                };

                var validateResult = person.Validate();
                Assert.IsFalse(validateResult.Any());
            }

            [TestMethod]
            public void ValidationTest_Match()
            {
                PersonWithInverted person = new PersonWithInverted();

                var validateResult = person.Validate();
                Assert.AreEqual(1, validateResult.Count());
                Assert.AreEqual(Person.CoupleNameErrorMessage, validateResult.Single().ErrorMessage);
            }
        }

        [TestClass]
        public class AllowEmptyStringTest
        {
            [TestMethod]
            public void ValidationTest_NotMatch()
            {
                PersonWithAllowEmpty person = new PersonWithAllowEmpty();

                var validateResult = person.Validate();
                Assert.IsFalse(validateResult.Any());
            }

            [TestMethod]
            public void ValidationTest_Match()
            {
                PersonWithAllowEmpty person = new PersonWithAllowEmpty()
                {
                    IsCouple = true
                };

                var validateResult = person.Validate();
                Assert.AreEqual(1, validateResult.Count());
                Assert.IsFalse(string.IsNullOrWhiteSpace(validateResult.Single().ErrorMessage));

                person.CoupleName = "";
                validateResult = person.Validate();
                Assert.IsFalse(validateResult.Any());

                person.CoupleName = "   ";
                validateResult = person.Validate();
                Assert.IsFalse(validateResult.Any());
            }
        }


        private class Person : ValidatableObject
        {
            public const string CoupleNameErrorMessage = "Couple's name is mandatory.";

            public bool IsCouple { get; set; }

            [RequiredIf("IsCouple", true, ErrorMessage = CoupleNameErrorMessage)]
            public string CoupleName { get; set; }
        }

        private class Person2 : ValidatableObject
        {
            public const string CoupleNameErrorMessage = "Couple's name is mandatory.";

            public string IsCouple { get; set; }

            [RequiredIf("IsCouple", "Yes", ErrorMessage = CoupleNameErrorMessage)]
            public string CoupleName { get; set; }
        }

        private class WrongPerson : ValidatableObject
        {
            public const string CoupleNameErrorMessage = "Couple's name is mandatory.";

            public bool IsCouple { get; set; }

            [RequiredIf("WrongPropertyName", true, ErrorMessage = CoupleNameErrorMessage)]
            public string CoupleName { get; set; }
        }

        private class WrongPerson2 : ValidatableObject
        {
            public const string CoupleNameErrorMessage = "Couple's name is mandatory.";

            public bool IsCouple { get; set; }

            [RequiredIf("IsCouple", "Yes", ErrorMessage = CoupleNameErrorMessage)]
            public string CoupleName { get; set; }
        }

        private class PersonWithInverted : ValidatableObject
        {
            public const string CoupleNameErrorMessage = "Couple's name is mandatory.";

            public bool IsNotCouple { get; set; }

            [RequiredIf("IsNotCouple", true, ErrorMessage = CoupleNameErrorMessage, IsInverted = true)]
            public string CoupleName { get; set; }
        }

        private class PersonWithAllowEmpty : ValidatableObject
        {
            public bool IsCouple { get; set; }

            [RequiredIf("IsCouple", true, AllowEmptyStrings = true)]
            public string CoupleName { get; set; }
        }
    }
}
