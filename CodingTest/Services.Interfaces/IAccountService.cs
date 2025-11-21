using CodingTest.Entities;
using CodingTest.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingTest.Services.Interfaces
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAll();

        Account GetAccountById(int accountId);

        void CreateAccount(Account account);


        Task<Account> UpdateDeliveryMethod(int accountId, InvoiceDeliveryMethod method);
    }
}