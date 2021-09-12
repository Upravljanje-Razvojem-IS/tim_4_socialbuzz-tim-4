using Microsoft.AspNetCore.Identity;
using System;

namespace UserService.Models.Entities
{
    public class User : IdentityUser<Guid>
    {
        public bool AccountIsActive { get; set; } = true;
    }
}
