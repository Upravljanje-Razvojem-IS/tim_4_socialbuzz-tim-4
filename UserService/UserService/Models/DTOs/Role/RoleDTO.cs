using System;

namespace UserService.Models.DTOs.Role
{
    public class RoleDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
    }
}
