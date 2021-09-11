using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UserService.Models.DTOs.Role;
using UserService.Models.Entities;
using UserService.Services.Roles;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRolesService _roleService;
        private readonly IMapper _mapper;

        public RoleController(IRolesService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Role>> PostApplicationRole(RoleCreateAndUpdateDTO role)
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

    }
}
