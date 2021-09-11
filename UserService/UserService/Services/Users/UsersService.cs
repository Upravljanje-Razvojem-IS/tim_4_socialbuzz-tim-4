using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using UserService.Models;
using UserService.Models.DTOs.User;
using UserService.Models.Entities;

namespace UserService.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UsersService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public User CreateUser(User user, string role, string password)
        {
            user.Id = Guid.NewGuid();
            user.AccountIsActive = true;
            IdentityResult result = _userManager.CreateAsync(user, password).Result;
            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, role).Wait();
                return user;
            }
            else
            {
                throw new Exception(result.Errors.ToList()[0].Description);
            }
        }

        public void DeleteUser(Guid id)
        {
            var user = _userManager.FindByIdAsync(id.ToString()).Result;
            if (user == null)
            {
                throw new Exception("Korisnik sa tim id-om ne postoji");
            }
            user.AccountIsActive = false;
            _userManager.UpdateAsync(user);
        }

        public List<UserDTO> GetAdmins()
        {
            var users = _userManager.GetUsersInRoleAsync(AccountRoles.Admin).Result;
            if (users.Count <= 0)
            {
                throw new Exception("Nije pronađen ni jedan korisnik sa ulogom admina");
            }
            else
            {
                return _mapper.Map<List<UserDTO>>(users);
            }
        }

        public List<UserDTO> GetCorporateUsers()
        {
            var users = _userManager.GetUsersInRoleAsync(AccountRoles.Corporate).Result;
            if (users.Count <= 0)
            {
                throw new Exception("Nije pronađen ni jedan korisnik sa ulogom korporativnog korisnika");
            }
            else
            {
                return _mapper.Map<List<UserDTO>>(users);
            }
        }

        public List<UserDTO> GetPersonalUsers()
        {
            var users = _userManager.GetUsersInRoleAsync(AccountRoles.Personal).Result;
            if (users.Count <= 0)
            {
                throw new Exception("Nije pronađen ni jedan korisnik sa ulogom personalnog korisnika");
            }
            else
            {
                return _mapper.Map<List<UserDTO>>(users);
            }
        }

        public UserDTO GetUserById(Guid id)
        {
            var user = _userManager.FindByIdAsync(id.ToString()).Result;
            if (user == null)
            {
                return null;
            }
            return _mapper.Map<UserDTO>(user);
        }

        public List<UserDTO> GetUsers()
        {
            var users = _userManager.Users.ToList();
            if (users.Count <= 0)
            {
                throw new Exception("Nije pronađen ni jedan korisnik");
            }
            else
            {
                return _mapper.Map<List<UserDTO>>(users);
            }
        }

        public void UpdateUser(Guid id, UserUpdateDTO userProfile)
        {
            var user = _userManager.FindByIdAsync(id.ToString()).Result;
            user = _mapper.Map<User>(userProfile);
            _userManager.UpdateAsync(user);
        }
    }
}
