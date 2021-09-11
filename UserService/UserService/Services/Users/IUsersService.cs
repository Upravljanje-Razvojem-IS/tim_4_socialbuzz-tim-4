using System;
using System.Collections.Generic;
using UserService.Models.DTOs.User;
using UserService.Models.Entities;

namespace UserService.Services.Users
{
    public interface IUsersService
    {
        List<UserDTO> GetUsers();
        List<UserDTO> GetPersonalUsers();
        List<UserDTO> GetCorporateUsers();
        List<UserDTO> GetAdmins();
        UserDTO GetUserById(Guid id);
        User CreateUser(User user, string role, string password);
        void UpdateUser(Guid id, UserUpdateDTO userProfile);
        void DeleteUser(Guid id);
    }
}
