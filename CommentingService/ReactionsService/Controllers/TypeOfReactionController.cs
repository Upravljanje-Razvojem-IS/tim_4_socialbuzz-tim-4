using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using ReactionsService.AuthorizationHelper;
using ReactionsService.Data.Type_of_reaction;
using ReactionsService.LoggerHelper;
using ReactionsService.Models;
using ReactionsService.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Controllers
{
    /// <summary>
    /// TypeOfReaction Kontroler izvrsava CRUD operacije nad podacima />.
    /// </summary>
    [ApiController]
    [Route("api/reactiontypes")]
    [Produces("application/json", "application/xml")]
    public class TypeOfReactionController : ControllerBase
    {
        private readonly ITypeOfReactionRepository typeOfReactionRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly IAuthorization authorization;
        private readonly ILoggingMockRepository<TypeOfReactionController> logger;

        public TypeOfReactionController(ITypeOfReactionRepository typeOfReactionRepository, IMapper mapper, LinkGenerator linkGenerator, IAuthorization authorization, ILoggingMockRepository<TypeOfReactionController> logger)
        {
            this.typeOfReactionRepository = typeOfReactionRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.authorization = authorization;
            this.logger = logger;
        }

        /// <summary>
        /// Vraca sve postojece tipove reakcija nad objavama.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer uspesnog zahteva Get All Types Of Reaction
        /// GET 'http://localhost:44300/api/reactiontypes/' \
        ///     --header 'Authorization: Bearer 123456' 
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <response code="200">Uspesno vracena lista svih tipova reakcija nad objavama korisnika.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Nisu pronadjeni tipovi reakcije ili ne postoji nijedan tip reakcije korisnika.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<TypeOfReactionDto>> GetAllTypesOfReaction([FromHeader(Name = "Authorization")] string key)
        {
            if (!authorization.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            var types = typeOfReactionRepository.GetAllTypesOfReaction();

            if (types == null || types.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "No types of reaction found...");
            }

            logger.LogInformation("Successfully returned list of all types of reaction.");

            return Ok(mapper.Map<List<TypeOfReactionDto>>(types));
        }

        /// <summary>
        /// Vraca tip reakcije na osnovu prosledjenog ID-a.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer uspesnog zahteva Get Type Of Reaction By ID
        /// GET 'http://localhost:44300/api/reactiontypes/typeOfReactionID' \
        ///     --header 'Authorization: Bearer 123456'  \
        ///     --param  'typeOfReactionID = 1'  
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="typeOfReactionID">ID tipa reakcije</param>
        /// <response code="200">Uspesno vracen tip reakcije na osnovu ID-a.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Nije pronadjen nijedan tip rekacije sa zadatim ID-jem.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{typeOfReactionID}")]
        [HttpHead]
        public ActionResult GetTypeOfReactionByID([FromHeader(Name = "Authorization")] string key, [FromQuery] int typeOfReactionID)
        {
            if (!authorization.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            var type = typeOfReactionRepository.GetTypeOfReactionByID(typeOfReactionID);

            if (type == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "No type of reaction with that ID found...");
            }

            logger.LogInformation("Successfully returned type of reaction based on ID");

            return Ok(mapper.Map<TypeOfReactionDto>(type));
        }

        /// <summary>
        /// Kreira novi tip reakcije.
        /// </summary>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="typeOfReaction">Model reakcije koja se kreira</param>
        /// <returns></returns>
        /// <remarks>
        /// Primer uspsnog zahteva za dodavanje novog tipa reakcije \
        /// POST 'http://localhost:44300/api/reactiontypes/' \
        ///      --header 'Authorization: Bearer 123456' \
        /// {   \
        ///  "ReactionType": "Heart again", \
        /// }
        /// </remarks>
        /// <response code="201">Vraca kreirani tip reakcije.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="500">Greska na serveru.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<TypeOfReactionCreationDto> CreateTypeOfReaction([FromHeader(Name = "Authorization")] string key, [FromBody] TypeOfReactionCreationDto typeOfReaction)
        {

            try
            {
                if (!authorization.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
                }

                TypeOfReaction typeEntity = mapper.Map<TypeOfReaction>(typeOfReaction);

                typeOfReactionRepository.CreateTypeOfReaction(typeEntity);
                typeOfReactionRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetTypeOfReactionByID", "TypeOfReaction", new { typeOfReactionID = typeEntity.TypeOfReactionID });

                return Created(location, mapper.Map<TypeOfReactionDto>(typeEntity));

                //return StatusCode(StatusCodes.Status201Created, "Type of reaction is successfully created!");

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating new type of reaction: " + ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new type of reaction!");
            }

        }

        /// <summary>
        /// Modifikacija postojeceg tipa reakcije.
        /// </summary>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="updatedType">Model tipa reakcije koji se modifikuje</param>
        /// <returns></returns>
        /// <remarks>
        /// Primer uspesnog zahteva za azuriranje tipa reakcije \
        /// PUT  'http://localhost:44300/api/reactiontypes' \
        ///    --header 'Authorization: Bearer 123456' \
        /// { \
        /// "TypeOfReactionID": 1, \
        /// "ReactionType": "Updating heart", \
        /// }
        /// </remarks>
        /// <response code="200">Vraća potvrdu da je uspesno izmenjen tip reakcija.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Ne postoji tip reakcije sa datim ID-ijem a koji korisnik pokusava da modifikuje.</response>
        /// <response code="500">Greska na serveru.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPut]
        public IActionResult UpdateTypeOfReaction([FromHeader(Name = "Authorization")] string key, [FromBody] TypeOfReactionModifyingDto updatedType)
        {
            if (!authorization.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }       

            var oldType = typeOfReactionRepository.GetTypeOfReactionByID(updatedType.TypeOfReactionID);

            if (oldType == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "There is no type of reaction with that ID");
            }

            var newType = mapper.Map<TypeOfReaction>(updatedType);

            try
            {
                mapper.Map(newType, oldType);
                typeOfReactionRepository.SaveChanges();

                return Ok(mapper.Map<TypeOfReaction>(newType));

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating type of reaction: " + ex.Message); 

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating type of reaction");

            }
        }

        /// <summary>
        /// Vrši brisanje jednog tipa reakcije, na osnovu ID-ja tipa reakcije
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva za brisanje tipa reakcije
        /// DELETE 'http://localhost:44300/api/reactiontypes/' \
        ///     --header 'Authorization: Bearer 123456' \
        ///     --param  'TypeOfReactionID = 8'
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="typeOfReactionID">ID tipa reakcije koji se brise</param>
        /// <response code="204">Uspesno obrisan tip reakcije.</response>
        /// <response code="401"> Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Korisnik pokusava da obrise nepostojeci tip reakcije.</response>
        /// <response code="500">Greska na serveru.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete]
        public IActionResult DeleteTypeOfReaction([FromHeader(Name = "Authorization")] string key, [FromQuery] int typeOfReactionID)
        {
            if (!authorization.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            var type = typeOfReactionRepository.GetTypeOfReactionByID(typeOfReactionID);

            if (type == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "There is no type of reaction with that ID!");
            }
            try
            {
                typeOfReactionRepository.DeleteTypeOfReaction(typeOfReactionID);
                typeOfReactionRepository.SaveChanges();

                return NoContent();
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting type of reaction: " + ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting type of reaction!");
            }
        }

        /// <summary>
        /// Prikaz HTTP metoda koje korisnik moze da pozove.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva za prikaz dostupnih HTTP metoda
        /// OPTIONS 'http://localhost:44300/api/reactiontypes' \
        /// </remarks>
        /// <response code="200">Uspesno prikazane dostupne metode.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpOptions]
        public IActionResult GetTypesOfReactionOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }


}
