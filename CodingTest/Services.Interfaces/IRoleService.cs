using System.Collections.Generic;
using CodingTest.Entities;

namespace CodingTest.Services.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAllRoles();

        Role GetRoleByName(string name);
    }
}