using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    /// <summary>
    /// ListingController izvršava CRUD operacije nad podacima o listinzima.
    /// </summary>
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
        private readonly IAuthenticationMock authenticationMock;

        public ListingController(IListingRepository listingRepository, LinkGenerator linkGenerator, IMapper mapper, 
            IUserMockRepository userMockRepository, ILoggerMockRepository<ListingController> logger, IAuthenticationMock authenticationMock)
        {
            this.listingRepository = listingRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;

            this.userMockRepository = userMockRepository;
            this.logger = logger;
            this.authenticationMock = authenticationMock;
        }

        /// <summary>
        /// Sluzi za autentifikaciju korisnika
        /// </summary>
        /// <param name="user">Model sa podacima na osnovu kojih se vrši autentifikacija</param>
        /// <returns></returns>
        [HttpPost("auth")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Authenticate(UserDto user)
        {
            //Pokušaj autentifikacije
            if (authenticationMock.AuthenticateUser(user))
            {
                var tokenString = authenticationMock.GenerateJwt(user);
                return Ok(new { token = tokenString });
            }

            //Ukoliko autentifikacija nije uspela vraća se status 401
            return Unauthorized();
        }

        /// <summary>
        /// Vraća sve listinge uz upotrebu opcionih parametara name, categoryId i listingTypeId
        /// </summary>
        /// <returns>Lista listinga</returns>
        /// <remarks>
        /// Primer zahteva za vraćanje svih listinga i uz upotrebu parametara \
        /// GET /api/listings \
        /// GET /api/listings?name=RTX \
        /// GET /api/listings?categoryId=dcb3e419-3f9a-4f45-ae1a-df2a57e7eefa \
        /// GET /api/listings?listingTypeId=1 \
        /// GET /api/listings?name=SEO&amp;categoryId=c1df5575-00ce-4ca8-88c0-750c9fab1772&amp;listingTypeId=2 \
        /// </remarks>
        /// <param name="name">Naziv (naslov) listinga, ne mora da sadrži pun naziv već deo stringa u naslovu.</param>
        /// <param name="categoryId">Id kategorije listinga.</param>
        /// <param name="listingTypeId">Id tipa listinga. (1 = products, 2 = services)</param>
        /// <response code="200">Uspešno su vraćeni svi listinzi.</response>
        /// <response code="204">Ne postoji nijedan listing i vraća se prazan odgovor.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ListingDto>> Get(string name, string categoryId, string listingTypeId)
        {
            var listing = this.listingRepository.GetListings(name, categoryId, listingTypeId);

            if (listing == null || listing.Count == 0)
            {
                logger.LogInformation("GET Listing no content.");
                return NoContent();
            }

            logger.LogInformation("GET Listing successful.");
            return Ok(mapper.Map<List<ListingDto>>(listing));
        }

        /// <summary>
        /// Vraća jedan listing
        /// </summary>
        /// <param name="id">id listinga</param>
        /// <returns>Jedan listing</returns>
        /// <remarks>
        /// Primer zahteva za vraćanje listinga sa traženim id-jem \
        /// GET /api/listings/e466cdac-718b-4f1e-bb9a-08d974eac29a
        /// </remarks>
        /// <response code="200">Uspešno je vraćen listing.</response>
        /// <response code="404">Ne postoji listing sa traženim id-jem.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// Kreiranje novog listinga
        /// </summary>
        /// <returns>Kreiran listing</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog listinga \
        /// POST /api/listings \
        /// Authorization: Bearer jwt
        /// { \
        ///     "Name": "Lenovo laptop 110-15ISK", \
        ///     "Description": "Polovan laptop sa sledećim specifikacijama: ...", \
        ///     "Price": 500, \
        ///     "PriceContact": false, \
        ///     "PriceDeal": false, \
        ///     "CategoryId": "8d47fd86-745d-4158-6d4a-08d9741e2107", \
        ///     "ListingTypeId": 1, \
        ///     "UserId": 1337 \
        /// }
        /// </remarks>
        /// <response code="201">Uspešno je kreiran listing.</response>
        /// <response code="400">Ne postoji User sa datim UserId i nije moguće kreiranje.</response>
        /// <response code="500">Greška na backend-u.</response>
        [HttpPost]
        [Authorize]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Izmena postojećeg listinga
        /// </summary>
        /// <returns>Izmenjen listing</returns>
        /// <remarks>
        /// Primer zahteva za izmenu listinga \
        /// PUT /api/listings
        /// Authorization: Bearer jwt
        /// { \
        ///     "ListingId": "7a466dbc-c6fd-4309-bb9b-08d974eac29a", \
        ///     "Name": "Lenovo laptop 110-15ISK", \
        ///     "Description": "Polovan laptop sa sledećim specifikacijama: ...", \
        ///     "Price": 500, \
        ///     "PriceContact": false, \
        ///     "PriceDeal": false, \
        ///     "CategoryId": "8d47fd86-745d-4158-6d4a-08d9741e2107", \
        ///     "ListingTypeId": 1, \
        ///     "UserId": 1337 \
        /// }
        /// </remarks>
        /// <response code="200">Uspešno je izmenjen listing.</response>
        /// <response code="400">Ne postoji User sa datim UserId i nije moguće izmeniti listing.</response>
        /// <response code="404">Ne postoji listing sa datim id-jem.</response>
        /// <response code="500">Greška na backend-u.</response>
        [HttpPut]
        [Authorize]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public ActionResult<ListingConfirmationDto> Put([FromBody] ListingUpdateDto listing)
        {
            try
            {
                if (this.listingRepository.GetListingById(listing.ListingId) == null)
                {
                    logger.LogInformation("PUT Listing not found.");
                    return NotFound();
                }
                if (this.userMockRepository.GetUserById(listing.UserId) == null)
                {
                    logger.LogInformation("POST Listing failed.");
                    return StatusCode(StatusCodes.Status400BadRequest, "User doesn't exist.");
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

        /// <summary>
        /// Brisanje jednog listinga
        /// </summary>
        /// <param name="id">id listinga za brisanje</param>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva za brisanje listinga \
        /// DELETE /api/listings/e466cdac-718b-4f1e-bb9a-08d974eac29a
        /// Authorization: Bearer jwt
        /// </remarks>
        /// <response code="204">Uspešno je obrisan listing i vraća odgovor bez sadržaja.</response>
        /// <response code="404">Ne postoji listing sa datim id-jem.</response>
        /// <response code="500">Greška na backend-u.</response>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Vraća dozvoljene HTTP metode na endpoint-u
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva za pregled dostupnih metoda \
        /// OPTIONS /api/listings
        /// </remarks>
        /// <response code="200">Uspešno vraćene metode.</response>
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
