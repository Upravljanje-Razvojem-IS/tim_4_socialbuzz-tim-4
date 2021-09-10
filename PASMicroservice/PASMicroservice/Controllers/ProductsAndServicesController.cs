using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PASMicroservice.Entities;
using PASMicroservice.Mocks;
using PASMicroservice.Models.ProductsAndServices;
using PASMicroservice.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PASMicroservice.Controllers
{
    [Route("api/pas")]
    [ApiController]
    public class ProductsAndServicesController : ControllerBase
    {
        private readonly IProductsAndServicesRepository pasRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        private readonly IUserMockRepository userMockRepository;
        private readonly ILoggerMockRepository<ProductsAndServicesController> logger;

        public ProductsAndServicesController(IProductsAndServicesRepository pasRepository, LinkGenerator linkGenerator, IMapper mapper, 
            IUserMockRepository userMockRepository, ILoggerMockRepository<ProductsAndServicesController> logger)
        {
            this.pasRepository = pasRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;

            this.userMockRepository = userMockRepository;
            this.logger = logger;
        }

        // GET: api/pas
        [HttpGet]
        public ActionResult<List<ProductsAndServicesDto>> Get()
        {
            var pas = this.pasRepository.GetPAS();

            if (pas == null || pas.Count() == 0)
            {
                logger.LogInformation("GET ProductsAndServices no content.");
                return NoContent();
            }

            logger.LogInformation("GET ProductsAndServices successful.");
            return Ok(mapper.Map<List<ProductsAndServicesDto>>(pas));
        }

        // GET api/pas/5
        [HttpGet("{id}")]
        public ActionResult<ProductsAndServicesDto> GetById(Guid id)
        {
            var pas = this.pasRepository.GetPASById(id);

            logger.LogInformation("GET ProductsAndServices successful.");
            return Ok(mapper.Map<ProductsAndServicesDto>(pas));
        }

        // POST api/pas
        [HttpPost]
        public ActionResult<ProductsAndServicesConfirmationDto> Post([FromBody] ProductsAndServicesCreationDto pas)
        {
            try
            {
                if (this.userMockRepository.GetUserById(pas.UserId) == null)
                {
                    logger.LogInformation("POST ProductsAndServices failed.");
                    return StatusCode(StatusCodes.Status400BadRequest, "User doesn't exist.");
                }
                var pasEntity = mapper.Map<ProductsAndServices>(pas);
                var confirmation = this.pasRepository.CreatePAS(pasEntity);

                string location = linkGenerator.GetPathByAction("GetById", "ProductsAndServices", new { Id = confirmation.Id });

                logger.LogInformation("POST ProductsAndServices successful.");
                return Created(location, mapper.Map<ProductsAndServicesConfirmationDto>(confirmation));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error creating new ProductsAndServices: " + e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        // PUT api/pas
        [HttpPut]
        public ActionResult<ProductsAndServicesConfirmationDto> Put([FromBody] ProductsAndServicesUpdateDto pas)
        {
            try
            {
                if (this.pasRepository.GetPASById(pas.Id) == null)
                {
                    logger.LogInformation("PUT ProductsAndServices not found.");
                    return NotFound();
                }
                ProductsAndServices pasEntity = mapper.Map<ProductsAndServices>(pas);
                ProductsAndServicesConfirmation confirmation = this.pasRepository.UpdatePAS(pasEntity);

                logger.LogInformation("PUT ProductsAndServices successful.");
                return Ok(mapper.Map<ProductsAndServicesConfirmationDto>(confirmation));                    
            } 
            catch(Exception e)
            {
                logger.LogError(e, "Error updating existing ProductsAndServices: " + e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        // DELETE api/pas/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var pas = this.pasRepository.GetPASById(id);

                if (pas == null)
                {
                    logger.LogInformation("DELETE ProductsAndServices not found.");
                    return NotFound();
                }
                this.pasRepository.DeletePAS(id);

                logger.LogInformation("DELETE ProductsAndServices successful.");
                return NoContent();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error deleting ProductsAndServices: ", e.Message);
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
