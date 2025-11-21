using System.Collections.Generic;
using System.Linq;
using CodingTest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodingTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganisationalUnitsController(IOrganisationalUnitService organisationalUnitService) : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> GetAllOrganisationalUnits()
        {
            var orgUnits = organisationalUnitService.GetAllOrganisationalUnits();
            return orgUnits.Select(x => x.Name);
        }
    }
}
