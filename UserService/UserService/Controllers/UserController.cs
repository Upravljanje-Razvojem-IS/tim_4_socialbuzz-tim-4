using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserService.Models.Entities;
using UserService.Models.Auth;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using UserService.Services.Users;
using UserService.Models.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using UserService.Models;

namespace UserService.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUsersService _userService;

        /// <summary>
        /// Kontroler sa endpoint-ima za podatke o korisniku
        /// </summary>
        public UserController(UserManager<User> userManager, IMapper mapper, IUsersService userService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userService = userService;
        }

        /// <summary>
        /// Provera kredencijala
        /// </summary>
        /// <returns>Uspešnost autentifikacije</returns>
        /// <response code="200">Uspešna provera</response>
        ///<response code="400">Neuspešna provera</response>
        /// <response code="500">Greška na serveru</response>
        [HttpPost]
        [Route("check")]
        [AllowAnonymous]
        public ActionResult<CheckPrincipalResponseDTO> CheckForAccount([FromBody] PrincipalDTO request)
        {
            try
            {
                CheckPrincipalResponseDTO response;
                User account = _userManager.FindByEmailAsync(request.Email).Result;
                if (account == null)
                {
                    response = new CheckPrincipalResponseDTO();
                    response.Success = false;
                    response.Message = "Pogrešna lozinka ili email";
                    response.Account = null;
                    return BadRequest(response);
                }
                var passwordValid = _userManager.CheckPasswordAsync(account, request.Password).Result;
                if (!passwordValid)
                {
                    response = new CheckPrincipalResponseDTO();
                    response.Success = false;
                    response.Message = "Pogrešna lozinka ili email";
                    response.Account = null;
                    return BadRequest(response);

                }
                if (!account.AccountIsActive)
                {
                    response = new CheckPrincipalResponseDTO();
                    response.Success = false;
                    response.Message = "Deaktiviran profil";
                    response.Account = null;
                    return BadRequest(response);

                }
                List<string> roles = _userManager.GetRolesAsync(account).Result.ToList();
                AccountDTO accountDTO = new AccountDTO();
                accountDTO.Id = account.Id;
                accountDTO.Role = roles[0];
                response = new CheckPrincipalResponseDTO();
                response.Success = true;
                response.Message = "Uspešna provera";
                response.Account = accountDTO;
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Pregled svih korisnika
        /// </summary>
        /// <returns>Lista uloga</returns>
        /// <response code="200">Lista korisnika</response>
        ///<response code="401">Neautorizovani pristup</response>
        /// <response code="500">Greška na serveru</response>
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            try
            {
                var users = _userService.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Pregled korisnika po ulozi
        /// </summary>
        /// <param name="id">Id uloge</param>
        /// <returns>Lista korisnika sa prosleđenim id-om uloge</returns>
        ///<response code="200">Lista korisnika</response>
        /// <response code="404">Lista korisnika nije pronađena</response>
        /// <response code="401">Neautorizovani pristup</response>
        /// <response code="500">Greška na serveru</response>
        [Route("{userRole}")]
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetUsersByType(string userRole)
        {
            try
            {
                if (string.IsNullOrEmpty(userRole))
                {
                    var users = _userService.GetUsers();
                    return Ok(users);
                }
                else
                {
                    switch (userRole)
                    {
                        case "corporate":
                            return _userService.GetCorporateUsers();
                        case "personal":
                            return _userService.GetPersonalUsers();
                        case "admin":
                            return _userService.GetAdmins();
                        default:
                            return StatusCode(StatusCodes.Status500InternalServerError, "Prosleđena pogrešna uloga (corporate, personal or admin)");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Pretraga korisnika po id-u
        /// </summary>
        /// <param name="id">Id korisnika</param>
        /// <returns>Korisnik sa prosleđenim id-om</returns>
        ///<response code="200">Korisnik</response>
        /// <response code="404">Korisnik nije pronađen</response>
        /// <response code="401">Neautorizovani pristup</response>
        /// <response code="500">Greška na serveru</response>
        [HttpGet("{id:Guid}")]
        public ActionResult<UserDTO> GetUserById(Guid id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound("Korisnik sa prosleđenim id-om nije pronađen");
                }
                else
                {
                    return Ok(user);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Kreiranje novog korisnika (korporativnog ili personalnog)
        /// </summary>
        /// <param name="role">Korisnik</param>
        /// <returns>Potvrda da je korisnik kreirana</returns>
        /// <response code="200">Kreiran korisnik</response>
        /// <response code="401">Neautorizovani pristup</response>
        /// <response code="500">Greška na serveru</response>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<User> CreateUser(UserCreateDTO user)
        {
            try
            {
                var password = user.Password;
                var role = user.Role;
                var userEntity = _mapper.Map<User>(user);
                if (!role.Equals(AccountRoles.Personal) 
                    && !role.Equals(AccountRoles.Corporate))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Pogrešna uloga korisnika (corporate, personal)");
                }
                return _userService.CreateUser(userEntity, role, password);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Kreiranje novog korisnika (admin)
        /// </summary>
        /// <param name="role">Korisnik</param>
        /// <returns>Potvrda da je korisnik kreirana</returns>
        /// <response code="200">Kreiran korisnik</response>
        /// <response code="401">Neautorizovani pristup</response>
        /// <response code="500">Greška na serveru</response>
        [HttpPost("admin")]
        [Authorize(Roles = "admin")]
        public ActionResult<User> CreateAdmin(UserCreateDTO user)
        {
            try
            {
                var password = user.Password;
                var role = user.Role;
                var userEntity = _mapper.Map<User>(user);
                if (!role.Equals(AccountRoles.Admin))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Pogrešna uloga korisnika (admin)");
                }
                return _userService.CreateUser(userEntity, role, password);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Brisanje korisnika iz sistema
        /// </summary>
        /// <param name="id">Id korisnika</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Korisnik uspešno obrisan</response>
        /// <response code="401">Neautorizovani pristup</response>
        /// <response code="404">Korisnik nije pronađen</response>
        /// <response code="500">Greška na serveru</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser(Guid id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound("Korisnik sa prosleđenim id-om ne postoji");
                }
                else
                {
                    _userService.DeleteUser(id);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
