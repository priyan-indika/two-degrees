using CodingTest.Controllers.ViewModels;
using CodingTest.Entities;
using System.Collections.Generic;
using CodingTest.Services.Interfaces;
using System.Linq;

namespace CodingTest.Controllers.Convertors
{
    public class AccountConvertor(IOrganisationalUnitService organisationalUnitService,
        IRoleService roleService)
    {
        public Account ConvertToAccount(AccountEditVm accountEditVm)
        {
            var account = new Account
            {
                Name = accountEditVm?.Name,
                OrganisationalUnit = organisationalUnitService.GetOrganisationalUnitByName(accountEditVm?.OrganisationalUnit),
                Contacts = new List<Contact>(),
            };

            if (accountEditVm?.Contacts == null)
            {
                return account;
            }

            foreach (var c in accountEditVm.Contacts)
            {
                var contact = new Contact
                {
                    Account = account,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PhoneNumber = c.PhoneNumber,
                    MobileNumber = c.MobileNumber,
                    EmailAddress = c.EmailAddress,
                    Role = roleService.GetRoleByName(c.Role),
                };
                account.Contacts.Add(contact);
            }

            return account;
        }

        public AccountVm ConvertToAccountVm(Account account)
        {
            if (account is null)
            {
                return null;
            }
            
            var vm = new AccountVm
            {
                AccountId = account.AccountId,
                Contacts = account.Contacts
                    .Select(c => new ContactVm
                    {
                        ContactId = c.ContactId,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        PhoneNumber = c.PhoneNumber,
                        MobileNumber = c.MobileNumber,
                        EmailAddress = c.EmailAddress,
                        Role = c.Role.Name,
                    }),
                Name = account.Name,
                OrganisationalUnit = account.OrganisationalUnit.Name,
            };

            return vm;
        }
    }
}
