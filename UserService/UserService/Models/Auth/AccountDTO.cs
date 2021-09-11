using System;

namespace UserService.Models.Auth
{
    public class AccountDTO
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
    }
}
