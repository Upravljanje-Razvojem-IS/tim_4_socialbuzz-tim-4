using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PASMicroservice.Entities;
using PASMicroservice.Mocks;
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

        private readonly IUserMockRepository userMockRepository;
        private readonly ILoggerMockRepository<PASTypeController> logger;
        public PASTypeController(IPASTypeRepository typeRepository, LinkGenerator linkGenerator, IMapper mapper,
            IUserMockRepository userMockRepository, ILoggerMockRepository<PASTypeController> logger)
        {
            this.typeRepository = typeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;

            this.userMockRepository = userMockRepository;
            this.logger = logger;
        }

        // GET: api/types
        [HttpGet]
        public ActionResult<List<PASTypeDto>> Get()
        {
            var types = this.typeRepository.GetTypes();

            if (types == null || types.Count() == 0)
            {
                logger.LogInformation("GET Type no content.");
                return NoContent();
            }

            logger.LogInformation("GET Type successful.");
            return Ok(mapper.Map<List<PASTypeDto>>(types));
        }

        // GET api/types/5
        [HttpGet("{id}")]
        public ActionResult<PASTypeDto> GetById(int id)
        {
            var type = this.typeRepository.GetTypeById(id);
            logger.LogInformation("GET Type successful.");
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

                logger.LogInformation("POST Type successful.");
                return Created(location, mapper.Map<PASTypeConfirmationDto>(confirmation));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error creating new type: ", e.Message);
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
                {
                    logger.LogInformation("PUT Type not found.");
                    return NotFound();
                }
                PASType typeEntity = mapper.Map<PASType>(type);
                PASTypeConfirmation confirmation = this.typeRepository.UpdateType(typeEntity);

                logger.LogInformation("PUT Type successful.");
                return Ok(mapper.Map<PASTypeConfirmationDto>(confirmation));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error updating existing type: ", e.Message);
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
                {
                    logger.LogInformation("DELETE Type not found.");
                    return NotFound();
                }
                this.typeRepository.DeleteType(id);

                logger.LogInformation("DELETE Type successful.");
                return NoContent();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error deleting type: ", e.Message);
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
