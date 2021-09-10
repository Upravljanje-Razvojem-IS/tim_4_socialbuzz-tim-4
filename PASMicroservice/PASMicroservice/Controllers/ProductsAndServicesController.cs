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

        public ProductsAndServicesController(IProductsAndServicesRepository pasRepository, LinkGenerator linkGenerator, IMapper mapper, IUserMockRepository userMockRepository)
        {
            this.pasRepository = pasRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.userMockRepository = userMockRepository;
        }

        // GET: api/pas
        [HttpGet]
        public ActionResult<List<ProductsAndServicesDto>> Get()
        {
            var pas = this.pasRepository.GetPAS();

            if (pas == null || pas.Count() == 0)
                return NoContent();

            return Ok(mapper.Map<List<ProductsAndServicesDto>>(pas));
        }

        // GET api/pas/5
        [HttpGet("{id}")]
        public ActionResult<ProductsAndServicesDto> GetById(Guid id)
        {
            var pas = this.pasRepository.GetPASById(id);
            return Ok(mapper.Map<ProductsAndServicesDto>(pas));
        }

        // POST api/pas
        [HttpPost]
        public ActionResult<ProductsAndServicesConfirmationDto> Post([FromBody] ProductsAndServicesCreationDto pas)
        {
            try
            {
                if (this.userMockRepository.GetUserById(pas.UserId) == null)
                    return StatusCode(StatusCodes.Status400BadRequest, "User doesn't exist.");

                var pasEntity = mapper.Map<ProductsAndServices>(pas);
                var confirmation = this.pasRepository.CreatePAS(pasEntity);

                string location = linkGenerator.GetPathByAction("GetById", "ProductsAndServices", new { Id = confirmation.Id });

                return Created(location, mapper.Map<ProductsAndServicesConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
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
                    return NotFound();

                ProductsAndServices pasEntity = mapper.Map<ProductsAndServices>(pas);
                ProductsAndServicesConfirmation confirmation = this.pasRepository.UpdatePAS(pasEntity);
                return Ok(mapper.Map<ProductsAndServicesConfirmationDto>(confirmation));                    
            } 
            catch(Exception)
            {
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
                    return NotFound();

                this.pasRepository.DeletePAS(id);
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
