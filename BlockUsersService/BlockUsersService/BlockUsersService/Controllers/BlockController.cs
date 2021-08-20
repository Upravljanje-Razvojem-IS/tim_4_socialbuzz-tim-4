using AutoMapper;
using BlockUsersService.AuthHelper;
using BlockUsersService.Entities;
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
    [Route("api/blockUser")]
    [ApiController]
    public class BlockController : Controller
    {
        private readonly IBlockingService blockingService1;
        private readonly IAuthHelper authHelper1;
        private readonly IMapper mapper1;


        public BlockController(IBlockingService blockingService, IAuthHelper authHelper, IMapper mapper)
        {
            this.blockingService1 = blockingService;
            this.authHelper1 = authHelper;
            this.mapper1 = mapper;
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

        [HttpDelete]
        public IActionResult Unblock_User([FromHeader] string key, UnblockDto unblock)
        {
            if (!authHelper1.AuthUser(key))
                return Unauthorized();

            try
            {
                blockingService1.Unblock_User(unblock);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


    }
}
