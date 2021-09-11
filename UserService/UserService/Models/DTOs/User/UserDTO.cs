﻿using System;

namespace UserService.Models.DTOs.User
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool AccountIsActive { get; set; }
    }
}
