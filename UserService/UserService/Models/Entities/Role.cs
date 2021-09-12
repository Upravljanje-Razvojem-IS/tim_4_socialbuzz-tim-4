using Microsoft.AspNetCore.Identity;
using System;

namespace UserService.Models.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public bool Deleted { get; set; }
    }
}
