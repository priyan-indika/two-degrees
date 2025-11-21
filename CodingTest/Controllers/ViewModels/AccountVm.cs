using System.Collections.Generic;

namespace CodingTest.Controllers.ViewModels
{
    public class AccountVm
    {
        public int AccountId { get; set; }

        public string Name { get; set; }

        public IEnumerable<ContactVm> Contacts { get; set; }

        public string OrganisationalUnit { get; set; }

        public int DeliveryMethod { get; set; }
    }
}