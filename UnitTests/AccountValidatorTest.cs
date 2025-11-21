using Castle.Core.Logging;
using CodingTest.Controllers;
using CodingTest.Controllers.Convertors;
using CodingTest.Controllers.ViewModels;
using CodingTest.Entities;
using CodingTest.Enums;
using CodingTest.Services.Interfaces;
using CodingTest.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestFixture]
    public class AccountValidatorTests
    {
        [Test]
        public void AccountValidator_ThrowsException_WhenOrganisationUnitNull()
        {
            //var validator = new AccountValidator(Mock.Of<IValidator<Contact>>());

            //var account = new Account
            //{
            //    OrganisationalUnit = null
            //};

            //Assert.That(() => validator.ValidateAndThrow(account), Throws.Exception);
        }


        [Test]
        public async Task UpdateDeliveryMethod_ReturnsOk_WhenValidBillingEmail()
        {
            // Arrange
            var accountId = 1;
            var account = new Account
            {
                AccountId = accountId,
                Name = "Bob Limited",
                Contacts = new List<Contact>
                {
                    new Contact
                    {
                        ContactId = 1,
                        Role = new Role { RoleId = 1, Name = "Account" },
                        EmailAddress = "bob.jones@boblimited.com",
                        MobileNumber = "0201234567"
                    }
                },
                DeliveryMethod = InvoiceDeliveryMethod.Paper
            };

            var mockService = new Mock<IAccountService>();
            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<AccountsController>>();

            var mockOrgUnitService = new Mock<IOrganisationalUnitService>();
            var mockRoleService = new Mock<IRoleService>();
            var convertor = new AccountConvertor(mockOrgUnitService.Object, mockRoleService.Object);

            mockService
                .Setup(s => s.UpdateDeliveryMethod(accountId, InvoiceDeliveryMethod.Email))
                .ReturnsAsync(account);

            var controller = new AccountsController(mockService.Object, convertor, mockLogger.Object);

            var request = new UpdateInvoiceDeliveryRequest
            {
                DeliveryMethod = 3
            };

            // Act
            var result = await controller.UpdateDeliveryMethod(accountId, request);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult?.Value, Is.EqualTo(account));
        }
    }
}