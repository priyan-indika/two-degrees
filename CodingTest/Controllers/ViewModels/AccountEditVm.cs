using System.Collections.Generic;

namespace CodingTest.Controllers.ViewModels
{
    public class AccountEditVm
    {
        public string Name { get; set; }

        public string OrganisationalUnit { get; set; }

        public IEnumerable<ContactEditVm> Contacts { get; set; }
    }
}