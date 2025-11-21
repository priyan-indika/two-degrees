using System.Collections.Generic;
using System.Linq;
using CodingTest.Entities;
using CodingTest.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace CodingTest.Services
{
    public class AccountService(AccountDbContext db, IValidator<Account> accountValidator) : IAccountService
    {
        public IEnumerable<Account> GetAll() {
            return db.Accounts
                .Include(a => a.OrganisationalUnit)
                .Include(a => a.Contacts)
                .ThenInclude(c => c.Role);
        }
        
        public Account GetAccountById(int accountId)
        {
            return db.Accounts
                .Include(a => a.OrganisationalUnit)
                .Include(a => a.Contacts)
                .ThenInclude(c => c.Role)
                .SingleOrDefault(a => a.AccountId == accountId);
        }

        public void CreateAccount(Account account)
        {
            accountValidator.ValidateAndThrow(account);
    
            db.Accounts.Add(account);
            db.SaveChanges();
        }
    }
}