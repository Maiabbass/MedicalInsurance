using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;
using Microsoft.AspNetCore.Identity;


namespace api.Services
{
    public class UserRoleService :IUserRoleService
    {
     private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRoleService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task AddUserToRoleAsync(string username, string roleName)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                user = new User { UserName = username, Email = $"{username}@example.com" };
                await _userManager.CreateAsync(user, "Password123!"); // يجب تغيير كلمة المرور الافتراضية
            }

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            if (!await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
        }
    }
}