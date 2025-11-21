using System.Collections.Generic;
using System.Linq;
using CodingTest.Entities;
using CodingTest.Services.Interfaces;

namespace CodingTest.Services
{
    public class RoleService(AccountDbContext db) : IRoleService
    {
        public IEnumerable<Role> GetAllRoles() {
            return db.Roles;
        }
        
        public Role GetRoleByName(string name)
        {
            return db.Roles.SingleOrDefault(r => r.Name == name);
        }
    }
}