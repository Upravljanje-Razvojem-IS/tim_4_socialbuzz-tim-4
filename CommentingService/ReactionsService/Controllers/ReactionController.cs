using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using ReactionsService.AuthorizationHelper;
using ReactionsService.Data.PostMock;
using ReactionsService.Data.Reactions;
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
    /// Reaction Kontroler izvrsava CRUD operacije nad podacima />.
    /// </summary>
    [ApiController]
    [Route("api/reactions")]
    [Produces("application/json", "application/xml")]
    public class ReactionController : ControllerBase
    {
        private readonly IReactionRepository reactionRepository;
        private readonly ITypeOfReactionRepository typeOfReactionRepository;
        private readonly IPostMockRepository postMockRepository;      
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly IAuthorization authorization;
        private readonly ILoggingMockRepository<ReactionController> logger;

        public ReactionController(IReactionRepository reactionRepository, ITypeOfReactionRepository typeOfReactionRepository, IPostMockRepository postMockRepository, IMapper mapper, LinkGenerator linkGenerator, IAuthorization authorization, ILoggingMockRepository<ReactionController> logger)
        {
            this.reactionRepository = reactionRepository;
            this.typeOfReactionRepository = typeOfReactionRepository;
            this.postMockRepository = postMockRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.authorization = authorization;
            this.logger = logger;
        }

        /// <summary>
        /// Vraca sve postojece reakcije na objavama.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva Get All Reactions
        /// GET 'http://localhost:44300/api/reactions/' \
        ///     --header 'Authorization: Bearer 123456'
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <response code="200">Uspesno vracena lista svih reakcija na objavama korisnika.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Nisu pronadjene reakcije ili ne postoji nijedna reakcija korisnika na objavama.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<ReactionsDto>> GetAllReactions([FromHeader] string key)
        {
            if (!authorization.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            var reactions = reactionRepository.GetAllReactions();

            if (reactions == null || reactions.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "No reactions found...");
            }

            logger.LogInformation("Successfully returned list of all reactions.");

            return Ok(mapper.Map<List<ReactionsDto>>(reactions));
        }

        /// <summary>
        /// Vraca sve reakcije na jednoj objavi.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// GET 'http://localhost:44300/api/reactions/postID' \
        /// Primer zahteva koji je uspesan \
        ///     --header 'Authorization: Bearer 123456' \
        ///     --param  'PostID = 1' \
        ///     --param  'UserID = 3' \
        /// Primer zahteva koji nije uspesan jer je korisnik sa ID-jem 1 blokirao korisnika sa ID-jem 2, a koji je objavio objavu sa ID-jem 2 \
        ///     --header 'Authorization: Bearer 123456' \
        ///     --param  'PostID = 2' \
        ///     --param  'UserID = 1 
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="postID">ID objave</param>
        /// <param name="userID">ID korisnika koji salje zahtev</param>
        /// <response code="200">Uspesno vracena lista reakcija na jednoj objavi.</response>
        /// <response code="400">Lose kreiran zahtev, npr. korisnik je blokiran.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{postID}")]
        public ActionResult<List<ReactionsDto>> GetReactionsByPostID([FromHeader] string key, [FromQuery] int postID, [FromQuery] int userID)
        {
            if (!authorization.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            if (postMockRepository.GetPostById(postID) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Post with that ID does not exist!");
            }

            var userThatPostedId = postMockRepository.GetPostById(postID).UserID;

            if (reactionRepository.CheckDidIBlockUser(userID, userThatPostedId))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "You can not see this user's posts!"); //User 1 blokirao Usera 2
            }

            var reactions = reactionRepository.GetReactionByPostID(postID, userID);

            if (reactions == null || reactions.Count == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "This post doesn't have any reactions yet..."); //Post 2
            }

            logger.LogInformation("Successfully returned list of all reactions on a single post.");

            return Ok(mapper.Map<List<ReactionsDto>>(reactions));
        }

        /// <summary>
        /// Kreira novu reakciju korisnika na objavi.
        /// </summary>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="reaction">Model reakcije koja se kreira</param>
        /// <param name="userId">ID korisnika koji salje zahtev za kreiranjem reakcije na objavi</param>
        /// <returns></returns>
        /// <remarks>
        /// POST 'http://localhost:44300/api/reactions/' \
        /// Primer zahteva za uspesno dodavanje nove reakcije \
        ///  --header 'Authorization: Bearer 123456' \
        ///  --param 'UserID = 1' \
        /// {     \
        ///  "PostID": 3, \
        ///  "TypeOfReactionID": 7 \
        /// } \
        /// Primer zahteva za neuspesno dodavanje reakcije jer je korisnik vec reagovao na ovu objavu \
        ///  --header 'Authorization: Bearer 123456' \
        ///  --param 'UserID = 1' \
        /// {     \
        ///  "PostID": 3, \
        ///  "TypeOfReactionID": 6 \
        /// }  \
        ///  Primer zahteva za neuspesno dodavanje reakcije jer je korisnik sa ID-jem 3 ne prati korisnika sa ID-jem 1, a koji je objavio objavu sa ID-ijem 1 \
        ///  --header 'Authorization: Bearer 123456' \
        ///  --param 'UserID = 3' \
        /// {     \
        ///  "PostID": 1, \
        ///  "TypeOfReactionID": 5 \
        /// } 
        /// </remarks>
        /// <response code="201">Vraca kreiranu reakciju na objavi.</response>
        /// <response code="400">Lose kreiran zahtev, npr. korisnik poksuava da doda reakciju na nepostojecu objavu.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="500">Greska na serveru.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<ReactionCreationDto> CreateReaction([FromHeader] string key, [FromBody] ReactionCreationDto reaction, [FromQuery] int userId)
        {

            try
            {
                if (!authorization.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
                }

                if (postMockRepository.GetPostById(reaction.PostID) == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Post with that ID does not exist!");
                }

                if (typeOfReactionRepository.GetTypeOfReactionByID(reaction.TypeOfReactionID) == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Type of reaction with that ID does not exist!");
                }

                Reaction reactionEntity = mapper.Map<Reaction>(reaction);
                var post = postMockRepository.GetPostById(reaction.PostID);
                var userThatPostedId = post.UserID;

                if (!reactionRepository.CheckDoIFollowUser(userId, userThatPostedId))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "You are not following this user and you can not react to his posts.");
                }

                if (reactionRepository.CheckDidIAlreadyReact(userId, reaction.PostID) != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "You have already reacted to this post.");
                }

                reactionEntity.UserID = userId;

                reactionRepository.CreateReaction(reactionEntity);
                reactionRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetReactionsByPostID", "Reaction", new { postID = reactionEntity.PostID });

                return Created(location, mapper.Map<ReactionsDto>(reactionEntity));

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating new reaction: " + ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new reaction!");
            }

        }

        /// <summary>
        /// Modifikacija postojece reakcije na objavi.
        /// </summary>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="updatedReaction">Model reakcije koji se modifikuje</param>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva za azuriranje reakcije  \
        /// PUT 'http://localhost:44300/api/reactions' \
        ///     --header 'Authorization: Bearer 123456'  \
        ///  { \
        /// "ReactionID": "8d3439c4-6637-40ff-b987-08d90802782c", \
        /// "PostID": 3, \
        /// "TypeOfReactionID": 2       \
        ///  }
        /// </remarks>
        /// <response code="200">Vraća potvrdu da je uspesno izmenjena reakcija.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Ne postoji reakcija koji korisnik pokusava da modifikuje.</response>
        /// <response code="400">Lose kreiran zahtev, npr. korisnik pokusava da definise reakciju kao tip reakcije koji ne postoji.</response>
        /// <response code="500">Greska na serveru.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPut]
        public IActionResult UpdateReaction([FromHeader] string key, [FromBody] ReactionModifyingDto updatedReaction)
        {
            if (!authorization.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            if (reactionRepository.GetReactionByID(updatedReaction.ReactionID) == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "There is no reaction with that ID!");
            }

            if (typeOfReactionRepository.GetTypeOfReactionByID(updatedReaction.TypeOfReactionID) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "There is no type of reaction with that ID!");

            }

            var oldReaction = reactionRepository.GetReactionByID(updatedReaction.ReactionID);
            var newReaction = mapper.Map<Reaction>(updatedReaction);

            if (oldReaction.PostID != newReaction.PostID)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Post ID can not be changed!");
            }

            try
            {
                newReaction.UserID = oldReaction.UserID;

                mapper.Map(newReaction, oldReaction);
                reactionRepository.SaveChanges();


                return Ok(mapper.Map<Reaction>(newReaction));

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating reaction: " + ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating reaction!");

            }
        }

        /// <summary>
        /// Vrši brisanje jedne reakcije korisnika na objavi, na osnovu ID-ja reakcije
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva za brisanje rekacije
        /// DELETE 'http://localhost:44300/reactions' \
        ///     --header 'Authorization: Bearer 123456' \
        ///     --param  'ReactionID = 23209e86-e2a5-4691-d1e7-08d8c11a2ff7'
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="reactionID">ID reakcija koja se brise</param>
        /// <response code="204">Uspesno obrisana reakcija.</response>
        /// <response code="401" > Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Korisnik pokusava da obrise nepostojecu reakciju.</response>
        /// <response code="500">Greska na serveru.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete]
        public IActionResult DeleteReaction([FromHeader] string key, [FromQuery] Guid reactionID)
        {
            if (!authorization.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            var reaction = reactionRepository.GetReactionByID(reactionID);

            if (reaction == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "There is no reaction with that ID!");
            }
            try
            {
                reactionRepository.DeleteReaction(reactionID);
                reactionRepository.SaveChanges();

                return NoContent();
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting reaction: " + ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting reaction!");
            }
        }

        /// <summary>
        /// Prikaz HTTP metoda koje korisnik moze da pozove.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva za prikaz dostupnih HTTP metoda
        /// OPTIONS 'https://localhost:44300/api/reactions' \
        /// </remarks>
        /// <response code="200">Uspesno prikazane dostupne metode.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpOptions]
        public IActionResult GetReactionsOpstions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
