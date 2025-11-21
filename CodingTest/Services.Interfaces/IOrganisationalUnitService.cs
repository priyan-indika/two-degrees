using System.Collections.Generic;
using CodingTest.Entities;

namespace CodingTest.Services.Interfaces
{
    public interface IOrganisationalUnitService
    {
        IEnumerable<OrganisationalUnit> GetAllOrganisationalUnits();

        OrganisationalUnit GetOrganisationalUnitByName(string name);
    }
}