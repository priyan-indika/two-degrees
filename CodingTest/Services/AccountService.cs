using CodingTest.Entities;
using CodingTest.Enums;
using CodingTest.Services.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<Account> UpdateAccount(Account account)
        {
            await db.SaveChangesAsync();
            return account;
        }

        public async Task<Account> UpdateDeliveryMethod(int accountId, InvoiceDeliveryMethod method)
        {
            var account = await db.Accounts
                .Include(a => a.OrganisationalUnit)
                .Include(a => a.Contacts)
                .ThenInclude(c => c.Role)
                .SingleOrDefaultAsync(a => a.AccountId == accountId);

            if (account is null)
                return null;

            var billingContact = account
                .Contacts.Where(x => x.IsBillingContact())
                .FirstOrDefault();

            bool isValid = method switch
            {
                InvoiceDeliveryMethod.Email => !string.IsNullOrWhiteSpace(billingContact?.EmailAddress),
                InvoiceDeliveryMethod.Text => !string.IsNullOrWhiteSpace(billingContact?.MobileNumber),
                InvoiceDeliveryMethod.Paper => true,
                _ => false
            };

            if (!isValid)
                throw new InvalidOperationException($"Invalid contact details for {method} delivery.");

            account.DeliveryMethod = method;
            db.Accounts.Update(account);
            await db.SaveChangesAsync();
            
            return account;
        }
    }
}