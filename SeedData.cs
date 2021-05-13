using leave_mgmnt.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_mgmnt
{
    public static class SeedData
    {
        public static void seed(UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager)
        {
            seedRoles(roleManager);
            seedUsers(userManager);

        }

        public static void seedUsers(UserManager<Employee> userManager)
        {
            if (userManager.FindByNameAsync("admin@gmail.com").Result == null)
            {
                var user = new Employee
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com"
                };
                var res = userManager.CreateAsync(user, "Asdf123#").Result;

                if (res.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }

        public static void seedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (roleManager.RoleExistsAsync("Administrator").Result)
            {
                // if found dont do anything
            }
            else
            {
                var role = new IdentityRole
                {
                    Name = "Administrator"
                };
                roleManager.CreateAsync(role);
            }

            if (roleManager.RoleExistsAsync("Employee").Result)
            {
                // if found dont do anything
            }
            else
            {
                var role = new IdentityRole
                {
                    Name = "Employee"
                };
                roleManager.CreateAsync(role);
            }
        }

    }
}
