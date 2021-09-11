using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PASMicroservice.Entities;
using PASMicroservice.Mocks;
using PASMicroservice.Models.Listing;
using PASMicroservice.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PASMicroservice.Controllers
{
    [Route("api/listings")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    public class ListingController : ControllerBase
    {
        private readonly IListingRepository listingRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        private readonly IUserMockRepository userMockRepository;
        private readonly ILoggerMockRepository<ListingController> logger;

        public ListingController(IListingRepository listingRepository, LinkGenerator linkGenerator, IMapper mapper, 
            IUserMockRepository userMockRepository, ILoggerMockRepository<ListingController> logger)
        {
            this.listingRepository = listingRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;

            this.userMockRepository = userMockRepository;
            this.logger = logger;
        }

        /// <summary>
        /// Vraća sve listinge (proizvode i usluge)
        /// </summary>
        /// <returns>Lista listinga (proizvoda i usluga)</returns>
        /// <remarks>
        /// GET /api/listings
        /// </remarks>
        [HttpGet]
        public ActionResult<List<ListingDto>> Get()
        {
            var listing = this.listingRepository.GetListings();

            if (listing == null || listing.Count() == 0)
            {
                logger.LogInformation("GET Listing no content.");
                return NoContent();
            }

            logger.LogInformation("GET Listing successful.");
            return Ok(mapper.Map<List<ListingDto>>(listing));
        }

        // GET api/listings/5
        /// <summary>
        /// Vraća jedan listing
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public ActionResult<ListingDto> GetById(Guid id)
        {
            var listing = this.listingRepository.GetListingById(id);

            if (listing == null)
            {
                logger.LogInformation("GET Listing not found.");
                return NotFound();
            }

            logger.LogInformation("GET Listing successful.");
            return Ok(mapper.Map<ListingDto>(listing));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listing"></param>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog listinga \
        /// POST /api/listings \
        /// { \
        ///     "Name": "Lenovo laptop 110-15ISK", \
        ///     "Description": "Polovan laptop sa sledećim specifikacijama: ...", \
        ///     "Price": 500, \
        ///     "PriceContact": false, \
        ///     "PriceDeal": false, \
        ///     "CategoryId": "8d47fd86-745d-4158-6d4a-08d9741e2107", \
        ///     "UserId": 1340 \
        /// }
        /// </remarks>
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<ListingConfirmationDto> Post([FromBody] ListingCreationDto listing)
        {
            try
            {
                if (this.userMockRepository.GetUserById(listing.UserId) == null)
                {
                    logger.LogInformation("POST Listing failed.");
                    return StatusCode(StatusCodes.Status400BadRequest, "User doesn't exist.");
                }
                var listingEntity = mapper.Map<Listing>(listing);
                var confirmation = this.listingRepository.CreateListing(listingEntity);

                string location = linkGenerator.GetPathByAction("GetById", "Listing", new { id = confirmation.ListingId });

                logger.LogInformation("POST Listing successful.");
                return Created(location, mapper.Map<ListingConfirmationDto>(confirmation));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error creating new listing: " + e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        // PUT api/listings
        [HttpPut]
        public ActionResult<ListingConfirmationDto> Put([FromBody] ListingUpdateDto listing)
        {
            try
            {
                if (this.listingRepository.GetListingById(listing.ListingId) == null)
                {
                    logger.LogInformation("PUT Listing not found.");
                    return NotFound();
                }
                Listing listingEntity = mapper.Map<Listing>(listing);
                ListingConfirmation confirmation = this.listingRepository.UpdateListing(listingEntity);

                logger.LogInformation("PUT Listing successful.");
                return Ok(mapper.Map<ListingConfirmationDto>(confirmation));                    
            } 
            catch(Exception e)
            {
                logger.LogError(e, "Error updating existing Listing: " + e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        // DELETE api/listings/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var listing = this.listingRepository.GetListingById(id);

                if (listing == null)
                {
                    logger.LogInformation("DELETE Listing not found.");
                    return NotFound();
                }
                this.listingRepository.DeleteListing(id);

                logger.LogInformation("DELETE Listing successful.");
                return NoContent();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error deleting listing: ", e.Message);
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
