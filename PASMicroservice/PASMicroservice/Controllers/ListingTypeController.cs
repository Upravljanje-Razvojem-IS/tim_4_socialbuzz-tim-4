using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    /// <summary>
    /// ListingTypeController izvršava CRUD operacije nad podacima o tipovima listinga.
    /// </summary>
    [Route("api/listingTypes")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    public class ListingTypeController : ControllerBase
    {
        private readonly IListingTypeRepository listingTypeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        private readonly ILoggerMockRepository<ListingTypeController> logger;
        public ListingTypeController(IListingTypeRepository listingTypeRepository, LinkGenerator linkGenerator, IMapper mapper,
            ILoggerMockRepository<ListingTypeController> logger, IAuthenticationMock authenticationMock)
        {
            this.listingTypeRepository = listingTypeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;

            this.logger = logger;
        }

        /// <summary>
        /// Vraća sve tipove listinga
        /// </summary>
        /// <returns>Lista tipova listinga</returns>
        /// <remarks>
        /// Primer zahteva za vraćanje svih tipova listinga \
        /// GET /api/listingTypes
        /// </remarks>
        /// <response code="200">Uspešno su vraćeni svi tipovi listinga.</response>
        /// <response code="204">Ne postoji nijedan tip listinga i vraća se prazan odgovor.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ListingTypeDto>> Get()
        {
            var listingTypes = this.listingTypeRepository.GetTypes();

            if (listingTypes == null || listingTypes.Count == 0)
            {
                logger.LogInformation("GET ListingType no content.");
                return NoContent();
            }

            logger.LogInformation("GET ListingType successful.");
            return Ok(mapper.Map<List<ListingTypeDto>>(listingTypes));
        }

        /// <summary>
        /// Vraća jedan tip listinga
        /// </summary>
        /// <param name="id">id tipa listinga</param>
        /// <returns>Jedan tip listinga</returns>
        /// <remarks>
        /// Primer zahteva za vraćanje tipa listinga sa traženim id-jem \
        /// GET /api/listingTypes/1
        /// </remarks>
        /// <response code="200">Uspešno je vraćen tip listinga.</response>
        /// <response code="404">Ne postoji tip listinga sa traženim id-jem.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ListingTypeDto> GetById(int id)
        {
            var listingType = this.listingTypeRepository.GetTypeById(id);

            if (listingType == null)
            {
                logger.LogInformation("GET ListingType not found.");
                return NotFound();
            }

            logger.LogInformation("GET ListingType successful.");
            return Ok(mapper.Map<ListingTypeDto>(listingType));
        }

        /// <summary>
        /// Kreiranje novog listinga
        /// </summary>
        /// <returns>Kreiran listing</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog listinga \
        /// POST /api/listingTypes \
        /// Authorization: Bearer jwt
        /// { \
        ///     "Name": "jobs", \
        /// } \
        /// </remarks>
        /// <response code="201">Uspešno je kreiran tip listinga.</response>
        /// <response code="500">Greška na backend-u.</response>
        [HttpPost]
        [Authorize]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Izmena postojećeg tipa listinga
        /// </summary>
        /// <returns>Izmenjen tip listinga</returns>
        /// <remarks>
        /// Primer zahteva za izmenu tipa listinga \
        /// PUT /api/listingTypes
        /// Authorization: Bearer jwt
        /// { \
        ///     "ListingTypeId": 3, \
        ///     "Name": "poslovi", \
        /// } \
        /// </remarks>
        /// <response code="200">Uspešno je izmenjen tip listinga.</response>
        /// <response code="404">Ne postoji tip listinga sa datim id-jem.</response>
        /// <response code="500">Greška na backend-u.</response>
        [HttpPut]
        [Authorize]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Brisanje jednog tipa listinga
        /// </summary>
        /// <param name="id">id tipa listinga za brisanje</param>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva za brisanje tipa listinga \
        /// DELETE /api/listingTypes/3 \
        /// Authorization: Bearer jwt
        /// </remarks>
        /// <response code="204">Uspešno je obrisan tip listinga i vraća odgovor bez sadržaja.</response>
        /// <response code="404">Ne postoji tip listing sa datim id-jem.</response>
        /// <response code="500">Greška na backend-u.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Vraća dozvoljene HTTP metode na endpoint-u
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva za pregled dostupnih metoda \
        /// OPTIONS /api/listingTypes
        /// </remarks>
        /// <response code="200">Uspešno vraćene metode.</response>
        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
