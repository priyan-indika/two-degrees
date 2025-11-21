using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CodingTest.Entities
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AccountDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AccountDbContext>>()))
            {
                context.OrganisationalUnits.AddRange(
                    new OrganisationalUnit
                    {
                        OrganisationalUnitId = 1,
                        Name = "Orcon"
                    },
                    new OrganisationalUnit
                    {
                        OrganisationalUnitId = 2,
                        Name = "Slingshot"
                    },
                    new OrganisationalUnit
                    {
                        OrganisationalUnitId = 3,
                        Name = "Flip"
                    });

                context.Roles.AddRange(
                    new Role
                    {
                        RoleId = 1,
                        Name = "Billing"
                    },
                    new Role
                    {
                        RoleId = 2,
                        Name = "Technical"
                    },
                    new Role
                    {
                        RoleId = 3,
                        Name = "Legal"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}