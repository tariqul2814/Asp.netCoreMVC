using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyTutorial.DAL
{
    public class AccountInitialize : IAccountInitialize
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountInitialize(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedData()
        {
            var adminRole = new IdentityRole("Admin");
            var userRole = new IdentityRole("User");

            if(!_roleManager.Roles.Any())
            {
                var roles = new List<IdentityRole>() { adminRole, userRole };
                foreach(var role in roles)
                {
                    _roleManager.CreateAsync(role).GetAwaiter().GetResult();
                }
            }

            if (_userManager.Users.Any()) return;

            var adminUser = new IdentityUser { 
                UserName = "Tariqul",
                Email = "tariqul@gmail.com"
            };
            var normalUser = new IdentityUser
            {
                UserName = "Dewan",
                Email = "dewan@gmail.com"
            };

            _userManager.CreateAsync(adminUser, "P@ssw0rd123").GetAwaiter().GetResult();
            _userManager.CreateAsync(normalUser, "P@ssw0rd123").GetAwaiter().GetResult();

            _userManager.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(normalUser, adminRole.Name).GetAwaiter().GetResult();

        }
    }

    public interface IAccountInitialize
    {
        Task SeedData();
    }
}
