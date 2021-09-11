using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
            throw new Exception("Uloga mora biti jedinstvena");
        }

        public void DeleteRole(Role role)
        {
            role.Deleted = true;
            var result = _roleManager.UpdateAsync(role).Result;
            if (!result.Succeeded)
            {
                throw new Exception("Greška na serveru");
            }
        }

        public Role GetRoleByRoleId(Guid roleId)
        {
            return _roleManager.FindByIdAsync(roleId.ToString()).Result;
        }

        public List<Role> GetRoles()
        {
            return _roleManager.Roles.ToList();
        }

        public void UpdateRole(Role oldRole, Role newRole)
        {
            if (_roleManager.RoleExistsAsync(newRole.Name).Result)
            {
                throw new Exception("Uloga mora biti jedinstvena");

            }
            oldRole.Name = newRole.Name;
            _ = _roleManager.UpdateAsync(oldRole).Result;
        }
    }
}
