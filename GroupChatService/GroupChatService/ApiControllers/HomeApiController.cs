using GroupChatService.Database;
using GroupChatService.Infrastructure;
using GroupChatService.Infrastructure.Repository;
using GroupChatService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroupChatService.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeApiController : BaseController
    {
        private IChatRepository _repo;
        public HomeApiController(IChatRepository repo) => _repo = repo;
        
        [HttpGet("/getRoom/{id}")]
        public IActionResult Chat(int id)
        {
            return Ok(_repo.GetChat(id));
        }

        [HttpPost("/createRoom")]
        public async Task<IActionResult> CreateRoom(string name)
        {
            await _repo.CreateRoom(name, GetUserId());

            return RedirectToAction("Index");
        }

        [HttpGet("/joinRoom")]
        public async Task<IActionResult> JoinRoom(int id)
        {
            await _repo.JoinRoom(id, GetUserId());

            return RedirectToAction("Chat", "Home", new { id = id });
        }

        [HttpDelete("/deleteRoom")]
        public async Task<IActionResult> DeleteRoom(int chatId)
        {
            await _repo.DeleteRoom(chatId);

            return Ok();
        }

        [HttpPut("/updateRoom")]
        public async Task<IActionResult> UpdateRoom(Chat chat)
        {
            await _repo.UpdateRoom(chat);
            return Ok(chat);
        }
    }
}
