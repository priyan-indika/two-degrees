using System.Collections.Generic;
using CodingTest.Entities;

namespace CodingTest.Services.Interfaces
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAll();

        Account GetAccountById(int accountId);

        void CreateAccount(Account account);
    }
}