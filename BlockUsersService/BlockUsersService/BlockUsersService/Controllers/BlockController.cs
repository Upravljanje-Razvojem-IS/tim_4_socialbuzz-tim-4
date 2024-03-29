﻿using AutoMapper;
using BlockUsersService.AuthHelper;
using BlockUsersService.Data.FollowingMock;
using BlockUsersService.Entities;
using BlockUsersService.Models.Dto;
using BlockUsersService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockUsersService.Controllers
{
    /// <summary>
    /// Kontroler Block servisa uz pomoc kojeg mozete blokirati i odblokirati nekog korisnika, 
    /// mozete videti listu blokiranih korisnika koje je odredjeni korisnik blokirao, 
    /// takodje i sve blokove koji trenutno postoje.
    /// </summary>
    [Route("api/blockingService")]
    [ApiController]
    public class BlockController : Controller
    {
        private readonly IBlockingService blockingService1;
        private readonly IAuthHelper authHelper1;
        private readonly IFollowingMockRepository followingMockRepository1;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public BlockController(IBlockingService blockingService, IAuthHelper authHelper, IFollowingMockRepository followingMockRepository, IMapper mapper, ILogger logger)
        {
            this.blockingService1 = blockingService;
            this.authHelper1 = authHelper;
            this.followingMockRepository1 = followingMockRepository;
            this.mapper = mapper;
            this.logger = logger;
        }


        /// <summary>
        /// Vraca sva trenutna blokiranja iz baze koja su korisnici napravili.
        /// </summary>
        /// <param name="key">Sifra uz pomoc koje korisnik ima pristup podacima</param>
        /// <returns></returns>
        /// <remarks>
        /// Primer uspesnog zahteva \
        /// - Get Blocks\
        ///    GET 'http://localhost:5003/api/blockingService' \
        ///    Header: 'Authorization(API Key): Key -> Key, Value -> Bearer sifra123' 
        /// </remarks>
        /// <response code="200">Uspesno vracena lista svih blokiranja korisnika.</response>
        /// <response code="401">Autorizacija korisnika neuspesna.</response>
        /// <response code="404">Ne postoji nijedno blokiranje korisnika.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [HttpGet]
        public ActionResult<BlockDto> GetBlocks([FromHeader]string key) {

            if (!authHelper1.AuthUser(key))
            {
                logger.LogWarning("Autorizacija korisnika neuspesna");
                return Unauthorized();
            }

            List<BlockDto> blocks = blockingService1.GetBlocks();

            if (blocks == null || blocks.Count == 0)
            {
                logger.LogWarning("Ne postoji nijedno blokiranje korisnika");
                return NotFound();
            }
          
            logger.LogInformation("Uspesno vracena lista svih blokiranja korisnika");
            return Ok(blocks);
        }


        /// <summary>
        /// Pretrazivanje po prosledjenom ID-u u listi blokiranja.
        /// </summary>
        /// <param name="key">Sifra uz pomoc koje korisnik ima pristup podacima</param>
        /// <param name="ID">Id blokiranja</param>
        /// <returns></returns>
        /// <remarks>
        /// Primer uspesnog zahteva \
        /// - Get Block By Id\
        /// 
        ///    GET 'http://localhost:5003/api/blockingService/{id}' \
        ///    {id}: 'id -> ECCC721F-5D96-4457-9A2F-1EFB548CE695' \
        ///    Header: 'Authorization(API Key): Key -> Key, Value -> Bearer sifra123'
        /// </remarks>
        /// <response code="200">Uspesno vracen blokiranje sa prosledjenim ID-em.</response>
        /// <response code="401">Autorizacija korisnika neuspesna.</response>
        /// <response code="404">Ne postoji nijedno blokiranje korisnika.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [HttpGet("{id}")]
        public ActionResult<BlockDto> GetBlockById([FromHeader] string key, Guid ID) {

            if (!authHelper1.AuthUser(key))
            {
                logger.LogWarning("Autorizacija korisnika neuspesna");
                return Unauthorized();
            }
            try
            {
                 BlockDto one = blockingService1.GetBlockById(ID);
                if (one == null)
                {
                    logger.LogWarning("Ne postoji nijedno blokiranje korisnika");
                    return NotFound();
                }

                logger.LogInformation("Uspesno vracen blokiranje sa prosledjenim ID-em");
                return Ok(one);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message); 
            }

            
        }


        /// <summary>
        /// Kreiranje novog bloka (Korisnik blokira drugog korisnika).
        /// </summary>
        /// <param name="key">Sifra uz pomoc koje korisnik ima pristup podacima</param>
        /// <param name="blockCreation">Model uz pomoc kojeg se kreira blok u bazi</param>
        /// <returns></returns>
        /// <remarks>
        /// Primer uspesnog zahteva \
        /// - Block_User\
        /// 
        ///    POST 'http://localhost:5003/api/blockingService/block' \
        ///    Body: 
        ///         {
        ///             "blockerID": 2,
        ///             "blockedID": 3
        ///         } \
        ///    Header: 'Authorization(API Key): Key -> Key, Value -> Bearer sifra123'
        /// </remarks>
        /// <response code="201">Korisnik je blokiran uspesno.</response>
        /// <response code="401">Autorizacija korisnika neuspesna.</response>
        /// <response code="400">Korisnik ne moze da blokira korisnika kojeg ne prati.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost("block")]
        public ActionResult<BlockDto> Block_User([FromHeader] string key, BlockCreationDto blockCreation)
        {
            if (!authHelper1.AuthUser(key))
            {
                logger.LogWarning("Autorizacija korisnika neuspesna");
                return Unauthorized();
            }

            if (!followingMockRepository1.FollowingUser(blockCreation.blockerID, blockCreation.blockedID))
            {
                logger.LogWarning("Korisnik ne moze da blokira korisnika kojeg ne prati");
                return StatusCode(StatusCodes.Status400BadRequest, $"You don't follow user with id = {blockCreation.blockedID}, so you can't block him!");
            }

            try
            {
                var c = blockingService1.Block_User(blockCreation);
                logger.LogInformation("Korisnik je blokiran uspesno");
                return Created("aaaaa", c);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        /// <summary>
        /// Modifikovanje postojeceg bloka.
        /// </summary>
        /// <param name="key">Sifra uz pomoc koje korisnik ima pristup podacima</param>
        /// <param name="blockModify">Model uz pomoc kojeg mozemo izmeniti odnosno modifikovati blok</param>
        /// <returns></returns>
        /// <remarks>
        /// Primer uspesnog zahteva \
        /// - Modify_Block\
        /// 
        ///    PUT 'http://localhost:5003/api/blockingService' \
        ///    Body: 
        ///         {
        ///             "id": "890843a8-106a-4d73-91d0-8df43864cf45",
        ///             "blockerID": 2,
        ///             "blockedID": 5
        ///         } \
        ///    Header: 'Authorization(API Key): Key -> Key, Value -> Bearer sifra123'
        /// </remarks>
        /// <response code="200">Blok je uspesno modifikovan.</response>
        /// <response code="401">Autorizacija korisnika neuspesna.</response>
        /// <response code="400">Korisnik ne moze da blokira korisnika kojeg ne prati.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPut]
        public ActionResult<BlockDto> Modify_Block([FromHeader] string key, BlockModifyDto blockModify)
        {
            if (!authHelper1.AuthUser(key))
            {
                logger.LogWarning("Autorizacija korisnika neuspesna");
                return Unauthorized();
            }

            if (!followingMockRepository1.FollowingUser(blockModify.BlockerId, blockModify.BlockedId))
            {
                logger.LogWarning("Korisnik ne moze da blokira korisnika kojeg ne prati");
                return StatusCode(StatusCodes.Status400BadRequest, $"You don't follow user with id = {blockModify.BlockedId}, so you can't block him!");
            }

            try
            {
                Block old = blockingService1.ModifyBlock(blockModify);

                logger.LogInformation("Blok je uspesno modifikovan");
                return Ok(mapper.Map<BlockDto>(old));
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        /// <summary>
        /// Brisanje bloka (Korisnik je odblokirao drugog korisnika).
        /// </summary>
        /// <param name="key">Sifra uz pomoc koje korisnik ima pristup podacima</param>
        /// <param name="unblock">Model uz pomoc kojeg brisemo blok u bazi</param>
        /// <returns></returns>
        /// <remarks>
        /// Primer uspesnog zahteva \
        /// - Unblock_User\
        /// 
        ///    DELETE 'http://localhost:5003/api/blockingService/unblock' \
        ///    Body: 
        ///         {
        ///             "blockerID": 2,
        ///             "blockedID": 3
        ///         } \
        ///    Header: 'Authorization(API Key): Key -> Key, Value -> Bearer sifra123'
        /// </remarks>
        /// <response code="200">Korisnik je odblokiran uspesno.</response>
        /// <response code="401">Autorizacija korisnika neuspesna.</response>
        /// <response code="400">Korisnik ne moze da blokira korisnika kojeg ne prati.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpDelete("unblock")]
        public IActionResult Unblock_User([FromHeader] string key, UnblockDto unblock)
        {
            if (!authHelper1.AuthUser(key))
            {
                logger.LogWarning("Autorizacija korisnika neuspesna");
                return Unauthorized();
            }

            if (!followingMockRepository1.FollowingUser(unblock.blockerID, unblock.blockedID))
            {
                logger.LogWarning("Korisnik ne moze da blokira korisnika kojeg ne prati");
                return StatusCode(StatusCodes.Status400BadRequest, $"You don't follow user with id = {unblock.blockedID}, so you can't unblock him!");
            }

            try
            {
                blockingService1.Unblock_User(unblock);

                logger.LogInformation("Korisnik je odblokiran uspesno");
                return Ok($"You have unblock user with id = {unblock.blockedID} succesfully!");
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        /// <summary>
        /// Vraca sve one blokirane korisnike koje je korisnik sa prosledjenim ID-em blokirao.
        /// </summary>
        /// <param name="key">Sifra uz pomoc koje korisnik ima pristup podacima</param>
        /// <param name="userID">ID korisnika koji blokira</param>
        /// <returns></returns>
        /// <remarks>
        /// Primer uspesnog zahteva \
        /// - Get Blocker List\
        ///    GET 'http://localhost:5003/api/blockingService/blockerList/{userID}' \
        ///    Header: 'Authorization(API Key): Key -> Key, Value -> Bearer sifra123' 
        /// </remarks>
        /// <response code="200">Uspesno vracena lista blokova za korisnika na osnovu ID-a.</response>
        /// <response code="401">Autorizacija korisnika neuspesna.</response>
        /// <response code="404">Nije pronadjen nijedan blok za korisnika.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [HttpGet("blockerList/{userID}")]
        public ActionResult<List<BlockDto>> GetBlockerList([FromHeader] string key, int userID)
        {
            if (!authHelper1.AuthUser(key))
            {
                logger.LogWarning("Autorizacija korisnika neuspesna");
                return Unauthorized();
            }

            try
            {
                var list = blockingService1.GetBlockerList(userID);
                if (list == null || list.Count == 0)
                {
                    logger.LogWarning("Nije pronadjen nijedan blok za korisnika");
                    return NotFound();
                }

                logger.LogInformation("Uspesno vracena lista blokova za korisnika na osnovu ID-a");
                return Ok(list);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Vraca sve korisnike koji su blokirali korisnika sa prosledjenim ID-em.
        /// </summary>
        /// <param name="key">Sifra uz pomoc koje korisnik ima pristup podacima</param>
        /// <param name="userID">ID blokiranog korisnika</param>
        /// <returns></returns>
        /// <remarks>
        /// Primer uspesnog zahteva \
        /// - Get Blocked List\
        ///    GET 'http://localhost:5003/api/blockingService/blockedList/{userID}' \
        ///    Header: 'Authorization(API Key): Key -> Key, Value -> Bearer sifra123' 
        /// </remarks>
        /// <response code="200">Uspesno vracena lista blokova blokiranog korisnika.</response>
        /// <response code="401">Autorizacija korisnika neuspesna.</response>
        /// <response code="404">Nije pronadjen nijedan blok za korisnika.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [HttpGet("blockedList/{userID}")]
        public ActionResult<List<BlockDto>> GetBlockedList([FromHeader] string key, int userID)
        {
            if (!authHelper1.AuthUser(key))
            {
                logger.LogWarning("Autorizacija korisnika neuspesna");
                return Unauthorized();
            }

            try
            {
                var list = blockingService1.GetBlockedList(userID);
                if (list == null || list.Count == 0)
                {
                    logger.LogWarning("Nije pronadjen nijedan blok za korisnika");
                    return NotFound();
                }

                logger.LogInformation("Uspesno vracena lista blokova blokiranog korisnika");
                return Ok(list);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }




    }
}
