using Microsoft.AspNetCore.Identity;
using System;
using UserService.Models.Entities;

namespace UserService.Services.Roles
{
    public class RolesService : IRolesService
    {
        private readonly RoleManager<Role> _roleManager;
        public RolesService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }
        public Role CreateRole(Role role)
        {
            if (!_roleManager.RoleExistsAsync(role.Name).Result)
            {
                _roleManager.CreateAsync(role).Wait();
                return role;

            }
            throw new Exception("Rola mora biti jedinstvena");
        }
    }
}
