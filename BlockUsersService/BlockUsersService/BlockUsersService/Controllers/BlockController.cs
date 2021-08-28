using AutoMapper;
using BlockUsersService.AuthHelper;
using BlockUsersService.Data.FollowingMock;
using BlockUsersService.Entities;
using BlockUsersService.ErrorHandler;
using BlockUsersService.Models.Dto;
using BlockUsersService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockUsersService.Controllers
{
    [Route("api/blockingService")]
    [ApiController]
    public class BlockController : Controller
    {
        private readonly IBlockingService blockingService1;
        private readonly IAuthHelper authHelper1;
        private readonly IMapper mapper1;
        private readonly IFollowingMockRepository followingMockRepository1;

        public BlockController(IBlockingService blockingService, IAuthHelper authHelper, IMapper mapper, IFollowingMockRepository followingMockRepository)
        {
            this.blockingService1 = blockingService;
            this.authHelper1 = authHelper;
            this.mapper1 = mapper;
            this.followingMockRepository1 = followingMockRepository;
        }


        
        [HttpGet]
        public IActionResult GetBlocks([FromHeader]string key) {

            if (!authHelper1.AuthUser(key)) 
                return Unauthorized();

            var blocks = blockingService1.GetBlocks();

            if (blocks == null || blocks.Count == 0)
                return NotFound();

            return Ok(blocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlockById([FromHeader] string key, Guid ID) {

            if (!authHelper1.AuthUser(key))
                return Unauthorized();
            try
            {
                var one = blockingService1.GetBlockById(ID);
                if (one == null)
                    return NotFound();

                return Ok(one);
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message); 
            }

            
        }

        
        [HttpPost("block")]
        public IActionResult Block_User([FromHeader] string key, BlockCreationDto blockCreation)
        {
            if (!authHelper1.AuthUser(key))
                return Unauthorized();

            if (!followingMockRepository1.FollowingUser(blockCreation.blockerID, blockCreation.blockedID))
                return StatusCode(StatusCodes.Status500InternalServerError, $"You don't follow user with id = {blockCreation.blockedID}, so you can't block him!");
                //throw new UserException($"You don't follow user with id = {blockCreation.blockedID}, so you can't block him!");

            try
            {
                var c = blockingService1.Block_User(blockCreation);
                return Created("aaaaa", c);
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

       
        [HttpPut]
        public IActionResult Modify_Block([FromHeader] string key, BlockModifyDto blockModify)
        {
            if (!authHelper1.AuthUser(key))
                return Unauthorized();

            if (!followingMockRepository1.FollowingUser(blockModify.BlockerId, blockModify.BlockedId))
                return StatusCode(StatusCodes.Status500InternalServerError, $"You don't follow user with id = {blockModify.BlockedId}, so you can't block him!");

            try
            {
                Block old = blockingService1.ModifyBlock(blockModify);
                return Ok(old);
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("unblock")]
        public IActionResult Unblock_User([FromHeader] string key, UnblockDto unblock)
        {
            if (!authHelper1.AuthUser(key))
                return Unauthorized();

            if (!followingMockRepository1.FollowingUser(unblock.blockerID, unblock.blockedID))
                return StatusCode(StatusCodes.Status500InternalServerError, $"You don't follow user with id = {unblock.blockedID}, so you can't unblock him!");

            try
            {
                blockingService1.Unblock_User(unblock);
                return Ok($"You have unblock user with id = {unblock.blockedID} succesfully!");
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("blockerList/{userID}")]
        public ActionResult<List<BlockDto>> GetBlockerList([FromHeader] string key, int userID)
        {
            if (!authHelper1.AuthUser(key))
                return Unauthorized();

            try
            {
                var list = blockingService1.GetBlockerList(userID);
                if (list == null)
                    return NotFound();
                return Ok(list);
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("blockedList/{userID}")]
        public ActionResult<List<BlockDto>> GetBlockedList([FromHeader] string key, int userID)
        {
            if (!authHelper1.AuthUser(key))
                return Unauthorized();

            try
            {
                var list = blockingService1.GetBlockedList(userID);
                if (list == null || list.Count == 0)
                    return NotFound();
                return Ok(list);
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }




    }
}
