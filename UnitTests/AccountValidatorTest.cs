using NUnit.Framework;
using FluentValidation;
using Moq;
using CodingTest.Entities;
using CodingTest.Validators;

namespace UnitTests
{
    [TestFixture]
    public class AccountValidatorTests
    {
        [Test]
        public void AccountValidator_ThrowsException_WhenOrganisationUnitNull()
        {
            var validator = new AccountValidator(Mock.Of<IValidator<Contact>>());

            var account = new Account
            {
                OrganisationalUnit = null
            };

            Assert.That(() => validator.ValidateAndThrow(account), Throws.Exception);
        }
    }
}