using GroupChatService.Database;
using GroupChatService.Infrastructure;
using GroupChatService.Infrastructure.Repository;
using GroupChatService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GroupChatService.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private IChatRepository _repo;
        public HomeController(IChatRepository repo) => _repo = repo;

        public IActionResult Index()
        {
            var chats = _repo.GetChats(GetUserId());
            return View(chats);
        }

        public IActionResult Find([FromServices] AppDbContext ctx)
        {
            var users = ctx.Users
                .Where(x => x.Id != User.GetUserId())
                .ToList();

            return View(users);
        }
        public IActionResult Private()
        {
            var chats = _repo.GetPrivateChats(GetUserId());

            return View(chats);
        }
        public async Task<IActionResult> CreatePrivateRoom(string userId)
        {
            var id = await _repo.CreatePrivateRoom(GetUserId(), userId);

            return RedirectToAction("Chat", new { id });
        }

        [HttpGet("{id}")]
        public IActionResult Chat(int id)
        {
            return View(_repo.GetChat(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            await _repo.CreateRoom(name, GetUserId());

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> JoinRoom(int id)
        {
            await _repo.JoinRoom(id, GetUserId());

            return RedirectToAction("Chat","Home",new { id = id });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRoom(int chatId)
        {
            await _repo.DeleteRoom(chatId);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRoom(Chat chat)
        {
            await _repo.UpdateRoom(chat);
            return Ok(chat);
        }
    }
}
