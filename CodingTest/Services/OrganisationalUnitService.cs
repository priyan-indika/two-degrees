using System.Collections.Generic;
using System.Linq;
using CodingTest.Entities;
using CodingTest.Services.Interfaces;

namespace CodingTest.Services
{
    public class OrganisationalUnitService(AccountDbContext db) : IOrganisationalUnitService
    {
        public IEnumerable<OrganisationalUnit> GetAllOrganisationalUnits() {
            return db.OrganisationalUnits;
        }

        public OrganisationalUnit GetOrganisationalUnitByName(string name)
        {
            return db.OrganisationalUnits.SingleOrDefault(ou => ou.Name == name);
        }
    }
}