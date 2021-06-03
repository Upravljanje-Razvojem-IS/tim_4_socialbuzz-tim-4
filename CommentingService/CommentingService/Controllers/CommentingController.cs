namespace CommentingService.Controllers
{
    using AutoMapper;
    using CommentingService.AuthorizationHelper;
    using CommentingService.Data.Commenting;
    using CommentingService.Data.PostMock;
    using CommentingService.Entities;
    using CommentingService.LoggerHelper;
    using CommentingService.Models;
    using CommentingService.Models.Dto;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Commenting Kontroler izvrsava CRUD operacije nad podacima />.
    /// </summary>
    [ApiController]
    [Route("api/comments")]
    [Produces("application/json", "application/xml")]
    public class CommentingController : ControllerBase
    {
        
        public readonly ICommentingRepository commentingRepository;
        private readonly IPostMockRepository postMockRepository;
        private readonly IMapper mapper;
        public readonly LinkGenerator linkGenerator;
        private readonly IAuthorization authorization;
        private readonly ILoggingMockRepository<CommentingController> logger;

        public CommentingController(ICommentingRepository commentingRepository, IPostMockRepository postMockRepository, LinkGenerator linkGenerator, IMapper mapper, IAuthorization authorization, ILoggingMockRepository<CommentingController> logger)
        {
            this.commentingRepository = commentingRepository;
            this.postMockRepository = postMockRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.authorization = authorization;
            this.logger = logger;

        }

        /// <summary>
        /// Vraca sve postojece komentare.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva Get all comments
        /// GET 'http://localhost:44200/api/comments' \
        ///     --header 'Authorization: Bearer 123456'
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <response code="200">Uspesno vracena lista svih komenatara.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Nisu pronadjeni komementari ili ne postoji nijedan komentar.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<CommentDto>> GetComments([FromHeader] string key)
        {

            if (!authorization.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            var comments = commentingRepository.GetAllComments();

            if (comments == null || comments.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "No comments found...");
            }

            logger.LogInformation("Successfuly returned list of all comments.");

            return Ok(mapper.Map<List<CommentDto>>(comments));
        }

        /// <summary>
        /// Vraca sve komentare na jednoj objavi.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva Get comments by post ID
        /// GET 'http://localhost:44200/api/comments/postID' \
        ///  Primer zahteva koji prolazi \
        ///     --header 'Authorization: Bearer 123456' \
        ///     --param  'postID = 3' \
        ///     --param  'userID = 1' \
        /// Primer zahteva koji ne prolazi jer je korisnik sa ID-jem 1 blokirao korisnika sa ID-jem 2, a koji je objavio post sa ID-jem 2, pa usled toga, ne može da vidi njegove objave \
        ///     --header 'Authorization: 123456' \
        ///     --param  'postID = 2' \
        ///     --param  'userID = 1'
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="postID">ID objave</param>
        /// <param name="userID">ID korisnika koji salje zahtev</param>
        /// <response code="200">Uspesno vracena lista komentara na jednoj objavi.</response>
        /// <response code="400">Lose kreiran zahtev, npr. korisnik je blokiran.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Nije pronadjen nijedan komenar na objavi sa zadatim ID-jem.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{postID}")]
        public ActionResult<List<CommentDto>> GetCommentsByPostID([FromHeader] string key, [FromQuery] int postID, [FromQuery] int userID)
        {
            if (!this.authorization.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            if (postMockRepository.GetPostById(postID) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Post with that ID does not exist!");
            }

            var userThatPostedId = postMockRepository.GetPostById(postID).UserID;

            if (commentingRepository.CheckDidIBlockUser(userID, userThatPostedId))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "You can not see this user's posts!"); //User 1 blokirao Usera 2
            }

            var comments = commentingRepository.GetCommentsByPostID(postID, userID);

            if (comments == null || comments.Count == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "This post doesn't have any comments yet..."); //Post 2
            }

            logger.LogInformation("Successfuly returned list of all comments on a single post.");

            return Ok(mapper.Map<List<CommentDto>>(comments));
        }


        /// <summary>
        /// Kreira novi komentar.
        /// </summary>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="comment">Model komenara koji se kreira</param>
        /// <param name="userId">ID korisnika koji salje zahtev za kreiranjem komentara</param>
        /// <returns></returns>
        /// <remarks>
        /// POST 'http://localhost:44200/api/comments/' \
        /// Primer zahteva za dodavanje novog komentara koji je uspesan \
        ///  --header 'Authorization: 123456' \
        ///  --param 'userID = 1' \
        /// {     \
        ///  "PostID": 3, \
        ///  "CommentText": "Testing insert" \
        /// } \
        ///  Primer zahteva za dodavanje novog komentara koji je neuspesan jer korisnik sa ID-jem 3 ne prati korisnika sa ID-jem 1, a koji je objavio objavu sa ID-jem 1
        ///  --header 'Authorization: Bearer 123456' \
        ///  --param 'userID = 3' \
        /// {     \
        ///  "PostID": 1, \
        ///  "CommentText": "Testing insert" \
        /// }
        /// </remarks>
        /// <response code="201">Vraca kreiran komentar.</response>
        /// <response code="400">Lose kreiran zahtev, npr. korisnik poksuava da doda komentar na nepostojecu objavu ili ne prati korisnika ciju objavu komentarise.</response>
        /// <response code="500">Greska na serveru.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<CommentCreationDto> CreateComment([FromHeader] string key, [FromBody] CommentCreationDto comment, [FromQuery] int userId)
        {

            try
            {
                if (!authorization.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
                }

                if (postMockRepository.GetPostById(comment.PostID) == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Post with that ID does not exist!");
                }

                Comment commentEntity = mapper.Map<Comment>(comment);
                var post = postMockRepository.GetPostById(comment.PostID);
                var userThatPostedId = post.UserID;

                if (!commentingRepository.CheckDoIFollowUser(userId, userThatPostedId))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "You are not following this user and you can not comment his posts.");
                }

                commentEntity.UserID = userId;
                commentEntity.CommentDate = DateTime.Now;

                commentingRepository.CreateComment(commentEntity);
                commentingRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetCommentsByPostID", "Commenting", new { postID = commentEntity.PostID });

                return Created(location, mapper.Map<CommentDto>(commentEntity));

            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error creating new comment: " + ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new comment!");
            }

        }

        /// <summary>
        /// Modifikacija postojeceg komentara.
        /// </summary>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="updatedComment">Model komentara koji se modifikuje</param>
        /// <returns></returns>
        /// <remarks>
        /// PUT 'http://localhost:44200/api/comments' \
        /// Primer zahteva za azuriranje postojeceg komentara    \
        ///  --header 'Authorization: Bearer 123456'  \
        ///  { \
        /// "CommentID": "704dbb58-b673-4f20-4e97-08d9071b3c43", \
        /// "PostID": 3, \
        ///  "CommentText": "Testing updating :)" \
        ///  } 
        /// </remarks>
        /// <response code="200">Vraća potvrdu da je uspesno izmenjen komentar.</response>
        /// <response code="401" > Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Ne postoji komentar koji korisnik pokusava da modifikuje.</response>
        /// <response code="400">Lose kreiran zahtev, npr. korisnik pokusava da modifikuje Post ID.</response>
        /// <response code="500">Greska na serveru.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPut]
        public IActionResult UpdateComment([FromHeader] string key, [FromBody] CommentModifyingDto updatedComment)
        {
            if(!authorization.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            if (commentingRepository.GetCommentByID(updatedComment.CommentID) == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "There is no comment with that ID!");
            }

            var oldComment = commentingRepository.GetCommentByID(updatedComment.CommentID);
            var newComment = mapper.Map<Comment>(updatedComment);

            if (oldComment.PostID != newComment.PostID)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Post ID can not be changed!");
            }

            try
            {
                newComment.UserID = oldComment.UserID;
                newComment.CommentDate = oldComment.CommentDate;

                mapper.Map(newComment, oldComment);
                commentingRepository.SaveChanges();


                return Ok(mapper.Map<Comment>(newComment));

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating comment: " + ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating comment!");
            }
        }

        /// <summary>
        /// Vrši brisanje jednog komentara na osnovu ID-ja komentara
        /// </summary>
        /// <returns></returns>
        /// <remarks>        
        /// Primer zahteva za brisanje komentara
        /// DELETE 'http://localhost:44200/api/comments' \
        ///     --header 'Authorization: Bearer 123456' \
        ///     --param  'CommentID = 88cc4f42-f0e4-4795-d71f-08d9071b602e'
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="commentID">ID komentara koji se brise</param>
        /// <response code="204">Uspesno obrisan komenatar.</response>
        /// <response code="401" > Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Korisnik pokusava da obrise nepostojeci komentar.</response>
        /// <response code="500">Greska na serveru.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete]
        public IActionResult DeleteComment([FromHeader] string key, [FromQuery] Guid commentID)
        {
            if(!authorization.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            var comment = commentingRepository.GetCommentByID(commentID);

            if (comment == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "There is no comment with that ID!");
            }
            try
            {
                commentingRepository.DeleteComment(commentID);
                commentingRepository.SaveChanges();

                return NoContent();
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting comment: " + ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting comment!");
            }
        }

        /// <summary>
        /// Prikaz HTTP metoda koje korisnik moze da pozove.
        /// </summary>
        /// <returns></returns>
        ///  <remarks>
        /// Primer zahteva za prikaz dostupnih HTTP metoda
        /// OPTIONS 'http://localhost:44200/api/comments' \
        /// </remarks>
        /// <response code="200">Uspesno prikazane dostupne metode.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpOptions]
        public IActionResult GetCommentsOpstions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
