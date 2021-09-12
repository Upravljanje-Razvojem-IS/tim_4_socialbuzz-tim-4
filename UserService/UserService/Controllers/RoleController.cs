using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Models.DTOs.Role;
using UserService.Models.Entities;
using UserService.Services.Roles;

namespace UserService.Controllers
{
    /// <summary>
    /// Kontroler sa endpoint-ima za podatke o ulogama
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRolesService _roleService;
        private readonly IMapper _mapper;

        public RoleController(IRolesService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        /// <summary>
        /// Pregled svih uloga
        /// </summary>
        /// <returns>Lista uloga</returns>
        /// <response code="200">Lista uloga</response>
        ///<response code="401">Neautorizovani pristup</response>
        /// <response code="500">Greška na serveru</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetRoles()
        {
            try
            {
                var roles = _roleService.GetRoles();
                return Ok(_mapper.Map<List<RoleDTO>>(roles));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        /// <summary>
        /// Pretraga uloge po id-u
        /// </summary>
        /// <param name="id">Id uloge</param>
        /// <returns>Uloga sa prosleđenim id-om</returns>
        ///<response code="200">Uloga</response>
        /// <response code="404">Uloga nije pronađena</response>
        /// <response code="401">Neautorizovani pristup</response>
        /// <response code="500">Greška na serveru</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDTO>> GetRole(Guid id)
        {
            try
            {
                var role = _roleService.GetRoleByRoleId(id);
                if (role == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<RoleDTO>(role));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        /// <summary>
        /// Kreiranje nove uloge
        /// </summary>
        /// <param name="role">Uloga</param>
        /// <returns>Potvrda da je uloga kreirana</returns>
        /// <response code="200">Kreirana uloga</response>
        /// <response code="401">Neautorizovani pristup</response>
        /// <response code="409">Uloga nije jedinstvena</response>
        /// <response code="500">Greška na serveru</response>
        [HttpPost]
        public async Task<ActionResult<Role>> CreateRole(RoleCreateAndUpdateDTO role)
        {
            try
            {
                var newRole = _mapper.Map<Role>(role);
                var createdRole = _roleService.CreateRole(newRole);
                return Ok(_mapper.Map<RoleDTO>(createdRole));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Izmena postojeće uloge
        /// </summary>
        /// <param name="id">Id uloge</param>
        /// <param name="role">Uloga</param>
        /// <returns>Potvrda da je uloga izmenjena</returns>
        /// <response code="200"></response>
        /// <response code="400">Uloga nije pronađena</response>
        /// <response code="401">Neautorizovani pristup</response>
        /// <response code="409">Uloga nije jedinstvena</response>
        /// <response code="500">Greška na serveru</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(Guid id, RoleCreateAndUpdateDTO role)
        {
            //TODO: convert dto to entity and change update in service to have 2 args
            try
            {
                Role roleWithId = _roleService.GetRoleByRoleId(id);
                if (roleWithId == null)
                {
                    return NotFound();
                }
                var applicationRoleUpdate = _mapper.Map<Role>(role);
                _roleService.UpdateRole(roleWithId, applicationRoleUpdate);
                return Ok(_mapper.Map<Role>(roleWithId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        /// <summary>
        /// Brisanje uloge iz sistema
        /// </summary>
        /// <param name="id">Id uloge</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Uloga uspešno obrisana</response>
        /// <response code="401">Neautorizovani pristup</response>
        /// <response code="404">Uloga nije pronađena</response>
        /// <response code="500">Greška na serveru</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationRole(Guid id)
        {
            try
            {
                var role = _roleService.GetRoleByRoleId(id);
                if (role == null)
                {
                    return NotFound();
                }
                _roleService.DeleteRole(role);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
