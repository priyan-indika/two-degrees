using CodingTest.Controllers.Convertors;
using CodingTest.Controllers.ViewModels;
using CodingTest.Entities;
using CodingTest.Enums;
using CodingTest.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController(
        IAccountService accountService,
        AccountConvertor accountConvertor,
        ILogger<AccountsController> logger) : ControllerBase
    {
        [HttpGet]
        public IEnumerable<AccountVm> GetAllAccounts()
        {
            var accounts = accountService.GetAll();
            return accounts.Select(accountConvertor.ConvertToAccountVm);
        }

        [HttpGet]
        [Route("{accountId}")]
        public AccountVm GetAccountById([FromRoute] int accountId)
        {
            var account = accountService.GetAccountById(accountId);
            return accountConvertor.ConvertToAccountVm(account);
        }

        [HttpPost]
        public AccountCreatedVm CreateAccount([FromBody] AccountEditVm accountEditVm)
        {
            var account = accountConvertor.ConvertToAccount(accountEditVm);
            accountService.CreateAccount(account);
            return new AccountCreatedVm { AccountId = account.AccountId };
        }

        [HttpPut("{accountId}/delivery-method")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDeliveryMethod([FromRoute] int accountId, [FromBody] UpdateInvoiceDeliveryRequest request)
        {
            try
            {
                var deliveryMethod = (InvoiceDeliveryMethod)request.DeliveryMethod;
                var account = await accountService.UpdateDeliveryMethod(accountId, (InvoiceDeliveryMethod)request.DeliveryMethod);
                if (account is null)
                {
                    NotFound($"Account {accountId} not found.");
                }
                //return Ok(new { message = $"Invoice delivery method updated to {account.DeliveryMethod}" });
                return Ok($"Account {accountId} is updated with the Inveice Devlivery Method {deliveryMethod}.");
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
