using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CodingTest.Controllers.Convertors;
using CodingTest.Controllers.ViewModels;
using CodingTest.Services.Interfaces;

namespace CodingTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController(
        IAccountService accountService,
        AccountConvertor accountConvertor) : ControllerBase
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
    }
}
