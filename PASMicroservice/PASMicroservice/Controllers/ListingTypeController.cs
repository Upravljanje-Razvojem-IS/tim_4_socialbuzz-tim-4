﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PASMicroservice.Entities;
using PASMicroservice.Mocks;
using PASMicroservice.Models.ListingType;
using PASMicroservice.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PASMicroservice.Controllers
{
    [Route("api/listingTypes")]
    [ApiController]
    public class ListingTypeController : ControllerBase
    {
        private readonly IListingTypeRepository listingTypeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        private readonly IUserMockRepository userMockRepository;
        private readonly ILoggerMockRepository<ListingTypeController> logger;
        public ListingTypeController(IListingTypeRepository listingTypeRepository, LinkGenerator linkGenerator, IMapper mapper,
            IUserMockRepository userMockRepository, ILoggerMockRepository<ListingTypeController> logger)
        {
            this.listingTypeRepository = listingTypeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;

            this.userMockRepository = userMockRepository;
            this.logger = logger;
        }

        // GET: api/listingTypes
        [HttpGet]
        public ActionResult<List<ListingTypeDto>> Get()
        {
            var listingTypes = this.listingTypeRepository.GetTypes();

            if (listingTypes == null || listingTypes.Count() == 0)
            {
                logger.LogInformation("GET Type no content.");
                return NoContent();
            }

            logger.LogInformation("GET Type successful.");
            return Ok(mapper.Map<List<ListingTypeDto>>(listingTypes));
        }

        // GET api/listingTypes/5
        [HttpGet("{id}")]
        public ActionResult<ListingTypeDto> GetById(int id)
        {
            var listingType = this.listingTypeRepository.GetTypeById(id);
            logger.LogInformation("GET Type successful.");
            return Ok(mapper.Map<ListingTypeDto>(listingType));
        }

        // POST api/listingTypes
        [HttpPost]
        public ActionResult<ListingTypeConfirmationDto> Post([FromBody] ListingTypeCreationDto listingType)
        {
            try
            {
                var listingTypeEntity = mapper.Map<ListingType>(listingType);
                var confirmation = this.listingTypeRepository.CreateType(listingTypeEntity);

                string location = linkGenerator.GetPathByAction("GetById", "ListingType", new { id = confirmation.ListingTypeId });

                logger.LogInformation("POST ListingType successful.");
                return Created(location, mapper.Map<ListingTypeConfirmationDto>(confirmation));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error creating new listingType: ", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        // PUT api/listingTypes
        [HttpPut()]
        public ActionResult<ListingTypeConfirmationDto> Put([FromBody] ListingTypeUpdateDto listingType)
        {
            try
            {
                if (this.listingTypeRepository.GetTypeById(listingType.ListingTypeId) == null)
                {
                    logger.LogInformation("PUT ListingType not found.");
                    return NotFound();
                }
                ListingType listingTypeEntity = mapper.Map<ListingType>(listingType);
                ListingTypeConfirmation confirmation = this.listingTypeRepository.UpdateType(listingTypeEntity);

                logger.LogInformation("PUT ListingType successful.");
                return Ok(mapper.Map<ListingTypeConfirmationDto>(confirmation));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error updating existing listing type: ", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        // DELETE api/listingTypes/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var listingType = this.listingTypeRepository.GetTypeById(id);

                if (listingType == null)
                {
                    logger.LogInformation("DELETE Type not found.");
                    return NotFound();
                }
                this.listingTypeRepository.DeleteType(id);

                logger.LogInformation("DELETE Type successful.");
                return NoContent();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error deleting listingType: ", e.Message);
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
