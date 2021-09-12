using System;
using System.Collections.Generic;
using UserService.Models.Entities;

namespace UserService.Services.Roles
{
    public interface IRolesService
    {
        List<Role> GetRoles();
        Role GetRoleByRoleId(Guid roleId);
        Role CreateRole(Role role);
        void UpdateRole(Role oldRole, Role newRole);
        void DeleteRole(Role role);
    }
}
