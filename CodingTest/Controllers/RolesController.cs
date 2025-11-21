using System.Collections.Generic;
using System.Linq;
using CodingTest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodingTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController(IRoleService roleService) : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> GetAllRoles()
        {
            var roles = roleService.GetAllRoles();
            return roles.Select(x => x.Name);
        }
    }
}
