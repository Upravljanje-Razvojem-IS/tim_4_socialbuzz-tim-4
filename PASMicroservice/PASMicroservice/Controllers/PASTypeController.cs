using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PASMicroservice.Entities;
using PASMicroservice.Models.PASType;
using PASMicroservice.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PASMicroservice.Controllers
{
    [Route("api/types")]
    [ApiController]
    public class PASTypeController : ControllerBase
    {
        private readonly IPASTypeRepository typeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        public PASTypeController(IPASTypeRepository typeRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.typeRepository = typeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        // GET: api/types
        [HttpGet]
        public ActionResult<List<PASTypeDto>> Get()
        {
            var types = this.typeRepository.GetTypes();

            if (types == null || types.Count() == 0)
                return NoContent();

            return Ok(mapper.Map<List<PASTypeDto>>(types));
        }

        // GET api/types/5
        [HttpGet("{id}")]
        public ActionResult<PASTypeDto> GetById(int id)
        {
            var type = this.typeRepository.GetTypeById(id);
            return Ok(mapper.Map<PASTypeDto>(type));
        }

        // POST api/types
        [HttpPost]
        public ActionResult<PASTypeConfirmationDto> Post([FromBody] PASTypeCreationDto type)
        {
            try
            {
                var typeEntity = mapper.Map<PASType>(type);
                var confirmation = this.typeRepository.CreateType(typeEntity);

                string location = linkGenerator.GetPathByAction("GetById", "PASType", new { Id = confirmation.Id });

                return Created(location, mapper.Map<PASTypeConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        // PUT api/types
        [HttpPut()]
        public ActionResult<PASTypeConfirmationDto> Put([FromBody] PASTypeUpdateDto type)
        {
            try
            {
                if (this.typeRepository.GetTypeById(type.Id) == null)
                    return NotFound();

                PASType typeEntity = mapper.Map<PASType>(type);
                PASTypeConfirmation confirmation = this.typeRepository.UpdateType(typeEntity);
                return Ok(mapper.Map<PASTypeConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        // DELETE api/types/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var type = this.typeRepository.GetTypeById(id);

                if (type == null)
                    return NotFound();

                this.typeRepository.DeleteType(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        // OPTIONS
        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
